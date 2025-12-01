using System;
using System.Collections.Generic;
using System.Threading;

namespace Bob.Core.Logging
{
    public class Logger
    {
        private static readonly List<object> m_Handlers = [];
        private static LogLevel m_MinLogLevel = LogLevel.DEBUG;
        private static readonly Lock m_Mutex = new();

        public static void InitLog()
        {
            AddHandler(new ConsoleHandler());
            AddHandler(new FileHandler("Charles.log"));
        }

        public static void SetLogLevel(LogLevel level) => m_MinLogLevel = level;

        public static void AddHandler(object handler)
        {
            if (handler is ConsoleHandler or FileHandler)
            {
                lock (m_Mutex)
                {
                    m_Handlers.Add(handler);
                }
            }
            else
            {
                throw new ArgumentException("Invalid handler type.");
            }
        }

        public static void Debug(string message, params object[] args) => Log(LogLevel.DEBUG, message, args);
        public static void Info(string message, params object[] args) => Log(LogLevel.INFO, message, args);
        public static void Warning(string message, params object[] args) => Log(LogLevel.WARNING, message, args);
        public static void Error(string message, params object[] args) => Log(LogLevel.ERROR, message, args);
        public static void Critical(string message, params object[] args) => Log(LogLevel.CRITICAL, message, args);

        private static void Log(LogLevel level, string message, params object[] args)
        {
            if (level < m_MinLogLevel)
                return;

            lock (m_Mutex)
            {
                string formattedMessage = string.Format(message, args);
                string msgNoColor = Formatter.Format(level, formattedMessage);

                foreach (var handler in m_Handlers)
                {
                    if (handler is ConsoleHandler)
                        ConsoleHandler.WriteImpl(msgNoColor);
                    else if (handler is FileHandler file)
                        file.WriteImpl(msgNoColor);
                }
            }
        }
    }
}
