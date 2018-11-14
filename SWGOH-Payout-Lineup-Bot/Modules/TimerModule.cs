using Discord.Commands;
using System.Threading.Tasks;
using SWGOH_Payout_Lineup_Bot.Services;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class TimerModule : ModuleBase<SocketCommandContext>
    {
        [Command("MessageTimeLog")]
        public async Task MessageTimeLogAsync()
        {
            var user = Context.Message.Author;
            var timeLog = new TimerLogSingleton();

            foreach (var logItem in timeLog.Log)
            {
                await Discord.UserExtensions.SendMessageAsync(user, logItem);
            }
        }

        [Command("IsTimerEnabled")]
        public async Task IsTimerEnabledAsync()
        {
            var timeService = new TimerServiceSingleton();

            await ReplyAsync(timeService.Timer.Enabled.ToString());
        }

        [Command("StartTimerService")]
        public async Task StartTimerServiceAsync()
        {
            var timeService = new TimerServiceSingleton();

            await ReplyAsync("TimerService is currently: " + timeService.Timer.Enabled);
            timeService.Start();
            await ReplyAsync("Forced a start of the timer service.");
            await ReplyAsync("TimerService is currently: " + timeService.Timer.Enabled);
        }
    }
}