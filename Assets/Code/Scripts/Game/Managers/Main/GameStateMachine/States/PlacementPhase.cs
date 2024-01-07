namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using UnityEngine;
    using VUDK.Extensions;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(PlacementPhase));
#endif
            Context.GameGrid.FillGrid();
            ChangeState(GamePhaseKey.InputPhase);
        }

        public override void Exit()
        {
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }
    }
}