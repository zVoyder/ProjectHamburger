namespace ProjectH.Features.Victory
{
    using ProjectH.Constants;
    using ProjectH.Managers.Main;
    using System;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Serializable;

    public class VictoryAnimationController : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField]
        private Plate _plate;
        [SerializeField]
        private Vector3 _cameraOffsetPosition;
        [SerializeField]
        private Vector3 _plateOffsetPosition;
        [SerializeField]
        private DelayTask _moveTask;

        private Transform _target;
        private CameraFade _cameraFade;
        private Vector3 _originalCameraPosition;
        private Quaternion _originalCameraRotation;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private Camera Camera => MainManager.Ins.GameStats.MainCamera;

        private void Awake()
        {
            _originalCameraPosition = Camera.transform.position;
            _originalCameraRotation = Camera.transform.rotation;
            Camera.TryGetComponent(out _cameraFade);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameVictory, AnimateVictory);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnNextLevel, FadeAndReset);
            _moveTask.OnTaskCompleted += OnMoveTaskCompleted;
            _cameraFade.OnFadeInEnd += OnLevelCompletedFade;
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnGameVictory, AnimateVictory);
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnNextLevel, FadeAndReset);
            _moveTask.OnTaskCompleted -= OnMoveTaskCompleted;
            _cameraFade.OnFadeInEnd -= OnLevelCompletedFade;
        }

        private void Update()
        {
            if (!_moveTask.Process()) return;

            _plate.transform.position = Vector3.Lerp(_plate.OriginalPosition, _target.position + _plateOffsetPosition, _moveTask.ElapsedPercentPrecise);
            Camera.transform.position = Vector3.Lerp(_originalCameraPosition, _target.position + _cameraOffsetPosition, _moveTask.ElapsedPercentPrecise);
            Camera.transform.LookAtLerp(_target, _moveTask.ElapsedPercentNormalized);
        }

        private void AnimateVictory()
        {
            _target = GameManager.LevelManager.CurrentPieces[0].CurrentTile.BottomPiece.transform;
            _moveTask.Start();
        }

        private void OnMoveTaskCompleted()
        {
            _plate.StartRotating();
            _target.transform.SetParent(_plate.transform);
            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnLevelCompleted);
        }

        private void FadeAndReset()
        {
            _cameraFade.DoFadeInOut();
        }

        private void OnLevelCompletedFade()
        {
            _plate.StopRotating();
            _plate.ResetPosition();
            Camera.transform.position = _originalCameraPosition;
            Camera.transform.rotation = _originalCameraRotation;
            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnLevelCompletedFade);
        }
    }
}