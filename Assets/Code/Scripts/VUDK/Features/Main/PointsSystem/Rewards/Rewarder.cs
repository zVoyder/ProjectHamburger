namespace VUDK.Features.Main.PointsSystem.Rewards
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Events;

    public class Rewarder : MonoBehaviour
    {
        public void SendReward(int points)
        {
            PointsEvents.ModifyPointsHandler?.Invoke(points);
        }
    }
}