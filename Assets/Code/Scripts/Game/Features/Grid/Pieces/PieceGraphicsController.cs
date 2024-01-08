namespace ProjectH.Features.Grid.Pieces
{
    using UnityEngine;
    using ProjectH.Constants;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Extensions;
    using System;
    using VUDK.Features.Main.ScriptableKeys;

    [RequireComponent(typeof(Animator))]
    public class PieceGraphicsController : MonoBehaviour, IInit<Piece>
    {
        [Header("Effects")]
        [SerializeField]
        private ParticleSystem _stackedEffect;

        private Piece _piece;
        private Animator _anim;

        private void Awake()
        {
            TryGetComponent(out _anim);
        }

        public void Init(Piece piece)
        {
            _piece = piece;
        }

        public bool Check()
        {
            return _piece != null;
        }

        public void PlayCantMove(Vector2Direction direction)
        {
            _anim.RefreshAnimator();
            _anim.SetTrigger(Constants.PieceAnimations.CantMoveTrigger);

            switch (direction)
            {
                case Vector2Direction.Up:
                    PlayCantMoveUp();
                    break;
                case Vector2Direction.Right:
                    PlayCantMoveRight();
                    break;
                case Vector2Direction.Down:
                    PlayCantMoveDown();
                    break;
                case Vector2Direction.Left:
                    PlayCantMoveLeft();
                    break;
            }
        }

        public void PlayPlacedAnimation()
        {
            _anim.SetTrigger(Constants.PieceAnimations.PlaceTrigger);
        }

        public void PlayStackedEffect()
        {
            _stackedEffect.Play();
        }

        private void PlayCantMoveUp()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveUp);
        }

        private void PlayCantMoveRight()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveRight);
        }

        private void PlayCantMoveDown()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveDown);
        }

        private void PlayCantMoveLeft()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveLeft);
        }

        
    }
}