namespace VUDK.Features.Main.DragSystem
{
    using UnityEngine;

    public interface IDraggable
    {
        /// <summary>
        /// On start dragging object.
        /// </summary>
        public void OnStartDragObject();

        /// <summary>
        /// On end dragging object.
        /// </summary>
        public void OnEndDragObject();

        /// <summary>
        /// Gets the dragged object transform.
        /// </summary>
        /// <returns>Dragged object transform.</returns>
        public Transform GetDragTransform();
    }
}