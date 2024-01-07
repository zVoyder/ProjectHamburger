namespace VUDK.Patterns.StateMachine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Patterns.Initialization;

    public abstract class StateMachine : Initializer
    {
        private bool _isChangingState;

        public Dictionary<Enum, State> States { get; protected set; } = new Dictionary<Enum, State>();

        public State CurrentState { get; private set; }

        protected virtual void Start()
        {
            CurrentState?.Enter();
        }

        protected virtual void Update()
        {
            CurrentState?.Process();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState?.FixedProcess();
        }

        /// <summary>
        /// Initializes the <see cref="StateMachine"/> and its states.
        /// </summary>
        public override abstract void Init();

        /// <summary>
        /// Checks if the <see cref="StateMachine"/> is initialized.
        /// </summary>
        /// <returns>True if it is initialized, False if not.</returns>
        public override abstract bool Check();

        /// <summary>
        /// Changes the current state to a state in the dictionary by its key.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        public void ChangeState(Enum stateKey)
        {
            if (_isChangingState) return;
            if (States[stateKey] != CurrentState)
            {
                CurrentState?.Exit();
                CurrentState = States[stateKey];
                CurrentState?.Enter();
            }
        }

        /// <summary>
        /// Changes the state to a state in the dictionary by its key after waiting for seconds.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        /// <param name="timeToWait">Time to wait in seconds.</param>
        public void ChangeState(Enum stateKey, float timeToWait)
        {
            if (_isChangingState) return;
            StartCoroutine(WaitForSecondsChangeStateRoutine(stateKey, timeToWait));
        }

        /// <summary>
        /// Removes a state from the dictionary by its key.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        public void RemoveState(Enum stateKey)
        {
            States.Remove(stateKey);
        }

        /// <summary>
        /// Adds a state.
        /// </summary>
        /// <param name="stateKey">State to add key.</param>
        /// <param name="state">State to add.</param>
        public void AddState(Enum stateKey, State state)
        {
            States.Add(stateKey, state);
        }

        /// <summary>
        /// Checks if the current state is the same as the passed state key.
        /// </summary>
        /// <param name="stateKey">State Key to check.</param>
        /// <returns>True if is the same, False if not.</returns>
        public bool IsState(Enum stateKey)
        {
            if (CurrentState == null) return false;

            return Equals(CurrentState.StateKey, stateKey);
        }

        /// <summary>
        /// Coroutine wait for seconds before changing state.
        /// </summary>
        /// <param name="stateKey">State Key.</param>
        /// <param name="time">Time in Seconds.</param>
        /// <returns></returns>
        private IEnumerator WaitForSecondsChangeStateRoutine(Enum stateKey, float time)
        {
            _isChangingState = true;
            yield return new WaitForSeconds(time);
            _isChangingState = false;
            ChangeState(stateKey);
        }
    }
}