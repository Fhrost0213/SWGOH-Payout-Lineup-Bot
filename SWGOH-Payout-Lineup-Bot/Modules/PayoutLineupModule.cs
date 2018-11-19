using Discord.Commands;
using SWGOH_Payout_Lineup_Bot.Services;
using System.Threading.Tasks;
using Discord;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class PayoutLineupModule : ModuleBase<SocketCommandContext>
    {
        [Command("GetLineup")]
        public async Task GetLineupAsync()
        {
            await WriteLineupAsync("Today's current lineup is: ");
        }

        public async Task WriteLineupAsync(string message)
        {
            await Context.Channel.SendMessageAsync(message);

            var order = 0;
            var lineup = PayoutLineupService.GetLineup();

            foreach (var player in lineup)
            {
                order++;
                await Context.Channel.SendMessageAsync(order + ": " + player);
            }
        }

        [Command("MoveLineup")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MoveLineupAsync()
        {
            await WriteLineupAsync("Previous lineup was: ");

            PayoutLineupService.MoveLineup();

            await WriteLineupAsync("New lineup is: ");
        }
    }
}
