namespace ProjectH.Features.Moves.Undo
{
    using System.Collections.Generic;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;
    using ProjectH.Constants;
    using ProjectH.Managers.Main;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;

    public static class UndoController
    {
        private static GameManager s_gameManager => MainManager.Ins.GameManager as GameManager;
        private static PiecesMovesGraphicsController s_animController => s_gameManager.AnimationController;

        private static readonly Stack<UndoMove> s_undoMoves = new Stack<UndoMove>();

        static UndoController()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameBegin, Clear);
        }

        /// <summary>
        /// Add undo move to stack list.
        /// </summary>
        public static void AddUndoMove(UndoMove undoMove)
        {
            s_undoMoves.Push(undoMove);
        }

        /// <summary>
        /// Undo last move.
        /// </summary>
        public static void Undo()
        {
            if (s_undoMoves.Count <= 0) return;
            if (!s_gameManager.GameMachine.IsState(GamePhaseKey.InputPhase)) return;

            UndoMove undoMove = s_undoMoves.Pop();
            s_animController.AnimateUndoMove(undoMove);
            EventManager.Ins.TriggerEvent(EventKeys.PieceEvents.OnUndoMove);
        }

        /// <summary>
        /// Clear undo moves stack.
        /// </summary>
        public static void Clear()
        {
            s_undoMoves.Clear();
        }
    }
}