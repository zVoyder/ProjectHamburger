namespace VUDK.Features.Main.DragSystem
{
    using UnityEngine;

    public abstract class DraggableObjectBase : MonoBehaviour, IDraggable
    {
        public abstract void OnStartDragObject();

        public abstract void OnEndDragObject();

        public Transform GetDragTransform() => transform;
    }
}
