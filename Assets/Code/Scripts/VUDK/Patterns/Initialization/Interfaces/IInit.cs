namespace VUDK.Patterns.Initialization.Interfaces
{
    public interface IInit
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        public void Init();

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }

    public interface IInit<T>
    {
        /// <summary>
        /// Initialize object with its arguments.
        /// </summary>
        public void Init(T arg);

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }

    public interface IInit<T1, T2>
    {
        /// <summary>
        /// Initialize object with its arguments.
        /// </summary>
        public void Init(T1 arg1, T2 arg2);

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }

    public interface IInit<T1, T2, T3>
    {
        /// <summary>
        /// Initialize object with its arguments.
        /// </summary>
        public void Init(T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }
}