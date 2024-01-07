namespace VUDK.Patterns.Singleton
{
    using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T s_instance;

        public static T Ins => s_instance;

        protected virtual void Awake()
        {
            if (!s_instance)
            {
                if (!TryGetComponent(out s_instance))
                    s_instance = gameObject.AddComponent<T>();
            }
            else
                Destroy(gameObject);
        }
    }
}