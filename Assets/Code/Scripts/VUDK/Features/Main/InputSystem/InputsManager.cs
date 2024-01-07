namespace VUDK.Features.Main.InputSystem
{
    public class InputsManager
    {
        public static InputsMap Inputs { get; private set; }

        static InputsManager()
        {
            Inputs = new InputsMap();
            Inputs.Enable();
        }
    }
}
