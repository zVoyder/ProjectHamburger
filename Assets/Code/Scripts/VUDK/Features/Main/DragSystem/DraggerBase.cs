namespace VUDK.Features.Main.DragSystem
{
    using UnityEngine;

    public abstract class DraggerBase : MonoBehaviour
    {
        [SerializeField, Min(0f), Header("Drag Settings")]
        private float _dragSpeed = 10f;

        private Vector3 _dragOffset;

        public IDraggable DraggedObject { get; private set; }
        public bool IsDragging => DraggedObject != null;

        protected virtual void Update() => Drag();

        /// <summary>
        /// Starts dragging object.
        /// </summary>
        /// <param name="draggedObject">Draggable object to drag.</param>
        /// <param name="offset">offset from the draggable object.</param>
        public virtual void StartDrag(IDraggable draggedObject, Vector3 offset = default)
        {
            DraggedObject = draggedObject;
            _dragOffset = offset;
            draggedObject.OnStartDragObject();
        }

        /// <summary>
        /// Stops dragging object.
        /// </summary>
        public virtual void StopDrag()
        {
            DraggedObject.OnEndDragObject();
            DraggedObject = null;
        }

        /// <summary>
        /// Calculates the target position for the dragged object.
        /// </summary>
        /// <returns>Target Position.</returns>
        protected abstract Vector3 CalculateTargetPosition();

        /// <summary>
        /// Drags the object.
        /// </summary>
        private void Drag()
        {
            if(!IsDragging) return;

            Vector2 fromPosition = DraggedObject.GetDragTransform().position;
            Vector2 targetPosition = CalculateTargetPosition() - _dragOffset;
            DraggedObject.GetDragTransform().position = Vector2.Lerp(fromPosition, targetPosition, Time.deltaTime * _dragSpeed);
        }
    }
}
