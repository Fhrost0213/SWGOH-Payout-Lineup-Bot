using Discord.Commands;
using SWGOH_Payout_Lineup_Bot.Services;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class Players : ModuleBase<SocketCommandContext>
    {
        [Command("AddPlayer")]
        public async Task AddPlayerAsync([Remainder]string playerName)
        {
            if (playerName is null)
            {
                await ReplyAsync("You need to specify a player!");
                return;
            }

            PlayerService.AddPlayer(playerName);
            PayoutLineupService.AddPlayerToLineup(playerName);
            await ReplyAsync("Added Player " + playerName);
        }

        [Command("RemovePlayer")]
        public async Task RemovePlayerAsync([Remainder]string playerName)
        {
            if (playerName is null)
            {
                await ReplyAsync("You need to specify a player!");
                return;
            }

            PlayerService.RemovePlayer(playerName);
            PayoutLineupService.RemovePlayerFromLineup(playerName);
            await ReplyAsync("Removed Player " + playerName);
        }

        [Command("GetPlayers")]
        public async Task GetPlayersAsync()
        {
            var players = PlayerService.GetPlayers();

            foreach (var player in players)
            {
                await ReplyAsync(player);
            }
        }
    }
}
