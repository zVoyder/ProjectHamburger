namespace VUDK.Features.Main.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem.Interfaces;

    public abstract class InteractableTriggerBase : InteractableBase
    {
        public IInteractor Interactor { get; private set; }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractor interactor))
                interactor.InteractWith(this);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractor interactor))
                interactor.InteractWith(null);
        }
    }
}
