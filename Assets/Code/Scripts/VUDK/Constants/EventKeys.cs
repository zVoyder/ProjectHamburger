namespace VUDK.Constants
{
    public static class EventKeys
    {
        public static class GameEvents
        {
            public const string OnGameMachineStart = "OnGameMachineStart";
            public const string OnGameOpened = "OnGameOpened";
            public const string OnFirstLaunch = "OnFirstLaunch";
        }

        public static class TimerEvents
        {
            public const string OnTimerStart = "OnTimerStart";
            public const string OnTimerEnd = "OnTimerEnd";
            public const string OnTimerCount = "OnTimerCount";
        }

        public static class ScoreEvents
        {
            public const string OnScoreChange = "OnScoreChange";
            public const string OnHighScoreChange = "OnHighScoreChange";
        }

        public static class PauseEvents
        {
            public const string OnPauseEnter = "OnPauseEnter";
            public const string OnPauseExit = "OnPuaseExit";
        }

        public static class SceneEvents
        {
            public const string OnMainMenuLoaded = "OnMainMenuLoaded";
            public const string OnBeforeChangeScene = "OnBeforeChangeScene";
        }

        public static class EntityEvents
        {
            public const string OnEntityInit = "OnEntityInit";
            public const string OnEntityTakeDamage = "OnEntityTakeDamage";
            public const string OnEntityHeal = "OnEntityHeal";
            public const string OnEntityDeath = "OnEntityDeath";
        }

        public static class UIEvents 
        {
            public const string OnButtonPressed = "OnButtonClick";
        }

        #region Legacy
        [System.Obsolete("Use the new Dialogue System instead.")]
        public static class DialogueEvents
        {
            public const string OnDialougeTypedLetter = "OnDialougeTypedLetter";
            public const string OnStartDialouge = "OnTriggeredDialouge";
            public const string OnInterruptDialogue = "OnInterruptDialogue";
            public const string OnStartDialogue = "OnStartDialogue";
            public const string OnEndDialogue = "OnEndDialogue";
        }
        #endregion
    }
}