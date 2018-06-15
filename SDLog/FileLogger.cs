using System;
using System.IO;

namespace SDLog
{
    public class FileLogger : ILog
    {
        public FileLogger()
        {
            try
            {
                Configuration = LogConfigurationManager.GetLogConfig();

                var subdir = Path.GetDirectoryName(Configuration.LogFilePath);
                if (!string.IsNullOrWhiteSpace(subdir))
                {
                    var logdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subdir);
                    if (!Directory.Exists(logdir))
                    {
                        Directory.CreateDirectory(logdir);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LogConfiguration Configuration { get; set; }
        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration?.LogFilePath);

        public void Log(string message, LogLevel logLevel)
        {
            File.AppendAllText(FilePath, $"[{logLevel.ToString().ToUpper()}] {DateTime.Now} {message}" + Environment.NewLine);
        }

        public void LogError(string errorMessage)
        {
            Log($"{DateTime.Now} {errorMessage}", LogLevel.Error);
        }
    }
}