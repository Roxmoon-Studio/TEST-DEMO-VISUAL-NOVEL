using System.IO;
using Newtonsoft.Json;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly FilePath _filePath = new();

        public void Save<T>(T save, string name, SaveType saveType = SaveType.Game) where T : new()
        {
            _filePath.CreateDirectoryIfNotExist(saveType);
            SerializeSave(save, _filePath.FullPath(name, saveType));
        }

        public T Load<T>(string name, SaveType saveType = SaveType.Game) where T : new()
        {
            _filePath.CreateDirectoryIfNotExist(saveType);

            return DeserializeSave<T>(_filePath.FullPath(name, saveType));
        }

        private void SerializeSave<T>(T save, string fullPath) where T : new()
        {
            var serializedObject = JsonConvert.SerializeObject(save);
            File.WriteAllText(fullPath, serializedObject);
        }

        private T DeserializeSave<T>(string fullPath) where T : new()
        {
            if (!File.Exists(fullPath))
                return new();

            var serializedObject = File.ReadAllText(fullPath);
            var obj = JsonConvert.DeserializeObject<T>(serializedObject);
            
            return obj;
        }
    }
}
