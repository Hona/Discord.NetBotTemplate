using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBotTemplate.Discord.Commands
{
    [RequireOwner]
    public class OwnerModule : ModuleBase
    {
        [Command("test")]
        public async Task TestCommandAsync(string input)
        {
            await ReplyAsync(input);
        }
    }
}
