using System.IO;
using UnityEngine;

namespace Services.SaveLoad
{
    public class FilePath
    {
        private const string SAVE_EXTENSION = ".epithymia";
        private const string SETTINGS_EXTENSION = ".json";

        public string GameSavePath => Application.persistentDataPath + "/Saves";
        public string SettingsPath => Application.persistentDataPath + "/Settings";

        public string FullPath(string fileName, SaveType saveType)
        {
            string fullPath;
            
            if (saveType == SaveType.Game) 
                fullPath = GameSavePath + "/" + fileName + SAVE_EXTENSION;
            else
                fullPath = SettingsPath + "/" + fileName + SETTINGS_EXTENSION;
            
            return fullPath;
        }

        public void CreateDirectoryIfNotExist(SaveType saveType)
        {
            string path;

            if (saveType == SaveType.Game) 
                path = GameSavePath;
            else
                path = SettingsPath;
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
