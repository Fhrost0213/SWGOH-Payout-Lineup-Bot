using System;
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
            await ReplyAsync("Use the prefix '#' with the current commands:");
            await ReplyAsync("AddPlayer          | Add a player to the lineup.");
            await ReplyAsync("AddPlayers         | Adds a comma seperated list of players to the lineup.");
            await ReplyAsync("RemovePlayer       | Remove a player from the lineup.");
            await ReplyAsync("GetPlayers         | Get the current players that are included in the lineup.");
            await ReplyAsync("GetLineup          | Get the current payout lineup.");
            await ReplyAsync("IsTimerEnabled     | Returns the timer enabled status.");
            await ReplyAsync("GetServerTime      | Gets the current time on the server.");
            await ReplyAsync("GetPayoutTimeStart | Gets the payout start time.");
            await ReplyAsync("GetPayoutTimeEnd   | Gets the payout end time.");
            await ReplyAsync("GetPayoutMessageOffset | Gets the payout message offset.");
            await ReplyAsync("SetPayoutTimeStart | Sets the payout start time.");
            await ReplyAsync("SetPayoutTimeEnd   | Sets the payout end time.");
            await ReplyAsync("SetPayoutMessageOffset | Sets the payout message offset. This value needs to be set in hours:minutes. Example - 06:00");
        } 
    }
}
