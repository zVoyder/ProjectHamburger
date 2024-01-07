namespace VUDK.Features.Main.PointsSystem.Data
{
    using VUDK.Features.Main.SaveSystem.SaveData;

    [System.Serializable]
    public class PointsSaveValue : SaveValue
    {
        public int Points;

        public PointsSaveValue() : base()
        {
        }
    }
}