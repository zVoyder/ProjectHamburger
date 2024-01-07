namespace ProjectH.Constants
{
    public static class EventKeys
    {
        public static class PieceEvents
        {
            public const string OnMoveStart = "OnMoveStart";
            public const string OnMoveAnimationCompleted = "OnMoveCompleted";
            public const string OnUndoMove = "OnUndoMove";
        }

        public static class GameEvents
        {
            public const string OnGameVictory = "OnGameVictory";
            public const string OnGameBegin = "OnGameBegin";
            public const string OnLevelCompleted = "OnLevelCompleted";
            public const string OnLevelCompletedFade = "OnLevelCompletedFade";
            public const string OnNextLevel = "OnGoNextLevel";
            public const string OnResetLevel = "OnResetLevel";
        }
    }
}
