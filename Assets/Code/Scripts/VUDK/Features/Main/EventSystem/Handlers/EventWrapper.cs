namespace ProjectH.Assets.Code.Scripts.VUDK.Features.Main.EventSystem.Handlers
{
    using System;

    public class EventWrapper<T> where T : EventArgs
    {
        private event EventHandler<T> _eventHandler;

        public event EventHandler<T> EventHandler
        {
            add => _eventHandler += value;
            remove => _eventHandler -= value;
        }

        public void Trigger(T args)
        {
            _eventHandler?.Invoke(this, args);
        }
    }
}