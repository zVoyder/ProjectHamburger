namespace VUDK.Features.Main.Inputs.MobileInputs.Behaviours
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Features.Main.InputSystem.MobileInputs.Utility;

    public abstract class TouchBehaviour2D : MonoBehaviour
    {
        protected MobileInputsManager MobileInputsManager;

        private bool _isTouchDown;

        protected void Init(MobileInputsManager inputsManager)
        {
            MobileInputsManager = inputsManager;
        }

        protected virtual void OnEnable()
        {
            InputsManager.Inputs.Touches.PrimaryTouch.canceled += TouchUp;
        }

        protected virtual void Update()
        {
            if(InputsManager.Inputs.Touches.PrimaryTouch.IsInProgress())
                TouchDown();
        }

        protected virtual void OnTouchDown2D()
        {
        }

        protected virtual void OnTouchUp2D()
        {
        }

        private void TouchDown()
        {
            if(_isTouchDown) return;
            _isTouchDown = true;

            RaycastHit2D hit = MobileInputsUtility.RaycastFromTouch2D(~0);

            if (hit && hit.transform.TryGetComponent(out TouchBehaviour2D touch))
            {
                if (touch == this)
                    OnTouchDown2D();
            }
        }

        private void TouchUp(InputAction.CallbackContext obj)
        {
            if(_isTouchDown)
            {
                _isTouchDown = false;
                OnTouchUp2D();
            }
        }
    }
}
