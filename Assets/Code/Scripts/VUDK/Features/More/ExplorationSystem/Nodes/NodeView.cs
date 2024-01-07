namespace VUDK.Features.More.ExplorationSystem.Nodes
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

    public class NodeView : NodeInteractiveBase
    {
        /// <inheritdoc/>
        public override void OnNodeEnter()
        {
            base.OnNodeEnter();
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnEnterNodeView);
        }

        /// <inheritdoc/>
        public override void OnNodeExit()
        {
            base.OnNodeExit();
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExitNodeView);
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawPreview();
        }

        protected override void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, "-View");
        }

        private void DrawPreview()
        {
            if (IsNodeSelectedInScene())
            {
                if (!Camera.main) return;

                Camera cam = Camera.main;
                GizmosExtension.DrawCameraFrustum(NodeTarget.transform, cam.fieldOfView, cam.nearClipPlane, cam.farClipPlane, cam.aspect);
            }
        }
#endif
    }
}