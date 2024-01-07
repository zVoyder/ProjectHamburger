namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using CharacterControllerBase = VUDK.Features.Main.CharacterController.CharacterControllerBase;

    public class FirstPersonCamera : CameraFreeLook
    {
        [SerializeField, Header("First Person Settings"), Tooltip("Target Character Controller")]
        private CharacterControllerBase _targetCharacter;
        [SerializeField]
        private Vector3 _offset;
        [SerializeField, Min(0f), Tooltip("Lock to target position smooth time value")]
        private float _PositionSmoothTime = 10f;
        [SerializeField, Min(0f)]
        private float _targetRotationSpeed = 50f;

        private Vector3 _currentVelocity = Vector3.zero;

        protected override void LateUpdate()
        {
            base.LateUpdate();
            LockToPosition();
        }

        private void LockToPosition()
        {
            Vector3 targetPosition = _targetCharacter.transform.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _PositionSmoothTime * Time.deltaTime);
        }

        protected override void LookRotate()
        {
            base.LookRotate();
            _targetCharacter.transform.rotation = Quaternion.Slerp(_targetCharacter.transform.rotation, new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w), _targetRotationSpeed * Time.deltaTime);
        }
    }
}