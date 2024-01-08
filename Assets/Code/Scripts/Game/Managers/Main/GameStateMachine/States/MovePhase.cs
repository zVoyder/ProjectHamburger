namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using ProjectH.Constants;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;

    public class MovePhase : State<GameContext>
    {
        public MovePhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(MovePhase));
#endif
            EventManager.Ins.AddListener(EventKeys.PieceEvents.OnMoveAnimationCompleted, OnMoveCompleted);
        }

        /// <inheritdoc/>
        public override void Exit()
        {
            EventManager.Ins.RemoveListener(EventKeys.PieceEvents.OnMoveAnimationCompleted, OnMoveCompleted);
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <summary>
        /// Callback for when the move animation is completed.
        /// </summary>
        private void OnMoveCompleted()
        {
            ChangeState(GamePhaseKey.CheckPhase);
        }
    }
}