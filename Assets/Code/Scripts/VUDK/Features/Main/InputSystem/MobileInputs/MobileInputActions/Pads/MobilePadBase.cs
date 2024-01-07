namespace VUDK.Features.Main.InputSystem.MobileInputs.MobileInputActions.Pads
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;

    public abstract class MobilePadBase : InputTouchBase
    {
        public Vector2 InputDirection { get; protected set; }

        public Action<Vector2> OnInputDirection;

        protected abstract void CalculateInputDirection(Vector2 startingInputPosition);
    }
}
