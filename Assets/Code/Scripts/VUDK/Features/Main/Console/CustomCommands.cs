namespace VUDK.Features.Main.Console
{
    using UnityEngine;

    [RequireComponent(typeof(Console))]
    public abstract class CustomCommands : MonoBehaviour
    {
        protected Console Console;

        private void Awake()
        {
            TryGetComponent(out Console);
        }

        protected abstract void Commands(string command);
    }
}
