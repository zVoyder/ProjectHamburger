namespace VUDK.Features.More.GameTaskSystem.Bases
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class GameTaskBase : MonoBehaviour
    {
        [Header("Task Settings")]
        [SerializeField]
        protected bool IsRepeatable;

        public bool IsSolved { get; protected set; }
        public bool IsInProgress { get; protected set; }
        public bool IsFocused { get; protected set; }

        [Header("Task Events")]
        [SerializeField]
        public UnityEvent OnTaskBegin;
        [SerializeField]
        public UnityEvent OnTaskResolved;
        [SerializeField]
        public UnityEvent OnTaskInterrupted;

        /// <summary>
        /// Begin task.
        /// </summary>
        public virtual void BeginTask()
        {
            OnEnterFocus();

            if (!IsSolved || IsRepeatable)
            {
                OnTaskBegin?.Invoke();
                IsInProgress = true;
            }
        }

        /// <summary>
        /// Resume task.
        /// </summary>
        public virtual void ResumeTask()
        {
            OnEnterFocus();
        }

        /// <summary>
        /// Interrupt task.
        /// </summary>
        public virtual void InterruptTask()
        {
            OnExitFocus();
            OnTaskInterrupted?.Invoke();
        }

        /// <summary>
        /// Resolve task.
        /// </summary>
        public virtual void ResolveTask()
        {
            OnTaskResolved?.Invoke();
            IsSolved = true;
            IsInProgress = false;
            OnEnterFocus();
        }

        /// <summary>
        /// On Enter Focus task.
        /// </summary>
        protected virtual void OnEnterFocus()
        {
            IsFocused = true;
            if (IsSolved && !IsRepeatable) OnEnterFocusIsSolved();
        }

        /// <summary>
        /// On Exit Focus task.
        /// </summary>
        protected virtual void OnExitFocus()
        {
            IsFocused = false;
        }

        /// <summary>
        /// On Enter Focus task when task is solved.
        /// </summary>
        protected abstract void OnEnterFocusIsSolved();
    }
}