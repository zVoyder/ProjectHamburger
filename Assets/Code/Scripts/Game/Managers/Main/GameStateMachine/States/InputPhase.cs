namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using UnityEngine.InputSystem;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.Inputs.MobileInputs.Keys;
    using VUDK.Features.Main.Inputs.MobileInputs.Interfaces;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;
    using VUDK.Extensions;
    using VUDK.Features.Main.InputSystem.MobileInputs.Utility;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using ProjectH.Features.Moves;
    using ProjectH.Constants;

    public class InputPhase : State<GameContext>, ICastMobileInput<SwipeInput>
    {
        public SwipeInput InputTouch => Context.MobileInputs.MobileInputsActions[MobileInputActionKeys.Swipe] as SwipeInput;

        private Piece _selectedPiece;

        public InputPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(InputPhase));
#endif
            InputsManager.Inputs.Touches.PrimaryTouch.performed += SelectPiece;
            InputsManager.Inputs.Touches.PrimaryTouch.canceled += DeselectPiece;
            InputTouch.OnSwipe += OnSwipe;
            EventManager.Ins.AddListener(EventKeys.PieceEvents.OnUndoMove, ChangeToMovePhase);
        }

        public override void Exit()
        {
            InputsManager.Inputs.Touches.PrimaryTouch.performed -= SelectPiece;
            InputsManager.Inputs.Touches.PrimaryTouch.canceled -= DeselectPiece;
            InputTouch.OnSwipe -= OnSwipe;
            EventManager.Ins.RemoveListener(EventKeys.PieceEvents.OnUndoMove, ChangeToMovePhase);
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void SelectPiece(InputAction.CallbackContext context)
        {
            if (!_selectedPiece && !Context.UIManager.IsOnMenu && MobileInputsUtility.IsTouchOn(out Piece piece, 10f, ~0))
                _selectedPiece = piece.CurrentTile.BottomPiece;
            else
                _selectedPiece = null;
        }

        private void DeselectPiece(InputAction.CallbackContext context = default)
        {
            _selectedPiece = null;
        }

        private void OnSwipe(Vector2Direction direction)
        {
            if (InputMoveController.TryMovePiece(_selectedPiece, direction))
                ChangeToMovePhase();

            DeselectPiece();
        }

        private void ChangeToMovePhase()
        {
            ChangeState(GamePhaseKey.MovePhase);
        }
    }
}