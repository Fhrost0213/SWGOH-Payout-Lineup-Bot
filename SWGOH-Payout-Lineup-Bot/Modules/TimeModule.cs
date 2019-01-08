using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using SWGOH_Payout_Lineup_Bot.Services;

namespace SWGOH_Payout_Lineup_Bot.Modules
{
    public class TimeModule : ModuleBase<SocketCommandContext>
    {
        [Command("GetServerTime")]
        public async Task GetServerTimeAsync()
        {
            await ReplyAsync("Current server time is: " + DateTime.Now);
        }

        [Command("GetPayoutTimeStart")]
        public async Task GetPayoutTimeStartAsync()
        {
            await ReplyAsync("Current payout start time is: " + TimerService.PayoutTimeStart + ".");
        }

        [Command("GetPayoutTimeEnd")]
        public async Task GetPayoutTimeEndAsync()
        {
            await ReplyAsync("Current payout end time is: " + TimerService.PayoutTimeEnd + ".");
        }

        [Command("GetPayoutMessageOffset")]
        public async Task GetPayoutMessageOffsetAsync()
        {
            await ReplyAsync("Current payout message offset is: " + TimerService.PayoutMessageOffset + "."); 
        }

        [Command("SetPayoutTimeStart")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetPayoutTimeStartAsync([Remainder]string time)
        {
            await GetPayoutTimeStartAsync();
            TimerService.PayoutTimeStart = time;
            await GetPayoutTimeStartAsync();
        }

        [Command("SetPayoutTimeEnd")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetPayoutTimeEndAsync([Remainder]string time)
        {
            await GetPayoutTimeEndAsync();
            TimerService.PayoutTimeEnd = time;
            await GetPayoutTimeEndAsync();
        }

        [Command("SetPayoutMessageOffset")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetPayoutMessageOffsetAsync([Remainder]string time)
        {
            await GetPayoutMessageOffsetAsync();
            TimerService.PayoutMessageOffset = time;
            await GetPayoutMessageOffsetAsync();
        }
    }
}
