namespace VUDK.Features.Main.Camera
{
    using UnityEngine;

    public class SimpleCameraFollow : MonoBehaviour
    {
        [SerializeField, Header("Camera")]
        private float _speed;
        [SerializeField]
        private Vector3 _cameraOffset;

        [SerializeField, Header("Target")]
        private Transform _target;

        private Vector3 _originalCameraOffset;
        private float _originalSpeed;

        private void Awake()
        {
            _originalCameraOffset = _cameraOffset;
            _originalSpeed = _speed;
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 desiredPosition = _target.position + _cameraOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.deltaTime);
        }

        public void ChangeCameraOffsetX(float x)
        {
            _cameraOffset.x = x;
        }

        public void ChangeCameraOffsetY(float y)
        {
            _cameraOffset.y = y;
        }

        public void ChangeCameraOffsetZ(float z)
        {
            _cameraOffset.z = z;
        }

        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }

        public void ResetCameraOffset()
        {
            _cameraOffset = _originalCameraOffset;
        }

        public void ResetSpeed()
        {
            _speed = _originalSpeed;
        }

        public void ResetCamera()
        {
            ResetCameraOffset();
            ResetSpeed();
        }
    }
}