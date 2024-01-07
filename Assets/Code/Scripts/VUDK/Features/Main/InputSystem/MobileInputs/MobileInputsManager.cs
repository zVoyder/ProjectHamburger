namespace VUDK.Features.Main.InputSystem.MobileInputs
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;
    using VUDK.Features.Main.Inputs.MobileInputs.Keys;
    using VUDK.Patterns.Singleton;

    [DefaultExecutionOrder(-500)]
    public sealed class MobileInputsManager : Singleton<MobileInputsManager>
    {
        [field: SerializeField, Header("Mobile Inputs")]
        public SerializableDictionary<MobileInputActionKeys, InputTouchBase> MobileInputsActions { get; private set; }
    }
}
