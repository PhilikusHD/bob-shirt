using System;
using System.Text;

namespace Bob.Core.Logging
{
    public class Formatter
    {
        public static string Format(LogLevel logLevel, string message)
        {
            string time = GetCurrentTime();

            StringBuilder stringBuilderNoColor = new();
            stringBuilderNoColor.Append("[" + time + "] ");
            stringBuilderNoColor.Append("[" + LogLevelToString(logLevel) + "] ");
            stringBuilderNoColor.AppendLine(message);

            return stringBuilderNoColor.ToString();
        }

        private static string GetCurrentTime()
        {
            return DateTime.Now.ToString();
        }

        private static string LogLevelToString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.DEBUG => "DEBUG",
                LogLevel.INFO => "INFO",
                LogLevel.WARNING => "WARNING",
                LogLevel.ERROR => "ERROR",
                LogLevel.CRITICAL => "CRITICAL",
                _ => "UNKNOWN",
            };
        }
    }
}
