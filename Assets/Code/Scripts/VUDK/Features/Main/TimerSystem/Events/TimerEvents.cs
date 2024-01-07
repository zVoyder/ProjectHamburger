namespace VUDK.Features.Main.TimerSystem.Events
{
    using System;

    public static class TimerEvents
    {
        public static Action OnTimerStart;
        public static Action OnTimerStop;
        public static Action<int> OnTimerCount;

        public static Action<int> StartTimerHandler;
        public static Action StopTimerHandler;
    }
}