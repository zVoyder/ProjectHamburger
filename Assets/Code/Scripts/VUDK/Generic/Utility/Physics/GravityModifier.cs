namespace VUDK.Generic.Utility.Physics
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class GravityModifier : MonoBehaviour
    {
        [SerializeField, Header("Settings")]
        private Vector3 _customGravity;

        private Rigidbody _rb;

        private void Awake()
        {
            TryGetComponent(out _rb);
        }

        private void OnEnable()
        {
            _rb.useGravity = false;
        }

        private void OnDisable()
        {
            _rb.useGravity = true;
        }

        private void FixedUpdate()
        {
            _rb.AddForce(_customGravity, ForceMode.Acceleration);
        }
    }
}