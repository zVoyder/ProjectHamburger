namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Extensions;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;

    public class GamevictoryPhase : State<GameContext>
    {
        public GamevictoryPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(GamevictoryPhase));
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