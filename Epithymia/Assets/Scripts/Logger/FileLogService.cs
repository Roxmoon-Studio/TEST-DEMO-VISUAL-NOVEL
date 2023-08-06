using System;
using System.IO;
using UnityEngine;

namespace Logger
{
    public class FileLogService : ILogService, IDisposable
    {
        private bool _isInited;
        private string _fileName;
        private string _directoryPath;
        private string _fullPath;
        private StreamWriter _streamWriter;

        public void Log(object message)
        {
            if (!_isInited)
                Init();

            _streamWriter.WriteLine(message);
        }

        public void LogWarning(object message)
        {
            if (!_isInited)
                Init();

            _streamWriter.WriteLine($"[WARNING]: {message}");
        }

        public void LogError(object message)
        {
            if (!_isInited)
                Init();

            _streamWriter.WriteLine($"[ERROR]: {message}");
        }

        private void Init()
        {
            _fileName = DateTime.Now + ".log";
            _directoryPath = Application.persistentDataPath;
            _fullPath = Path.Combine(_directoryPath, _fileName);
            _streamWriter = new StreamWriter(_fullPath, append: true);
        }

        void IDisposable.Dispose() =>
            _streamWriter.Dispose();
    }
}
