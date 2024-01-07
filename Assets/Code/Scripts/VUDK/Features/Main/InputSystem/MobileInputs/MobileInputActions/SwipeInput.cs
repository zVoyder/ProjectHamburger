namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Extensions;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.InputSystem.MobileInputs.Utility;

    public class SwipeInput : InputTouchBase
    {
        [SerializeField, Min(0.1f), Header("Swipe Settings")]
        private float _swipeStrength;

        public Action<Vector2Direction> OnSwipe;

        private Vector2Direction _swipeDirection;

        private Vector2 _fingerStartPosition;
        private Vector2 _fingerEndPosition;

        private void OnEnable()
        {
            InputsManager.Inputs.Touches.PrimaryTouch.performed += BeginSwipe;
            InputsManager.Inputs.Touches.PrimaryTouch.canceled += EndSwipe;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Touches.PrimaryTouch.performed -= BeginSwipe;
            InputsManager.Inputs.Touches.PrimaryTouch.canceled -= EndSwipe;
        }

        private void BeginSwipe(InputAction.CallbackContext context)
        {
            _fingerStartPosition = MobileInputsUtility.GetRawTouchPosition();
        }

        private void EndSwipe(InputAction.CallbackContext context)
        {
            _fingerEndPosition = MobileInputsUtility.GetRawTouchPosition();
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            if(Vector2.Distance(_fingerStartPosition, _fingerEndPosition) < _swipeStrength)
            {
                OnInputInvalid?.Invoke();
                return;
            }

            Vector2 swipeDirection = _fingerEndPosition - _fingerStartPosition;
            float angle = Vector2.SignedAngle(Vector2.right, swipeDirection);

            if (angle < -45f && angle >= -135f)
            {
                _swipeDirection = Vector2Direction.Down;
            }
            else if (angle >= 45f && angle < 135f)
            {
                _swipeDirection = Vector2Direction.Up;
            }
            else if ((angle >= 135f && angle <= 180f) || (angle >= -180f && angle < -135f))
            {
                _swipeDirection = Vector2Direction.Left;
            }
            else if (angle >= -45f && angle < 45f)
            {
                _swipeDirection = Vector2Direction.Right;
            }

            SendInput();
        }

        private void SendInput()
        {
            OnInputPerformed?.Invoke();
            OnSwipe?.Invoke(_swipeDirection);
        }
    }
}