namespace VUDK.Features.More.ExplorationSystem.Transition.Types
{
    public class TransitionInstant : TransitionBase
    {
        public TransitionInstant(TransitionContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public override void Begin()
        {
            Context.PathExplorer.transform.SetPositionAndRotation(Context.TargetNode.NodePosition, Context.TargetNode.NodeRotation);
        }

        /// <inheritdoc/>
        public override void Process()
        {
            OnTransitionCompletedHandler();
        }

        /// <inheritdoc/>
        public override void End()
        {
        }
    }
}
