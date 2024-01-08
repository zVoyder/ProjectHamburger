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

        /// <summary>
        /// Resets plate position.
        /// </summary>
        public void ResetPosition()
        {
            transform.position = OriginalPosition;
        }

        /// <summary>
        /// Starts rotating plate.
        /// </summary>
        public void StartRotating()
        {
            _isRotating = true;
        }

        /// <summary>
        /// Stops rotating plate.
        /// </summary>
        public void StopRotating()
        {
            _isRotating = false;
        }
    }
}