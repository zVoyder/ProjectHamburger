namespace VUDK.Features.More.GameTaskSystem
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Features.Main.SaveSystem.SaveData;
    using VUDK.Features.Main.TimerSystem.Events;
    using VUDK.Features.More.GameTaskSystem.Bases;
    using VUDK.Features.More.GameTaskSystem.SaveData;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class GameTaskSaverBase<T> : GameTaskBase, ISavable, IInit where T : TaskSaveValue, new()
    {
        [Header("Task Settings")]
        [SerializeField]
        private int _taskPeriod = 24;

        protected T SaveValue;
        public int SaveID => GetInstanceID();
        public DateTime AchieveDate => SaveValue.LastCompletedTime.AddHours(_taskPeriod);

        protected virtual void Awake()
        {
            Pull();
            Init();
        }

        /// <inheritdoc/>
        public virtual void Init()
        {
            IsInProgress = SaveValue.IsInProgress;
            IsSolved = !IsTimePassed();
        }

        /// <inheritdoc/>
        public bool Check()
        {
            return SaveValue != null;
        }

        /// <summary>
        /// Returns the name of the save file.
        /// </summary>
        /// <returns>String name of the save file.</returns>
        public virtual string GetSaveName() => "Tasks";

        /// <summary>
        /// Checks if the time has passed for the task to be solved again.
        /// </summary>
        /// <returns>True if the time has passed, False if not.</returns>
        private bool IsTimePassed()
        {
            return DateTime.Now > AchieveDate;
        }

        /// <inheritdoc/>
        public override void BeginTask()
        {
            base.BeginTask();
            Push(); // Push to save task is in progress
        }

        /// <inheritdoc/>
        public override void ResolveTask()
        {
            base.ResolveTask();
            SaveValue.IsInProgress = IsInProgress;
            SaveValue.LastCompletedTime = DateTime.Now;
            DisplayTimer(true);
            Push(); // Push to save task is solved
        }

        /// <inheritdoc/>
        public void Push()
        {
            SavePacketData saveData = new SavePacketData(SaveValue);
            SaveManager.Push(GetSaveName(), SaveID, saveData);
        }

        /// <inheritdoc/>
        public void Pull()
        {
            if (SaveManager.TryPull<T>(GetSaveName(), SaveID, out SavePacketData _saveData))
                SaveValue = _saveData.Value as T;
            else
                SaveValue = new T();
        }

        /// <inheritdoc/>
        protected override void OnEnterFocus()
        {
            IsSolved = !IsTimePassed();
            base.OnEnterFocus();
        }

        /// <inheritdoc/>
        protected override void OnExitFocus()
        {
            base.OnExitFocus();
            DisplayTimer(false);
        }

        /// <summary>
        /// Returns the number of seconds to wait before the task is solved.
        /// </summary>
        /// <returns>Number of seconds.</returns>
        protected int GetSecondsToWait()
        {
            TimeSpan timeDifference = AchieveDate - DateTime.Now;
            int secondsToWait = (int)timeDifference.TotalSeconds;
            return secondsToWait;
        }

        /// <summary>
        /// Displays the timer.
        /// </summary>
        /// <param name="isEnabled">True if the timer is enabled, False if not.</param>
        protected void DisplayTimer(bool isEnabled)
        {
            if (isEnabled)
                TimerEventsHandler.StartTimerHandler(GetSecondsToWait());
            else
                TimerEventsHandler.StopTimerHandler();
        }

        /// <inheritdoc/>
        protected override void OnEnterFocusIsSolved()
        {
            DisplayTimer(true);
        }
    }

    public abstract class GameTaskSaverBase : GameTaskSaverBase<TaskSaveValue>
    {
    }
}