using UnityEngine;

namespace Logger
{
    public class DebugLogService : ILogService
    {
        public void Log(object message) =>
            Debug.Log(message);

        public void LogWarning(object message) =>
            Debug.LogWarning(message);

        public void LogError(object message) =>
            Debug.LogError(message);
    }
}
