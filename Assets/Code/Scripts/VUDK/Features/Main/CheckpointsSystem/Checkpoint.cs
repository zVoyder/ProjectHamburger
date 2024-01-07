namespace VUDK.Features.Main.CheckpointSystem
{
    using UnityEngine;
    using VUDK.Features.Main.EntitySystem.Interfaces;
    using VUDK.Features.Main.TriggerSystem;

    public abstract class Checkpoint<T> : PhysicTriggerEvent where T : IEntity
    {
        [SerializeField, Header("Offset")]
        private Vector3 _positionOffset;

        private Vector3 _savePosition => transform.position + _positionOffset;

        protected override void OnTriggerEnter(Collider interactor)
        {
            if(interactor.TryGetComponent(out T ent))
            {
                CheckpointsManager.SetCheckpoint(ent, _savePosition);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            float size = transform.localScale.magnitude / 8f;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _savePosition);
            Gizmos.DrawWireSphere(_savePosition, size);
        }
#endif
    }
}