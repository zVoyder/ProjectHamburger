namespace VUDK.Features.Main.PointsSystem
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Data;
    using VUDK.Features.Main.PointsSystem.Events;
    using VUDK.Features.Main.SaveSystem.Bases;
    using VUDK.Generic.Serializable;

    public class PointsManager : SaverBase<PointsSaveValue>
    {
        [Header("Points Settings")]
        [SerializeField, Min(0)]
        private int _initPoints;
        [SerializeField]
        private Range<int> _pointsLimits;

        public int Points
        {
            get
            {
                return SaveValue.Points;
            }
            set
            {
                SaveValue.Points = value;
            }
        }

        private void OnEnable()
        {
            PointsEvents.ModifyPointsHandler += ModifyPoints;
        }

        private void OnDisable()
        {
            PointsEvents.ModifyPointsHandler -= ModifyPoints;
        }

        /// <inheritdoc/>
        public override void Init()
        {
            PointsEvents.OnPointsInit?.Invoke(Points);
        }

        /// <summary>
        /// Modifies the points.
        /// </summary>
        /// <param name="pointsToModify">Points to modify.</param>
        public void ModifyPoints(int pointsToModify)
        {
            int modifiedPoints = Mathf.Clamp(Points + pointsToModify, _pointsLimits.Min, _pointsLimits.Max) - Points;

            Points += modifiedPoints;
            PointsEvents.OnPointsChanged?.Invoke(this, modifiedPoints);
            Push();
        }

        /// <inheritdoc/>
        public override string GetSaveName()
        {
            return "Points";
        }
    }
}