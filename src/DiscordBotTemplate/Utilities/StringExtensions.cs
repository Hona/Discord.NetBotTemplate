using System.Text.RegularExpressions;

namespace DiscordBotTemplate.Utilities
{
    public static class StringExtensions
    {
        public static string CleanLineEndings(this string input) 
            => Regex.Replace(input.Trim(), @"\r\n?|\n", string.Empty);
    }
}
