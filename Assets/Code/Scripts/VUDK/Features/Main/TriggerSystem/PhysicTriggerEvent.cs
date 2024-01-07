namespace VUDK.Features.Main.TriggerSystem
{
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(Collider))]
    public class PhysicTriggerEvent : MonoBehaviour, ITrigger
    {
        [SerializeField, Header("Events")]
        protected UnityEvent OnEnter;
        [SerializeField]
        protected UnityEvent OnExit;

        public virtual void Trigger()
        {
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
            Trigger();
        }
    }
}