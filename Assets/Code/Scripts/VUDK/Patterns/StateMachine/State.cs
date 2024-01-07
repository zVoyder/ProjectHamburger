namespace VUDK.Patterns.StateMachine
{
    using System;
    using VUDK.Patterns.StateMachine.Interfaces;

    public abstract class State : IEventState
    {
        private StateMachine _relatedStateMachine;

        public Enum StateKey { get; protected set; }
        protected StateContext Context { get; private set; }

        public State(Enum stateKey, StateMachine relatedStateMachine, StateContext context)
        {
            StateKey = stateKey;
            _relatedStateMachine = relatedStateMachine;
            Context = context;
        }

        /// <summary>
        /// Called when entering the state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public abstract void Exit();

        /// <summary>
        /// Called to process the state's logic each frame.
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// Called to process the state's logic each fixed frame.
        /// </summary>
        public abstract void FixedProcess();

        /// <summary>
        /// Changes the state of its related state machine.
        /// </summary>
        /// <param name="key">State key.</param>
        protected void ChangeState(Enum key) 
        {
            _relatedStateMachine.ChangeState(key);
        }

        /// <summary>
        /// Changes the state of its related state machinee to a state in the dictionary by its key after waiting for seconds.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        /// <param name="timeToWait">Time to wait in seconds.</param>
        protected void ChangeState(Enum key, float time)
        {
            _relatedStateMachine.ChangeState(key, time);
        }
    }

    public abstract class State<T> : State, ICastContext<T> where T : StateContext
    {
        public new T Context => (T)base.Context;

        public State(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
    }
}