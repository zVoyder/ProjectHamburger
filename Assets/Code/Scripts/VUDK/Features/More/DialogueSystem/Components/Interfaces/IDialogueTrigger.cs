namespace VUDK.Features.More.DialogueSystem.Components.Interfaces
{
    using VUDK.Features.Main.TriggerSystem;

    public interface IDialogueTrigger : ITrigger
    {
        public void Interrupt();
    }
}