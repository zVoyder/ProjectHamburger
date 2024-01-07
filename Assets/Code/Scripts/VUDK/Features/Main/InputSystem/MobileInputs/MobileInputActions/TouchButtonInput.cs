namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using UnityEngine.EventSystems;
    using VUDK.Features.Main.Inputs.MobileInputs.Interfaces;

    public class TouchButtonInput : InputTouchBase, ITouchHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            OnInputPerformed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnInputCanceled?.Invoke();
        }
    }
}
