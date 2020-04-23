using System;
using System.Threading.Tasks;
using DiscordBotTemplate.Logging;

namespace DiscordBotTemplate.Utilities
{
    public static class ApplicationHelper
    {
        public static void AnnounceAndExit()
        {
            Logger.LogInfo("Awaiting all log tasks...");
            Task.WhenAll(Logger.LogTasks).GetAwaiter().GetResult();
            Logger.LogInfo("Application closing safely...");
            Environment.Exit(0);
        }
    }
}
