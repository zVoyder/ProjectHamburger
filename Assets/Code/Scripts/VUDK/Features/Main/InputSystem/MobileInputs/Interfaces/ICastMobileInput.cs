namespace VUDK.Features.Main.Inputs.MobileInputs.Interfaces
{
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;

    public interface ICastMobileInput<T> where T : InputTouchBase
    {
        public T InputTouch { get; }
    }
}
