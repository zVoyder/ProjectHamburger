namespace VUDK.Features.Main.Camera.CameraModifiers
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    
    public class CameraFovChanger : CameraModifierBase
    {
        [Header("FOV Settings")]
        [SerializeField]
        private float _targetFov;
        [field: SerializeField]
        public DelayTask TimeProcess { get; private set; }

        private float _originalFov;
        private float _fromFov;
        private float _toFov;

        protected override void Awake()
        {
            base.Awake();
            _originalFov = Camera.fieldOfView;
        }

        private void Update()
        {
            if(!TimeProcess.Process()) return;

            Camera.fieldOfView = Mathf.SmoothStep(_fromFov, _toFov, TimeProcess.ElapsedPercentPrecise);
        }

        /// <summary>
        /// Starts fov change effect.
        /// </summary>
        public void Change()
        {
            ChangeFov(Camera.fieldOfView, _targetFov);
            TimeProcess.Start();
        }

        /// <summary>
        /// Reverts fov change effect.
        /// </summary>
        public void Revert()
        {
            ChangeFov(Camera.fieldOfView, _originalFov);
            TimeProcess.Start();
        }

        private void ChangeFov(float fromFov, float toFov)
        {
            _fromFov = fromFov;
            _toFov = toFov;
        }
    }
}
