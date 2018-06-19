namespace SDLog
{
    public interface ILog
    {   

        void Log(string message, LogLevel logLevel);

        void LogError(string errorMessage);
    }
}