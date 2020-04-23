using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBotTemplate.Services
{
    public class DependencyInjectionService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _baseCommandService;
        public DependencyInjectionService(DiscordSocketClient discordClient, CommandService baseCommandService)
        {
            _discordClient = discordClient;
            _baseCommandService = baseCommandService;
        }

        public IServiceProvider BuildServiceProvider() =>
            new ServiceCollection()
                .AddSingleton(_discordClient)
                .AddSingleton(_baseCommandService)
                .BuildServiceProvider();
    }
}
