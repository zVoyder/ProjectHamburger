namespace VUDK.Features.More.ExplorationSystem.Transition
{
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.More.ExplorationSystem.Transition.Types;
    using VUDK.Features.More.ExplorationSystem.Nodes;
    using VUDK.Features.More.ExplorationSystem.Managers;
    using VUDK.Features.More.ExplorationSystem.Explorers;

    public class TransitionContext : StateContext
    {
        public ExplorationManager ExplorationManager { get; private set; }

        public GameStats GameStats => MainManager.Ins.GameStats;
        public NodeBase TargetNode => ExplorationManager.CurrentTargetNode;
        public NodeBase PreviousNode => ExplorationManager.PreviousTargetNode;
        public TransitionBase Transition => ExplorationManager.CurrentTransition;
        public PathExplorer PathExplorer => ExplorationManager.PathExplorer;

        public TransitionContext(ExplorationManager explorationManager) : base()
        {
            ExplorationManager = explorationManager;
        }
    }
}
