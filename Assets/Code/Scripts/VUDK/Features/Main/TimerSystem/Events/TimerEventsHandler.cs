namespace VUDK.Features.Main.TimerSystem.Events
{
    public static class TimerEventsHandler
    {
        public static void StartTimerHandler(int seconds)
        {
            TimerEvents.StartTimerHandler?.Invoke(seconds);
        }

        public static void StopTimerHandler()
        {
            TimerEvents.StopTimerHandler?.Invoke();
        }
    }
}
