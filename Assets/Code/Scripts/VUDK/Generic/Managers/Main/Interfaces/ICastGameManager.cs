namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Generic.Managers.Main.Bases;

    public interface ICastGameManager<T> where T : GameManagerBase
    {
        public T GameManager { get; }
    }
}
