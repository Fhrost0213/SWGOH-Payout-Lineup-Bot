using Discord.Commands;
using SWGOH_Payout_Lineup_Bot.Services;
using System.Threading.Tasks;
using Discord;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class PayoutLineupModule : ModuleBase<SocketCommandContext>
    {
        public PayoutLineupService PayoutLineupService { get; set; }

        [Command("GetLineup")]
        public async Task GetLineupAsync()
        {
            await PayoutLineupService.WriteLineupAsync("Today's current lineup is: ");
        }

        [Command("MoveLineup")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MoveLineupAsync()
        {
            await PayoutLineupService.WriteLineupAsync("Previous lineup was: ");

            PayoutLineupService.MoveLineup();

            await PayoutLineupService.WriteLineupAsync("New lineup is: ");
        }
    }
}
