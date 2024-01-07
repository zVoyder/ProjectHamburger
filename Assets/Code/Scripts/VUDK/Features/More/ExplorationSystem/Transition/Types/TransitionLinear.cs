namespace VUDK.Features.More.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class TransitionLinear : TransitionBase
    {
        protected DelayTask TimeProcess;
        protected Vector3 StartPosition;
        protected Quaternion StartRotation;

        public TransitionLinear(TransitionContext context, DelayTask timeProcess) : base(context)
        {
            TimeProcess = timeProcess;
        }

        /// <inheritdoc/>
        public override void Begin()
        {
            StartPosition = Context.PathExplorer.transform.position;
            StartRotation = Context.PathExplorer.transform.rotation;
            TimeProcess.Start();
            TimeProcess.OnTaskCompleted += OnTransitionCompletedHandler;
        }

        /// <inheritdoc/>
        public override void Process()
        {
            if (!TimeProcess.Process()) return;

            Context.PathExplorer.transform.position = Vector3.Lerp(StartPosition, Context.TargetNode.NodePosition, TimeProcess.ElapsedPercentPrecise);
            Context.PathExplorer.transform.rotation = Quaternion.Lerp(StartRotation, Context.TargetNode.NodeRotation, TimeProcess.ElapsedPercentPrecise);
        }

        /// <inheritdoc/>
        public override void End()
        {
            TimeProcess.OnTaskCompleted -= OnTransitionCompletedHandler;
        }
    }
}