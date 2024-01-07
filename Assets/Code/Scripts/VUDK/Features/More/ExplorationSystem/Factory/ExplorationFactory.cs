namespace VUDK.Features.More.ExplorationSystem.Factory
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.More.ExplorationSystem.Transition;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.More.ExplorationSystem.Transition.Types;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.More.ExplorationSystem.Managers;

    public static class ExplorationFactory
    {

        /// <summary>
        /// Create TransitionInstant.
        /// </summary>
        /// <param name="context">TransitionContext.</param>
        /// <returns>TransitionInstant.</returns>
        public static TransitionInstant Create(TransitionContext context)
        {
            return new TransitionInstant(context);
        }

        /// <summary>
        /// Create TransitionLinear.
        /// </summary>
        /// <param name="context">TransitionContext.</param>
        /// <param name="timeProcess">DelayTask.</param>
        /// <returns>TransitionLinear.</returns>
        public static TransitionLinear Create(TransitionContext context, DelayTask timeProcess)
        {
            return new TransitionLinear(context, timeProcess);
        }

        /// <summary>
        /// Create TransitionFov.
        /// </summary>
        /// <param name="context">TransitionContext.</param>
        /// <param name="fovChanger">CameraFovChanger.</param>
        /// <param name="timeProcess">DelayTask.</param>
        /// <returns>TransitionFov.</returns>
        public static TransitionFov Create(TransitionContext context, CameraFovChanger fovChanger, DelayTask timeProcess)
        {
            return new TransitionFov(context, fovChanger, timeProcess);
        }

        /// <summary>
        /// Create TransitionPhaseBase.
        /// </summary>
        /// <param name="key">TransitionStateKey.</param>
        /// <param name="relatedStateMachine">StateMachine.</param>
        /// <param name="context">TransitionContext.</param>
        /// <returns>TransitionPhaseBase.</returns>
        public static TransitionPhaseBase Create(TransitionStateKey key, StateMachine relatedStateMachine, TransitionContext context)
        {
            switch (key)
            {
                case TransitionStateKey.Start:
                    return new TransitionBegin(key, relatedStateMachine, context);

                case TransitionStateKey.Process:
                    return new TransitionProcess(key, relatedStateMachine, context);

                case TransitionStateKey.End:
                    return new TransitionEnd(key, relatedStateMachine, context);
            }

            return null;
        }

        /// <summary>
        /// Create TransitionContext.
        /// </summary>
        /// <param name="explorationManager">ExplorationManager.</param>
        /// <returns>TransitionContext.</returns>
        public static TransitionContext Create(ExplorationManager explorationManager)
        {
            return new TransitionContext(explorationManager);
        }
    }
}