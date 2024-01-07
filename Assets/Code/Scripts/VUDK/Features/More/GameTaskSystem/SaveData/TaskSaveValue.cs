namespace VUDK.Features.More.GameTaskSystem.SaveData
{
    using System;
    using VUDK.Features.Main.SaveSystem.SaveData;

    [System.Serializable]
    public class TaskSaveValue : SaveValue
    {
        public bool IsInProgress;
        public DateTime LastCompletedTime;

        public TaskSaveValue() : base()
        {
            IsInProgress = false;
            LastCompletedTime = DateTime.MinValue;
        }
    }
}