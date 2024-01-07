namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Extensions;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;

    public class CheckPhase : State<GameContext>
    {
        public CheckPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(CheckPhase));
#endif
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