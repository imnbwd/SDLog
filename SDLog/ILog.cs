namespace SDLog
{
    public interface ILog
    {
        LogConfiguration Configuration { get; set; }

        void Log(string message, LogLevel logLevel);

        void LogError(string errorMessage);
    }
}