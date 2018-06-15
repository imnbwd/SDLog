using System;

namespace SDLog
{
    public static class ILogExtensions
    {
        public static void Log<T>(this ILog logger, string message)
        {
            logger?.Log(GenerateCategoriedMessage<T>(message), LogLevel.Debug);
        }

        public static void Log(this ILog logger, string message)
        {
            logger.Log(message, LogLevel.Info);
        }

        public static void LogDebug(this ILog logger, string message)
        {
            // if (ClientHelper.Instance.IsDebugLogOutputEnabled())
            {
                logger?.Log($"{message}", LogLevel.Debug);
            }
        }

        public static void LogDebug<T>(this ILog logger, string message)
        {
            // if (ClientHelper.Instance.IsDebugLogOutputEnabled())
            {
                logger?.Log($"[{typeof(T).Name}] {message}", LogLevel.Debug);
            }
        }

        public static void LogError(this ILog logger, Exception ex)
        {
            logger?.LogError(ex.ToString());
        }

        private static string GenerateCategoriedMessage<T>(string message) => $"[{typeof(T).Name}] {message}";
    }
}