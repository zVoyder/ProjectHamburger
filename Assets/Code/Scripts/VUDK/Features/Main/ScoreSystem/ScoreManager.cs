namespace VUDK.Features.Main.ScoreSystem
{
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    public class ScoreManager : MonoBehaviour
    {
        public int ScoreValue { get; private set; }
        public int HighScore => PlayerPrefs.GetInt(_scorePref);
        private string _scorePref => Constants.Prefs.HighScore;

        private void Start()
        {
            EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnScoreChange, ScoreValue);
            EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnHighScoreChange, HighScore);
        }

        public void ChangeScore(int scoreToAdd)
        {
            ScoreValue += scoreToAdd;

            if(ScoreValue < 0)
                ScoreValue = 0;

            SaveHighScore();
        }

        private void SaveHighScore()
        {
            if (ScoreValue > HighScore)
            {
                PlayerPrefs.SetInt(_scorePref, ScoreValue);
                EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnHighScoreChange, HighScore);
            }
        }
    }
}

