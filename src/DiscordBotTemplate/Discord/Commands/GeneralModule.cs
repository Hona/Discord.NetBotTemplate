using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBotTemplate.Models;
using DiscordBotTemplate.Utilities;

namespace DiscordBotTemplate.Discord.Commands
{
    public class GeneralModule : ModuleBase
    {
        public CommandService CommandService { get; set; }
        public IServiceProvider Services { get; set; }
        public Config Config { get; set; }

        [Command("help")]
        [Summary("The command you are running")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Only the messages you have permission to use in this channel are included.");

            var tempEmbed = new EmbedBuilder
            {
                Description = "Building the help command... This message will be deleted when all help messages are sent",
                Color = Color.Blue
            };
            var tempMessage = await ReplyAsync(embed: tempEmbed.Build());

            foreach (var module in CommandService.Modules.Where(x => !x.Name.Contains("ModuleBase")))
            {
                var moduleHelpEmbed = HelpCommandUtilities.GetModuleHelpEmbed(module, Context, Services, Config);
                if (moduleHelpEmbed.Fields.Length > 0)
                {
                    await ReplyAsync(embed: moduleHelpEmbed);
                }
            }
            await tempMessage.DeleteAsync();
        }
    }
}
