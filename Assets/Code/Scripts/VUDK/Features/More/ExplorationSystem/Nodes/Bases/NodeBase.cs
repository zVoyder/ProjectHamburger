namespace VUDK.Features.More.ExplorationSystem.Nodes
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InteractSystem.Interfaces;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.More.ExplorationSystem.Explorers;
    using VUDK.Features.More.ExplorationSystem.Managers;
    using VUDK.Features.More.ExplorationSystem.Nodes.Components;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class NodeBase : MonoBehaviour, IInteractable, IInit<ExplorationManager>
    {
        [field: Header("Node Settings")]
        [field: SerializeField]
        protected NodeTarget NodeTarget { get; private set; }

        [field: Header("Transition Settings")]
        [field: SerializeField]
        protected bool HasCustomTransition { get; private set; }

        [field: SerializeField]
        protected TransitionType CustomTransition { get; private set; }

        [Header("Node Events")]
        [SerializeField]
        private UnityEvent _onEnter;

        [SerializeField]
        private UnityEvent _onExit;

        protected ExplorationManager ExplorationManager { get; private set; }

        protected PathExplorer PathExplorer => ExplorationManager.PathExplorer;
        public Quaternion NodeRotation => NodeTarget.transform.rotation;
        public Vector3 NodePosition => NodeTarget.transform.position;

        private void OnValidate()
        {
            if (!NodeTarget)
            {
                NodeTarget = GetComponentInChildren<NodeTarget>();
                if (!NodeTarget)
                    Debug.LogError($"NodeTarget not found in {gameObject.name}");
            }
        }

        protected virtual void Awake()
        {
            Disable();
        }

        protected virtual void OnEnable()
        {
            EventManager.Ins.AddListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        protected virtual void OnDisable()
        {
            EventManager.Ins.RemoveListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        /// <inheritdoc/>
        public virtual void Init(ExplorationManager explorationManager)
        {
            ExplorationManager = explorationManager;
        }

        /// <inheritdoc/>
        public virtual bool Check()
        {
            return ExplorationManager != null;
        }

        /// <summary>
        /// Interacts with this node.
        /// </summary>
        public virtual void Interact()
        {
            CheckCustomTransition();
            GoToThisNode();
            OnNodeChangedHandler();
            Disable();
        }

        /// <summary>
        /// Interacts with this node as the first node.
        /// </summary>
        /// <param name="transitionType">Transtion Type.</param>
        public virtual void OnFirstNode(TransitionType transitionType)
        {
            PathExplorer.ChangeTransitionType(transitionType);
            GoToThisNode();
            OnNodeChangedHandler();
            Disable();
        }

        /// <summary>
        /// Enables the node.
        /// </summary>
        public abstract void Enable();

        /// <summary>
        /// Disables the node.
        /// </summary>
        public abstract void Disable();

        /// <summary>
        /// Called when the player enters the node.
        /// </summary>
        public virtual void OnNodeEnter()
        {
            _onEnter?.Invoke();
        }

        /// <summary>
        /// Called when the player exits the node.
        /// </summary>
        public virtual void OnNodeExit()
        {
            _onExit?.Invoke();
        }

        /// <summary>
        /// Called when the node changes.
        /// </summary>
        protected void OnNodeChangedHandler()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnChangedNode);
        }

        /// <summary>
        /// Goes to this node.
        /// </summary>
        protected void GoToThisNode()
        {
            ExplorationManager.GoToNode(this);
        }

        /// <summary>
        /// Checks if the node has a custom transition.
        /// </summary>
        protected void CheckCustomTransition()
        {
            if (HasCustomTransition)
                PathExplorer.ChangeTransitionType(CustomTransition);
            else
                PathExplorer.ResetToDefaultTransition();
        }

#if UNITY_EDITOR

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            DrawLabel();
        }

        protected virtual void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, $"-Node");
        }

        protected bool IsNodeSelectedInScene()
        {
            return
                UnityEditor.Selection.activeGameObject == gameObject ||
                UnityEditor.Selection.activeObject == NodeTarget.gameObject;
        }
#endif
    }
}