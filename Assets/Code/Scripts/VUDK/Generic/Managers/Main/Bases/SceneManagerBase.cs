namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine.SceneManagement;
    using VUDK.Features.Main.SceneManagement;

    public abstract class SceneManagerBase : SceneSwitcher
    {
        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

        public void LoadNextScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ReloadScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
