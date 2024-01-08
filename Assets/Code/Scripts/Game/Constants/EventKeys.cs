namespace ProjectH.Constants
{
    public static class EventKeys
    {
        public static class PieceEvents
        {
            public const string OnMoveAnimationStarted = "OnMoveAnimationStarted";
            public const string OnMoveAnimationCompleted = "OnMoveCompleted";
            public const string OnUndoMove = "OnUndoMove";
        }

        public static class GameEvents
        {
            public const string OnGameVictory = "OnGameVictory";
            public const string OnGameBegin = "OnGameBegin";
            public const string OnEatPhase = "OnEatPhase";
            public const string OnEatPhaseFadeInStart = "OnEatPhaseFadeCompletely";
            public const string OnEatPhaseFadeInEnd = "OnEatPhaseFadeCompletely";
            public const string OnEatTapped = "OnEatTapped";
            public const string OnNextLevelTriggered = "OnGoNextLevel";
            public const string OnResetLevel = "OnResetLevel";
        }
    }
}
