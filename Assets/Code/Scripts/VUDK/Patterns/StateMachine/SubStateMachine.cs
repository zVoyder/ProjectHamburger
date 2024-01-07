namespace VUDK.Patterns.StateMachine
{
    public abstract class SubStateMachine : StateMachine
    {
        public StateMachine ParentStateMachine { get; private set; }

        /// <summary>
        /// Initializes the <see cref="SubStateMachine"/>.
        /// </summary>
        /// <param name="parentStateMachine">Parent <see cref="StateMachine{TKey, TContext}"/>.</param>
        public virtual void Init(StateMachine parentStateMachine)
        {
            ParentStateMachine = parentStateMachine;
            Init();
        }
    }
}