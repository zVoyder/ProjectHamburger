namespace VUDK.Features.Main.PointsSystem.Debug
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Events;

    public class DebugPointsModifier : MonoBehaviour
    {
        public int PointsModifyValue;

        [ContextMenu("Modify Points")]
        public void ModifyPoints()
        {
            PointsEvents.ModifyPointsHandler.Invoke(PointsModifyValue);
        }
    }
}