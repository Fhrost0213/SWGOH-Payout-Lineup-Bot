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
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _serviceProvider;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _serviceProvider = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "NTExOTMxNzIzMjY1NzM2NzA2.DsyIvQ.JT3AKTMMXQi74nqKtwgFtA3qrZU";

            //event subscriptions
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(Discord.TokenType.Bot, botToken);

            await _client.StartAsync();

            new TimeService().Start();

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

            int argPos = 0;

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
