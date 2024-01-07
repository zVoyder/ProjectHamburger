namespace VUDK.Patterns.StateMachine.Interfaces
{
    public interface ICastContext<T> where T : StateContext
    {
        public T Context { get; }
    }
}