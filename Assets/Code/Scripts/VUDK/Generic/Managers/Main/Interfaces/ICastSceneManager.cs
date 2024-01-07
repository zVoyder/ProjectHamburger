namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Generic.Managers.Main.Bases;

    public interface ICastSceneManager<T> where T : SceneManagerBase
    {
        public T SceneManager { get; }
    }
}
