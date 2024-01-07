namespace ProjectH.Features.Moves.Undo
{
    using ProjectH.Constants;
    using ProjectH.Managers.Main;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using System.Collections.Generic;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;

    public static class UndoController
    {
        private static GameManager s_gameManager => MainManager.Ins.GameManager as GameManager;
        private static AnimationController s_animController => s_gameManager.AnimationController;

        private static readonly Stack<UndoMove> s_undoMoves = new Stack<UndoMove>();

        public static void AddUndoMove(UndoMove undoMove)
        {
            s_undoMoves.Push(undoMove);
        }

        public static void Undo()
        {
            if (s_undoMoves.Count <= 0) return;
            if (!s_gameManager.GameMachine.IsState(GamePhaseKey.InputPhase)) return;

            UndoMove undoMove = s_undoMoves.Pop();
            s_animController.AnimateUndoMove(undoMove);
            EventManager.Ins.TriggerEvent(EventKeys.PieceEvents.OnUndoMove);
        }

        public static void Clear()
        {
            s_undoMoves.Clear();
        }
    }
}