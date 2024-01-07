namespace VUDK.Features.Main.TimerSystem
{
    using System.Collections;
    using UnityEngine;
    using VUDK.Features.Main.TimerSystem.Events;

    public class Timer : MonoBehaviour
    {
        [Header("Timer Settings")]
        [SerializeField, Min(0)]
        private int _time;

        private void OnEnable()
        {
            TimerEvents.StartTimerHandler += StartTimer;
            TimerEvents.StopTimerHandler += StopTimer;
        }

        private void OnDisable()
        {
            TimerEvents.StartTimerHandler -= StartTimer;
            TimerEvents.StopTimerHandler -= StopTimer;
        }

        [ContextMenu("Start Timer")]
        public void StartTimer()
        {
            StartTimer(_time);
        }

        public void StartTimer(int time)
        {
            if (time < 0) return;

            TimerEvents.OnTimerStart?.Invoke();
            StartCoroutine(CountdownRoutine(time));
        }

        [ContextMenu("Stop Timer")]
        public void StopTimer()
        {
            TimerEvents.OnTimerStop?.Invoke();
            StopAllCoroutines();
        }

        private IEnumerator CountdownRoutine(int time)
        {
            do
            {
                TimerEvents.OnTimerCount?.Invoke(time--);
                yield return new WaitForSeconds(1);
            } while (time > 0);

            TimerEvents.OnTimerCount?.Invoke(time);
            StopTimer();
        }
    }
}