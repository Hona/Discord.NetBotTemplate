using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Logging
{
    public static class Logger
    {
        public static List<Task> LogTasks { get; set; } = new List<Task>();
        private static object _lock = new object();
        public static void LogInfo(string message) => LogToConsole(new LogMessage(LogType.Info, message));
        public static void LogWarning(string message) => LogToConsole(new LogMessage(LogType.Warning, message));
        public static void LogError(string message, Exception exception = null) => LogToConsole(new LogMessage(LogType.Error, message, exception));
        public static void LogException(Exception exception, string message = null) => LogToConsole(new LogMessage(LogType.Error, message ?? "No extra information.", exception));
        public static void LogToConsole(LogMessage logMessage)
        {
            // Fire and forget
            LogTasks.Add(Task.Run(() =>
            {
                lock (_lock)
                {
                    PrintSeverityPrefix(logMessage.Severity);
                    Console.WriteLine($" - {logMessage.Message}");

                    if (logMessage.HasException)
                    {
                        Console.WriteLine();
                        Console.WriteLine(logMessage.Exception.ToString());
                    }

                    LogTasks = LogTasks.Where(x => !x.IsCanceled && !x.IsCompleted && !x.IsCompletedSuccessfully && !x.IsFaulted).ToList();
                }
            }));

            
        }
        private static void PrintSeverityPrefix(LogType severity)
        {
            // Looks like '[Info]' but adds color to the inner text, and restore the old color
            Console.Write("[");
            var oldColor = Console.ForegroundColor;
            var severityColor = severity switch
            {
                LogType.Error => ConsoleColor.Red,
                LogType.Warning => ConsoleColor.DarkYellow,
                LogType.Info => ConsoleColor.DarkBlue,
                _ => throw new NotImplementedException("That log type doesn't exist"),
            };
            Console.ForegroundColor = severityColor;
            Console.Write(severity.ToString());
            Console.ForegroundColor = oldColor;
            Console.Write("]");
        }
    }
}
