namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using ProjectH.Constants;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(PlacementPhase));
#endif
            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnGameBegin);
            Context.GameGrid.Init(Context.LevelManager.GetCurrentLevelData());
            ChangeState(GamePhaseKey.InputPhase);
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