namespace ProjectH.Features.Moves
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Serializable;
    using ProjectH.Constants;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Moves.Undo;

    public class PiecesMovesGraphicsController : MonoBehaviour
    {
        private const float Angle = 180f;

        [SerializeField, Min(0)]
        private float _pieceSpacing;

        [SerializeField]
        private float _moveDuration = 1f;

        private DelayTask _moveTask;

        private Piece _pieceToRotate;
        private GameGridTile _targetTile;
        private List<Piece> _oldStack;

        private Quaternion _targetRotation;
        private Vector3 _rotationPoint;
        private Vector3 _axisRotation;
        private float _stepAngle;

        private float PieceHeight => _pieceSpacing * 2f;

        private void Awake()
        {
            _moveTask = new DelayTask(_moveDuration, OnAnimationCompleted);
            _stepAngle = Angle / _moveDuration;
        }

        private void Update()
        {
            if (_moveTask.Process())
                ProcessRotationAnimation();
        }

        /// <summary>
        /// Animate piece move.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <param name="toTile">Tile to move to.</param>
        /// <param name="direction">Direction to move.</param>
        public void AnimateMovePiece(Piece piece, GameGridTile toTile, Vector2Direction direction)
        {
            _pieceToRotate = piece;
            _targetTile = toTile;
            _oldStack = _pieceToRotate.CurrentTile.Stack;

            switch (direction)
            {
                case Vector2Direction.Up:
                    CalculateRotationUp();
                    break;

                case Vector2Direction.Down:
                    CalculateRotationDown();
                    break;

                case Vector2Direction.Left:
                    CalculateRotationLeft();
                    break;

                case Vector2Direction.Right:
                    CalculateRotationRight();
                    break;
            }

            EventManager.Ins.TriggerEvent(EventKeys.PieceEvents.OnMoveAnimationStarted);
            UndoController.AddUndoMove(new UndoMove(_pieceToRotate, _oldStack, _pieceToRotate.CurrentTile, _rotationPoint, _axisRotation));
            _moveTask.Start();
        }

        /// <summary>
        /// Animate undo move.
        /// </summary>
        /// <param name="undoMove">Undo move to animate.</param>
        public void AnimateUndoMove(UndoMove undoMove)
        {
            _pieceToRotate = undoMove.UndoPiece;
            _targetTile = undoMove.TargetTile;
            _rotationPoint = undoMove.RotationPoint;
            _axisRotation = -undoMove.AxisDirection;
            _oldStack = undoMove.OldStack;

            _moveTask.Start();
        }

        /// <summary>
        /// Calculate right pivot point rotation.
        /// </summary>
        private void CalculateRotationRight()
        {
            _rotationPoint = _targetTile.GetWestPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateRotationPointHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = -Vector3.forward;
        }

        /// <summary>
        /// Calculate left pivot point rotation.
        /// </summary>
        private void CalculateRotationLeft()
        {
            _rotationPoint = _targetTile.GetEastPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateRotationPointHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = Vector3.forward;
        }

        /// <summary>
        /// Calculate up pivot point rotation.
        /// </summary>
        private void CalculateRotationUp()
        {
            _rotationPoint = _targetTile.GetSouthPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateRotationPointHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = Vector3.right;
        }

        /// <summary>
        /// Calculate down pivot point rotation.
        /// </summary>
        private void CalculateRotationDown()
        {
            _rotationPoint = _targetTile.GetNorthPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateRotationPointHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = -Vector3.right;
        }

        /// <summary>
        /// Calculate rotation point height.
        /// </summary>
        /// <param name="firstStack">First stack.</param>
        /// <param name="secondStack">Second stack.</param>
        /// <returns>Rotation point height.</returns>
        private float CalculateRotationPointHeight(List<Piece> firstStack, List<Piece> secondStack)
        {
            float lowestCount = Mathf.Min(firstStack.Count, secondStack.Count);
            float difference = Mathf.Abs(firstStack.Count - secondStack.Count);

            float totalHeight = (lowestCount + (difference / 2f)) * PieceHeight;
            return totalHeight;
        }

        /// <summary>
        /// Process rotation animation.
        /// </summary>
        private void ProcessRotationAnimation()
        {
            _pieceToRotate.transform.RotateAround(_rotationPoint, _axisRotation, _stepAngle * Time.deltaTime);
        }

        /// <summary>
        /// On animation completed.
        /// </summary>
        private void OnAnimationCompleted()
        {
            AdjustPiecePosition();
            InputMoveController.InsertStackInTile(_oldStack, _pieceToRotate, _pieceToRotate.CurrentTile, _targetTile);
            EventManager.Ins.TriggerEvent(EventKeys.PieceEvents.OnMoveAnimationCompleted);
        }

        /// <summary>
        /// Adjusts piece position.
        /// </summary>
        private void AdjustPiecePosition()
        {
            _pieceToRotate.transform.rotation = QuaternionExtensions.RoundToFundamentalAngles(_pieceToRotate.transform.rotation);
            if (_targetTile.StackCount > 0)
            {
                Vector3 offset = Vector3.up * (PieceHeight * (_targetTile.StackCount + _pieceToRotate.CurrentTile.StackCount - 1));
                _pieceToRotate.transform.position = _targetTile.transform.position + offset;
            }
            else
            {
                _pieceToRotate.transform.position = _targetTile.transform.position;
            }
        }

        private void OnDrawGizmos()
        {
            if (_targetTile == null || !_moveTask.IsRunning) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_rotationPoint, 0.01f);
        }
    }
}