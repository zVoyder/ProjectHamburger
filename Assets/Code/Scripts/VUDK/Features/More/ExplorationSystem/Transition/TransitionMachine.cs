namespace VUDK.Features.More.ExplorationSystem.Transition
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.StateMachine;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.More.ExplorationSystem.Transition.Types;
    using VUDK.Features.More.ExplorationSystem.Factory;
    using VUDK.Features.More.ExplorationSystem.Managers;

    public class TransitionMachine : StateMachine, IInit<ExplorationManager>
    {
        [Header("Transition Settings")]
        [SerializeField]
        private TransitionType _transitionType;

        [SerializeField]
        private DelayTask _timeProcess;
        [SerializeField]
        private CameraFovChanger _fovChanger;

        private ExplorationManager _explorationManager;
        private TransitionType _defaultTransition;
        private TransitionType _currentTransitionType;

        #region Transitions Instances
        private TransitionContext _context;
        private TransitionInstant _transitionInstant;
        private TransitionLinear _transitionLinear;
        private TransitionFov _transitionFov;
        #endregion

        /// <inheritdoc/>
        public virtual void Init(ExplorationManager explorationManager)
        {
            _explorationManager = explorationManager;
            Init();
        }

        /// <inheritdoc/>
        public override void Init()
        {
            if(!Check()) return;
            _defaultTransition = _transitionType;

            _context = ExplorationFactory.Create(_explorationManager);

            TransitionBegin beginPhase = ExplorationFactory.Create(TransitionStateKey.Start, this, _context) as TransitionBegin;
            TransitionProcess processPhase = ExplorationFactory.Create(TransitionStateKey.Process, this, _context) as TransitionProcess;
            TransitionEnd endPhase = ExplorationFactory.Create(TransitionStateKey.End, this, _context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
            SetTransition(_defaultTransition);
        }

        /// <inheritdoc/>
        public override bool Check()
        {
            return _explorationManager != null;
        }

        /// <summary>
        /// Resets the transition to the default transition.
        /// </summary>
        public void ResetToDefaultTransition()
        {
            ChangeTransitionType(_defaultTransition);
        }

        /// <summary>
        /// Changes the default transition.
        /// </summary>
        /// <param name="transitionType">New default transition.</param>
        public void ChangeDefaultTransition(TransitionType transitionType)
        {
            _defaultTransition = transitionType;
        }

        /// <summary>
        /// Changes the current transition.
        /// </summary>
        /// <param name="transitionType">New transition.</param>
        public void ChangeTransitionType(TransitionType transitionType)
        {
            if(_currentTransitionType == transitionType) return;
            SetTransition(transitionType);
        }

        /// <summary>
        /// Sets the current transition.
        /// </summary>
        /// <param name="transitionType">New transition.</param>
        private void SetTransition(TransitionType transitionType)
        {
            _currentTransitionType = transitionType;
            TransitionBase transition = transitionType switch
            {
                TransitionType.Instant => _transitionInstant ??= ExplorationFactory.Create(_context),
                TransitionType.Linear => _transitionLinear ??= ExplorationFactory.Create(_context, _timeProcess),
                TransitionType.Fov => _transitionFov ??= ExplorationFactory.Create(_context, _fovChanger, _timeProcess),
                _ => null,
            };

            _explorationManager.ChangeTransition(transition);
        }
    }
}
