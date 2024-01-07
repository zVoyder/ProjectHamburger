namespace VUDK.Features.More.ExplorationSystem.Transition.Phases
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;

    public class TransitionProcess : TransitionPhaseBase
    {
        public TransitionProcess(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
            Context.Transition.OnTransitionCompleted += OnTransitionComplete;
        }

        /// <inheritdoc/>
        public override void Process()
        {
            Context.Transition.Process();
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Exit()
        {
            Context.Transition.OnTransitionCompleted -= OnTransitionComplete;
        }

        /// <inheritdoc/>
        private void OnTransitionComplete()
        {
            ChangeState(TransitionStateKey.End);
        }
    }
}
