namespace ProjectH.Features.Levels
{
    using UnityEngine;
    using ProjectH.Features.Levels.Data;
    using System.Collections.Generic;
    using ProjectH.Features.Grid.Pieces;
    using VUDK.Features.Main.EventSystem;
    using ProjectH.Constants;
    using VUDK.Patterns.Initialization.Interfaces;
    using System;
    using ProjectH.Managers.Main;

    public class LevelManager : MonoBehaviour, IInit<GameManager>
    {
        [Header("Levels")]
        [SerializeField]
        private LevelsMapData _levelsMapData;

        private GameManager _gameManager;

        public bool IsLoadingLevel => _gameManager.VictoryAnimationController.CameraFade.IsFading;

        public int CurrentLevelIndex { get; private set; }
        public List<Piece> PiecesOnField { get; private set; }
        public int PiecesCount => PiecesOnField.Count;

        private void Awake()
        {
            PiecesOnField = new List<Piece>();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameBegin, ClearPieces);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnGameBegin, ClearPieces);
        }

        /// <inheritdoc/>
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <inheritdoc/>
        public bool Check()
        {
            return _gameManager != null;
        }

        /// <summary>
        /// Get current level data.
        /// </summary>
        /// <returns> Current level data. </returns>
        public LevelData GetCurrentLevelData()
        {
            return _levelsMapData.Levels[CurrentLevelIndex];
        }

        /// <summary>
        /// Set current level index to next level.
        /// </summary>
        public void SetCurrentLevelIndexToNextLevel()
        {
            if (CurrentLevelIndex >= _levelsMapData.Levels.Count - 1)
            {
                CurrentLevelIndex = 0;
                return;
            }

            CurrentLevelIndex++;
        }

        /// <summary>
        /// Adds a piece to the pieces on field list.
        /// </summary>
        /// <param name="piece"> Piece to add. </param>
        public void AddPieceToField(Piece piece)
        {
            PiecesOnField.Add(piece);
        }

        /// <summary>
        /// Removes a piece from the pieces on field list.
        /// </summary>
        /// <param name="piece"> Piece to remove. </param>
        public void RemovePieceFromField(Piece piece)
        {
            PiecesOnField.Remove(piece);
        }

        /// <summary>
        /// Clear pieces on field list.
        /// </summary>
        public void ClearPieces()
        {
            foreach (Piece piece in PiecesOnField)
                piece.Dispose();

            PiecesOnField.Clear();
        }
    }
}