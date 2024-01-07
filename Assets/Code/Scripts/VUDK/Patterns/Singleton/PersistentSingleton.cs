namespace VUDK.Patterns.Singleton
{
    using UnityEngine;

    public abstract class PersistentSingleton<T> : Singleton<T> where T : Component
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}