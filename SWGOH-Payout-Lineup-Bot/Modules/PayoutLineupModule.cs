using Discord.Commands;
using SWGOH_Payout_Lineup_Bot.Services;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class PayoutLineupModule : ModuleBase<SocketCommandContext>
    {
        [Command("GetLineup")]
        public async Task GetLineupAsync()
        {
            await ReplyAsync("Today's current lineup is: ");

            await WriteLineupAsync();
        }

        private async Task WriteLineupAsync()
        {
            int order = 0;
            var lineup = PayoutLineupService.GetLineup();

            foreach (var player in lineup)
            {
                order++;
                await ReplyAsync(order + ": " + player);
            }
        }

        [Command("MoveLineup")]
        public async Task MoveLineupAsync()
        {
            await ReplyAsync("Previous lineup was: ");

            await WriteLineupAsync();

            PayoutLineupService.MoveLineup();

            await ReplyAsync("New lineup is: ");

            await WriteLineupAsync();
        }
    }
}
