namespace ProjectH.Features.Victory
{
    using UnityEngine;

    public class VictoryPlate : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 10f;

        private bool _isRotating;

        public Vector3 OriginalPosition { get; private set;}

        private void Awake()
        {
            OriginalPosition = transform.position;
        }

        private void Update()
        {
            if (_isRotating)
                transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }

        public void ResetPosition()
        {
            transform.position = OriginalPosition;
        }

        public void StartRotating()
        {
            _isRotating = true;
        }

        public void StopRotating()
        {
            _isRotating = false;
        }
    }
}