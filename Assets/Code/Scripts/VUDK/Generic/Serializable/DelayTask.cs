namespace VUDK.Generic.Serializable
{
    using System;
    using UnityEngine;

    [System.Serializable]
    public class DelayTask
    {
        [field: SerializeField, Min(0f)]
        public float Duration { get; private set; }

        private bool _isProcessing;

        public float ElapsedTime { get; private set; }
        public bool IsCompleted { get; private set; }

        public bool IsRunning => _isProcessing;
        public float ElapsedPercentNormalized => Mathf.Clamp01(ElapsedPercentPrecise);
        public float ElapsedPercent => ElapsedTime / Duration;
        public float ElapsedPercentPrecise => ElapsedPercent * ElapsedPercent * (3f - 2f * ElapsedPercent);

        public event Action OnTaskCompleted;

        public DelayTask()
        {
        }

        public DelayTask(float duration)
        {
            Duration = duration;
        }

        public DelayTask(float duration, Action onTaskCompleted)
        {
            Duration = duration;
            OnTaskCompleted = onTaskCompleted;
        }

        public void Start()
        {
            Reset();
            _isProcessing = true;
        }

        public void Start(float changeDuration)
        {
            ChangeDuration(changeDuration);
            Start();
        }

        public void Stop() => _isProcessing = false;
        
        public void Resume() => _isProcessing = true;
        
        public void Reset()
        {
            IsCompleted = false;
            _isProcessing = false;
            ElapsedTime = 0;
        }

        public bool Process() => Process(Time.deltaTime);

        public bool Process(float time)
        {
            if (!_isProcessing) return false;
            if (ElapsedTime >= Duration)
            {
                Complete();
                return false;
            }
            ElapsedTime += time;
            return true;
        }

        public void ChangeDuration(float duration)
        {
            Duration = duration;
            Reset();
        }

        private void Complete()
        {
            IsCompleted = true;
            _isProcessing = false;
            OnTaskCompleted?.Invoke();
        }
    }
}
