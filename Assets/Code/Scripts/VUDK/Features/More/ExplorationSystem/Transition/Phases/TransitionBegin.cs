namespace VUDK.Features.More.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

    public class TransitionBegin : TransitionPhaseBase
    {
        public TransitionBegin(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey,  relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnBeginTransition);
            Context.Transition.Begin();
            Context.PreviousNode.OnNodeExit();
            ChangeState(TransitionStateKey.Process);
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Exit()
        {
        }
    }
}
