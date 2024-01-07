namespace VUDK.Features.More.ExplorationSystem.Nodes.Components
{
    using UnityEngine;
    using VUDK.Extensions;

    [DisallowMultipleComponent]
    public class NodeTarget : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawNode();
        }

        private void DrawNode()
        {
            Gizmos.color = Color.yellow;
            float size = transform.lossyScale.magnitude / 4f;
            GizmosExtension.DrawWireSphere(transform.position, size, transform.rotation);
            GizmosExtension.DrawArrowRay(transform.position, transform.forward * size);
        }
#endif
    }
}
