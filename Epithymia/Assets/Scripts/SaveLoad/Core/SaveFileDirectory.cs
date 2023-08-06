using System.IO;
using System.Linq;

namespace Services.SaveLoad
{
    public class SaveFileDirectory : ISaveFileDirectory
    {
        private readonly string _savePath;

        public SaveFileDirectory(string savePath)
        {
            _savePath = savePath;
        }

        public string[] GetSaveFiles()
        {
            if (!Directory.Exists(_savePath))
                return new string[0];

            string[] fullFileNames = Directory.GetFiles(_savePath);

            return fullFileNames.Select(Path.GetFileNameWithoutExtension).ToArray();
        }
    }
}
