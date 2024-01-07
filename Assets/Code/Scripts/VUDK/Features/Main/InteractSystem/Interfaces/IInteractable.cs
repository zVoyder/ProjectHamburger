namespace VUDK.Features.Main.InteractSystem.Interfaces
{
    using UnityEngine;

    public interface IInteractable
    {
        /// <summary>
        /// Interacts with this object.
        /// </summary>
        public void Interact();

        /// <summary>
        /// Enables interaction object.
        /// </summary>
        public void Enable();

        /// <summary>
        /// Disables interaction object.
        /// 
        public void Disable();
    }
}