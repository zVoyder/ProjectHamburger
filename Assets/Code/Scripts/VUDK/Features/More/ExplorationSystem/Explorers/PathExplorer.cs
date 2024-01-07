namespace VUDK.Features.More.ExplorationSystem.Explorers
{
    using VUDK.Features.More.ExplorationSystem.Transition;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;

    public class PathExplorer : TransitionMachine
    {
        /// <summary>
        /// Start the transition state machine.
        /// </summary>
        public void TransitionStart()
        {
            ChangeState(TransitionStateKey.Start);
        }
    }
}
