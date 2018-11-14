using Discord.Commands;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("Help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("This bot will display the current payout lineup for each day.");
            await ReplyAsync("");
            await ReplyAsync("Use the prefix '#' with the current commands:");
            await ReplyAsync("AddPlayer          | Add a player to the lineup.");
            await ReplyAsync("RemovePlayer       | Remove a player from the lineup.");
            await ReplyAsync("GetPlayers         | Get the current players that are included in the lineup.");
            await ReplyAsync("GetLineup          | Get the current payout lineup.");
            await ReplyAsync("MessageTimeLog     | Send a direct message with the timer log.");
            await ReplyAsync("IsTimerEnabled     | Returns the timer enabled status.");
            await ReplyAsync("StartTimerService  | Force start the timer service.");     
        }
    }
}
