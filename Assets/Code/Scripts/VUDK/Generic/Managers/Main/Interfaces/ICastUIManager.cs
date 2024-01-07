namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Generic.Managers.Main.Bases;

    public interface ICastUIManager<T> where T : UIManagerBase
    {
        public T UIManager { get; }
    }
}