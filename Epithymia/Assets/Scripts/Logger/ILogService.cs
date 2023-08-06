namespace Logger {
    public interface ILogService
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
    }
}
