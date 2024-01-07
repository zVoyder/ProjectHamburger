namespace VUDK.Features.Main.SaveSystem.Utility
{
    using System.IO;
    using UnityEngine;

    public static class SaveUtility
    {
        public static string[] GetFilePaths(string extension)
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*" + extension);
            return files;
        }

        public static string[] GetFileNames(string extension, bool hasExtension = false)
        {
            string[] files = GetFilePaths(extension);
            for (int i = 0; i < files.Length; i++)
                files[i] = hasExtension ? Path.GetFileName(files[i]) : Path.GetFileNameWithoutExtension(files[i]);

            return files;
        }
    }
}