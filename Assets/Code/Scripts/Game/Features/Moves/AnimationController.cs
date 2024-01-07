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

    public class AnimationController : MonoBehaviour
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
        //private bool _isUndo;

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

            UndoController.AddUndoMove(new UndoMove(_pieceToRotate, _oldStack, _pieceToRotate.CurrentTile, _rotationPoint, _axisRotation));
            _moveTask.Start();
        }

        public void AnimateUndoMove(UndoMove undoMove)
        {
            _pieceToRotate = undoMove.UndoPiece;
            _targetTile = undoMove.TargetTile;
            _rotationPoint = undoMove.RotationPoint;
            _axisRotation = -undoMove.AxisDirection;
            _oldStack = undoMove.OldStack;

            _moveTask.Start();
        }

        private void CalculateRotationRight()
        {
            _rotationPoint = _targetTile.GetWestPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateTotalHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = -Vector3.forward;
        }

        private void CalculateRotationLeft()
        {
            _rotationPoint = _targetTile.GetEastPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateTotalHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = Vector3.forward;
        }

        private void CalculateRotationUp()
        {
            _rotationPoint = _targetTile.GetSouthPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateTotalHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = Vector3.right;
        }

        private void CalculateRotationDown()
        {
            _rotationPoint = _targetTile.GetNorthPosition(new Vector3(0, -_pieceSpacing, 0)) + Vector3.up * CalculateTotalHeight(_pieceToRotate.CurrentTile.Stack, _targetTile.Stack);
            _axisRotation = -Vector3.right;
        }

        private float CalculateTotalHeight(List<Piece> firstStack, List<Piece> secondStack)
        {
            float lowestCount = Mathf.Min(firstStack.Count, secondStack.Count);
            float difference = Mathf.Abs(firstStack.Count - secondStack.Count);

            float totalHeight = (lowestCount + (difference / 2f)) * PieceHeight;
            return totalHeight;
        }

        private void ProcessRotationAnimation()
        {
            _pieceToRotate.transform.RotateAround(_rotationPoint, _axisRotation, _stepAngle * Time.deltaTime);
        }

        private void OnAnimationCompleted()
        {
            AdjustPiecePosition();
            InputMoveController.InsertStackInTile(_oldStack, _pieceToRotate.CurrentTile, _targetTile);
            EventManager.Ins.TriggerEvent(EventKeys.PieceEvents.OnMoveAnimationCompleted);
        }

        private void AdjustPiecePosition()
        {
            _pieceToRotate.transform.rotation = QuaternionExtensions.RoundToFundamentalAngles(_pieceToRotate.transform.rotation);
            if (_targetTile.StackCount > 0)
            {
                Vector3 offset = Vector3.up * (PieceHeight * (_targetTile.StackCount + _pieceToRotate.CurrentTile.StackCount - 1));
                _pieceToRotate.transform.position = _targetTile.transform.position + offset;
                _pieceToRotate.transform.SetParent(_targetTile.TopPiece.transform);
            }
            else
                _pieceToRotate.transform.SetParent(_targetTile.transform);
        }

        private void OnDrawGizmos()
        {
            if (_targetTile == null || !_moveTask.IsRunning) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_rotationPoint, 0.01f);
        }
    }
}