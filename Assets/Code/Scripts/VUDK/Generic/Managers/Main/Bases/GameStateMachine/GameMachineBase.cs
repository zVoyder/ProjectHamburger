namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Patterns.StateMachine;

    [DefaultExecutionOrder(-990)]
    public abstract class GameMachineBase : StateMachine
    {
        public override void Init()
        {
#if UNITY_EDITOR
            DebugExtension.Advise("GameStateMachine initialized.");
#endif
            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnGameMachineStart);
        }
    }
}
