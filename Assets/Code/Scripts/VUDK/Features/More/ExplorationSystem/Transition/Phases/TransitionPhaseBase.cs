namespace VUDK.Features.More.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Patterns.StateMachine;

    public abstract class TransitionPhaseBase : State<TransitionContext>
    {
        public TransitionPhaseBase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
    }
}
