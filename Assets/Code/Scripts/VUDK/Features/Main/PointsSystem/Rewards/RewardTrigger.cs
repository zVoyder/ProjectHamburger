namespace VUDK.Features.Main.PointsSystem.Rewards
{
    using UnityEngine;

    public class RewardTrigger : Rewarder
    {
        [Header("Reward Settings")]
        [SerializeField]
        private bool _isOnce = true;
        [SerializeField]
        private int _rewardPoints;

        private bool _isObtained;

        public void TriggerReward()
        {
            if (_isOnce && _isObtained) return;
            _isObtained = true;
            SendReward(_rewardPoints);
        }
    }
}