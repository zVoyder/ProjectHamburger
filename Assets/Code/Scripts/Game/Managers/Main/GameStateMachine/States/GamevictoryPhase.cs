namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Constants;

    public class GamevictoryPhase : State<GameContext>
    {
        public GamevictoryPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(GamevictoryPhase));
#endif
            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnGameVictory);
        }

        /// <inheritdoc/>
        public override void Exit()
        {
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }
    }
}