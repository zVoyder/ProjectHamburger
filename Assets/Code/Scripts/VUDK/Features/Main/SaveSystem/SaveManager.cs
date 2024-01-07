namespace VUDK.Features.Main.SaveSystem
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.SaveData;
    using VUDK.Features.Main.SaveSystem.Serializers;
    using VUDK.Features.Main.SaveSystem.Utility;

    public static class SaveManager
    {
        private static Dictionary<string, SaveDict> s_fileSaves;

        static SaveManager()
        {
            s_fileSaves = new Dictionary<string, SaveDict>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Init()
        {
            Load();
        }

        public static void Save(string groupName)
        {
            SavePacketData dataToSave = new SavePacketData(s_fileSaves[groupName]);
            BinarySave.Save(dataToSave, groupName);
        }

        public static void Load()
        {
            string[] files = SaveUtility.GetFileNames(".bin");

            foreach (string fileName in files)
            {
                if (BinarySave.TryLoad(out SavePacketData saveData, fileName))
                {
                    SaveDict saveDict = saveData.Value as SaveDict;
                    s_fileSaves.Add(fileName, saveDict);
                }
            }
        }

        public static void Push(string groupName, int key, SavePacketData saveData)
        {
            if (!s_fileSaves.ContainsKey(groupName))
                s_fileSaves.Add(groupName, new SaveDict());

            if (s_fileSaves[groupName].Dict.ContainsKey(key))
                s_fileSaves[groupName].Dict[key] = saveData.Value;
            else
                s_fileSaves[groupName].Dict.Add(key, saveData.Value);

            Save(groupName);
        }

        public static bool TryPull<T>(string groupName, int key, out SavePacketData saveData) where T : SaveValue
        {
            if(s_fileSaves.ContainsKey(groupName))
            {
                if (s_fileSaves[groupName].Dict.ContainsKey(key))
                {
                    saveData = new SavePacketData();
                    saveData.Value = s_fileSaves[groupName].Dict[key] as T;
                    return true;
                }
            }

            saveData = null;
            return false;
        }

        public static bool DeleteSave(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + ".bin");

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public static bool DeleteAllSaves()
        {
            string[] files = SaveUtility.GetFileNames(".bin");

            foreach (string fileName in files)
                DeleteSave(fileName);

            return true;
        }
    }
}