namespace VUDK.Patterns.Initialization
{
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class Initializer : MonoBehaviour, IInit
    {
        public abstract void Init();

        public abstract bool Check();
    }

    public abstract class Initializer<T> : MonoBehaviour, IInit<T> where T : IInjectArgs
    {
        protected T Args;

        public void Init(T args)
        {
            Args = args;
        }

        public virtual bool Check()
        {
            return Args != null;
        }
    }
}