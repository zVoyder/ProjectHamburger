namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using System;
    using UnityEngine;

    public abstract class InputTouchBase : MonoBehaviour
    {
        public Action OnInputPerformed;
        public Action OnInputCanceled;
        public Action OnInputInvalid;
    }
}
