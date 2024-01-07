namespace VUDK.Features.Main.SaveSystem.SaveData
{
    using System.Collections.Generic;

    [System.Serializable]
    public class SaveDict : SaveValue
    {
        public Dictionary<int, SaveValue> Dict;

        public SaveDict()
        {
            Dict = new Dictionary<int, SaveValue>();
        }

        public SaveDict(Dictionary<int, SaveValue> saves)
        {
            Dict = saves;
        }
    }
}