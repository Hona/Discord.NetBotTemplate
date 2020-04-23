using System;
using System.IO;

namespace DiscordBotTemplate.Constants
{
    public static class PathConstants
    {
        private static string EnvironmentConfigFolder => Path.Combine(Environment.CurrentDirectory, Environment.GetEnvironmentVariable("ENVIRONMENT"), "config");
        public static string DiscordBotTokenFile => Path.Combine(EnvironmentConfigFolder, "discordBotToken.txt");
        public static string ConfigFile => Path.Combine(EnvironmentConfigFolder, "config.json");
    }
}
