using System;
using System.IO;

namespace SDLog
{
    public class LogConfiguration
    {
        public string DateTimeFormat { get; set; } = "yyyy/m/d hh:MM:ss";
        public bool IsDebugEnabled { get; set; } = false;

        public string LogFilePath { get; set; } = @"Logs\app.log";
    }

    public class LogConfigurationManager
    {
        public const string ConfigFileName = "SDLog.json";
        public static string ConfigFileDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public static string ConfigFilePath => Path.Combine(ConfigFileDirectory, ConfigFileName);

        public static LogConfiguration GetLogConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                InitConfiguration();
                return new LogConfiguration();
            }
            else
            {
                LogConfiguration config = new LogConfiguration();
                try
                {
                    config = JsonConverter.DeserializeFromFile<LogConfiguration>(ConfigFilePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return config;
            }
        }

        public static void InitConfiguration()
        {
            var config = new LogConfiguration();
            JsonConverter.SerializeToFile(config, ConfigFilePath);
        }
    }
}