namespace VUDK.Features.Main.SaveSystem.Serializers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.SaveData;

    public static class BinarySave
    {
        private const string DefaultExtension = ".bin";

        public static void Save(SavePacketData data, string fileName, string extension = DefaultExtension)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static bool TryLoad(out SavePacketData data, string fileName, string extension = DefaultExtension)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());

            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = (SavePacketData)formatter.Deserialize(stream);
                return true;
            }
        }
    }
}
