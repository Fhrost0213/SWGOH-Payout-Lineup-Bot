using Discord;
using Discord.WebSocket;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class ChannelService
    {
        public DiscordSocketClient Client { get; set; }

        private ulong _channelId;

        public void SetMainChannelId(ulong channelId)
        {
            _channelId = channelId;
        }
        public IMessageChannel GetMainChannel()
        {
            return (IMessageChannel)Client.GetChannel(_channelId);
        }
    }
}
