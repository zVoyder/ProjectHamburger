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

        /// <inheritdoc />
        public void Init(Piece piece)
        {
            _piece = piece;
        }

        /// <inheritdoc />
        public bool Check()
        {
            return _piece != null;
        }
        
        /// <summary>
        /// Plays can't move animation.
        /// </summary>
        /// <param name="direction">Specified direction.</param>
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

        /// <summary>
        /// Plays placed animation.
        /// </summary>
        public void PlayPlacedAnimation()
        {
            _anim.SetTrigger(Constants.PieceAnimations.PlaceTrigger);
        }

        /// <summary>
        /// Plays stacked effect.
        /// </summary>
        public void PlayStackedEffect()
        {
            _stackedEffect.Play();
        }

        /// <summary>
        /// Plays can't move up animation.
        /// </summary>
        private void PlayCantMoveUp()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveUp);
        }

        /// <summary>
        /// Plays can't move right animation.
        /// </summary>
        private void PlayCantMoveRight()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveRight);
        }

        /// <summary>
        /// Plays can't move down animation.
        /// </summary>
        private void PlayCantMoveDown()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveDown);
        }

        /// <summary>
        /// Plays can't move left animation.
        /// </summary>
        private void PlayCantMoveLeft()
        {
            _anim.SetInteger(Constants.PieceAnimations.CantMoveState, Constants.PieceAnimations.CantMoveLeft);
        }

        
    }
}