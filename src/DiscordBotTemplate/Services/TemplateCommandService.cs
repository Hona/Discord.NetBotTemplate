using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotTemplate.Constants;
using DiscordBotTemplate.Logging;
using DiscordBotTemplate.Models;
using Environment = System.Environment;

namespace DiscordBotTemplate.Services
{
    public class TemplateCommandService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _baseCommandService;
        private readonly IServiceProvider _serviceProvider;
        private readonly Config _config;

        public TemplateCommandService(DiscordSocketClient discordClient, CommandService baseCommandService, IServiceProvider serviceProvider, Config config)
        {
            _discordClient = discordClient;
            _baseCommandService = baseCommandService;
            _serviceProvider = serviceProvider;
            _config = config;
        }

        public static CommandService BuildBaseCommandService()
        {
            var commandServiceConfig = new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                // Default RunMode to async to prevent commands from blocking the thread
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true,
                LogLevel = LogSeverity.Info
            };

            return new CommandService(commandServiceConfig);
        }

        public async Task InitializeAsync()
        {
            // Main handler for command input
            _discordClient.MessageReceived += HandleCommandAsync;
            Logger.LogInfo("CommandService: Registered MessageReceived event");

            // Post execution handler
            _baseCommandService.CommandExecuted += OnCommandExecutedAsync;
            Logger.LogInfo("CommandService: Registered CommandExecuted event");

            // Install discord commands
            await _baseCommandService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
            Logger.LogInfo($"CommandService: Added {_baseCommandService.Modules.Count()} modules using reflection, with a total of {_baseCommandService.Commands.Count()} commands");
        }

        private static async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context,
            IResult result)
        {
            // Since commands are run in an async context, errors have to be manually handled
            if (!string.IsNullOrEmpty(result?.ErrorReason))
            {
                var embedBuilder = new EmbedBuilder
                {
                    Timestamp = DateTime.Now,
                    Color = Color.Red
                };
                await context.Channel.SendMessageAsync(embed: embedBuilder.WithDescription(result.ErrorReason).Build());
                var commandName = command.IsSpecified ? command.Value.Name : "An unknown command";

                Logger.LogError($"MomentumCommandService: {commandName} threw an error at {DateTime.Now}: {Environment.NewLine}{result.ErrorReason}");
            }
        }

        private async Task HandleCommandAsync(SocketMessage inputMessage)
        {
            // Don't process the command if it was a system message
            if (inputMessage is SocketUserMessage message)
            {
                // Create a number to track where the prefix ends and the command begins
                var argPosition = 0;

                // Determine if the message is a command based on the prefix and make sure no bots trigger commands
                if (message.Author.IsBot ||
                    !(message.HasStringPrefix(_config.CommandPrefix, ref argPosition) ||
                      message.HasMentionPrefix(_discordClient.CurrentUser, ref argPosition))) return;

                // Create a WebSocket-based command context based on the message
                var context = new SocketCommandContext(_discordClient, message);

                // Execute the command with the command context we just
                // created, along with the service provider for precondition checks.
                await _baseCommandService.ExecuteAsync(
                    context,
                    argPosition,
                    _serviceProvider);
            }
        }
    }
}
