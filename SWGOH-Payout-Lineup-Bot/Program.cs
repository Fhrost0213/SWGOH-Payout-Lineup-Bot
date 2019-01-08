using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SWGOH_Payout_Lineup_Bot.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot
{
    internal class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private ChannelService _channelService;
        private PlayerService _playerService;
        private IServiceProvider _serviceProvider;

        private const string BotToken = "";

        public async Task RunBotAsync()
        {
            Console.WriteLine("Version 0.2");

            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _channelService = new ChannelService();
            _playerService = new PlayerService();

            _channelService.SetMainChannelId(511939319704059912);

            _serviceProvider = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton(_channelService)
                .AddSingleton(_playerService)
                .BuildServiceProvider();

            //event subscriptions
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, BotToken);

            await _client.StartAsync();

            var timerService = new TimerService();
            timerService.Start();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
                return;

            var argPos = 0;

            if (message.HasStringPrefix("#", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _serviceProvider);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
