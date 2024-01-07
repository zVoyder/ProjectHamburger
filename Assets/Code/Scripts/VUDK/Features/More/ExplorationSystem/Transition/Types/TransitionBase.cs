namespace VUDK.Features.More.ExplorationSystem.Transition.Types
{
    using System;

    public abstract class TransitionBase
    {
        protected TransitionContext Context { get; private set; }

        public Action OnTransitionCompleted;

        public TransitionBase(TransitionContext context)
        {
            Context = context;
        }

        /// <summary>
        /// On transition begin.
        /// </summary>
        public virtual void Begin()
        {
        }

        /// <summary>
        /// On transition process.
        /// </summary>
        public virtual void Process()
        {
        }

        /// <summary>
        /// On transition end.
        /// </summary>
        public virtual void End()
        {
        }

        /// <summary>
        /// On transition completed.
        /// </summary>
        public virtual void OnTransitionCompletedHandler()
        {
            OnTransitionCompleted?.Invoke();
        }
    }
}
