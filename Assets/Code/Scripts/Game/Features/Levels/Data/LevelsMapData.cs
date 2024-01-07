namespace ProjectH.Features.Levels.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "LevelsMapData", menuName = "Game/Levels/LevelsMap")]
    public class LevelsMapData : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}