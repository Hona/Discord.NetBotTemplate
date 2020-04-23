using System;
using DiscordBotTemplate.Logging;

namespace DiscordBotTemplate.Utilities
{
    public static class ApplicationHelper
    {
        public static void AnnounceAndExit()
        {
            Logger.LogInfo("Application closing safely...");
            Environment.Exit(0);
        }
    }
}
