namespace ProjectH.Features.Levels
{
    using ProjectH.Features.Levels.Data;

    public static class LevelManager
    {
        public static LevelData SelectedLevel { get; private set; }

        public static void SelectLevel(LevelData levelData)
        {
            SelectedLevel = levelData;
        }
    }
}