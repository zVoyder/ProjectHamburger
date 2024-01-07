namespace VUDK.Features.More.ExplorationSystem.Managers
{
    using UnityEngine;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.More.ExplorationSystem.Nodes;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.More.ExplorationSystem.Transition.Types;
    using VUDK.Features.More.ExplorationSystem.Explorers;
    using VUDK.Patterns.Initialization;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;

    [DefaultExecutionOrder(-100)]
    public class ExplorationManager : Initializer
    {
        [Header("First Node Settings")]
        [SerializeField]
        private NodeBase _firstNode;
        [SerializeField]
        private bool _playOnAwake;
        [SerializeField]
        private TransitionType _firstTransitionType;

        [field: Header("Path Explorer")]
        [field: SerializeField]
        public PathExplorer PathExplorer { get; private set; }

        public NodeBase CurrentTargetNode { get; private set; }
        public NodeBase PreviousTargetNode { get; private set; }
        public TransitionBase CurrentTransition { get; private set; }

        protected virtual void OnValidate()
        {
            if (PathExplorer == null) PathExplorer = FindAnyObjectByType<PathExplorer>();
        }

        protected virtual void Awake()
        {
            if (_firstNode == null) Debug.LogError($"First Node in {nameof(ExplorationManager)} NOT Selected!");
        }

        protected virtual void OnEnable()
        {
            EventManager.Ins.AddListener(ExplorationEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
        }

        protected virtual void OnDisable()
        {
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
        }

        protected virtual void Start()
        {
            Init();
        }

        /// <summary>
        /// Begins the exploration.
        /// </summary>
        public void Begin()
        {
            CurrentTargetNode.OnFirstNode(_firstTransitionType);
        }

        /// <inheritdoc/>
        public override void Init()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExplorationManagerInit, this);
            PathExplorer.Init(this);
            InitFirstNode();
        }

        /// <inheritdoc/>
        public override bool Check()
        {
            return PathExplorer && _firstNode;
        }

        /// <summary>
        /// Changes the transition.
        /// </summary>
        /// <param name="transition">Transition to use.</param>
        public void ChangeTransition(TransitionBase transition)
        {
            if(transition == null) return;

            CurrentTransition = transition;
        }

        /// <summary>
        /// Changes the target node to the previous node.
        public void ChangeTargetToPrevious()
        {
            if (PreviousTargetNode == null) return;
            GoToNode(PreviousTargetNode);
        }

        /// <summary>
        /// Changes the target node.
        /// </summary>
        /// <param name="targetNode">Target node.</param>
        public void GoToNode(NodeBase targetNode)
        {
            if (PathExplorer.IsState(TransitionStateKey.Process)) return;
            PreviousTargetNode = CurrentTargetNode;
            CurrentTargetNode = targetNode;
            PathExplorer.TransitionStart();
        }

        /// <summary>
        /// Initializes the first node.
        /// </summary>
        private void InitFirstNode()
        {
            CurrentTargetNode = _firstNode;
            PreviousTargetNode = CurrentTargetNode;

            if (_playOnAwake) Begin();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_firstNode == null) return;

            UnityEditor.Handles.Label(_firstNode.NodePosition, "-Start");
        }
#endif
    }
}
