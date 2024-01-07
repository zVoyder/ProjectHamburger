namespace VUDK.Features.Main.PointsSystem.Events
{
    using System;

    public static class PointsEvents
    {
        // Event Handlers
        public static Action<int> ModifyPointsHandler;

        // Events
        public static Action<int> OnPointsInit;
        public static EventHandler<int> OnPointsChanged;
    }
}