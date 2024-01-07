namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Features.Main.EventSystem;

    public interface ICastEventManager<T> where T : EventManager
    {
        public T EventManager { get; }
    }
}
