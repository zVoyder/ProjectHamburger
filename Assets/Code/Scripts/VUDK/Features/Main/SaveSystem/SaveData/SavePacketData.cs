namespace VUDK.Features.Main.SaveSystem.SaveData
{
    [System.Serializable]
    public sealed class SavePacketData
    {
        public SaveValue Value;

        public SavePacketData() { }

        public SavePacketData(SaveValue saveValue)
        {
            Value = saveValue;
        }
    }
}
