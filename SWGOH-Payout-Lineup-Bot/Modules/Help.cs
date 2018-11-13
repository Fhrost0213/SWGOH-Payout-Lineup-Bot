using Discord.Commands;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("This bot will display the current payout lineup for each day.");
            await ReplyAsync("");
            await ReplyAsync("Use the prefix '#' with the current commands:");
            await ReplyAsync("AddPlayer");
            await ReplyAsync("RemovePlayer");
            await ReplyAsync("GetPlayers");
            await ReplyAsync("GetLineup");
        }
    }
}
