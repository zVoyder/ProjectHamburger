namespace VUDK.Patterns.StateMachine
{
    public abstract class InputContext : StateContext
    {
        public InputsMap Inputs { get; protected set; }

        public InputContext(InputsMap inputs) : base()
        {
            Inputs = inputs;
        }
    }
}