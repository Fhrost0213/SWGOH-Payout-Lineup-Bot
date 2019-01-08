using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class PayoutLineupService
    {
        public ChannelService ChannelService { get; set; }

        private static readonly string[] _payoutLineup = new string[20];
        private static int _currentIndex;

        public static void AddPlayerToLineup(string playerName)
        {
            _payoutLineup[_currentIndex] = playerName;
            _currentIndex++;
        }

        public static void RemovePlayerFromLineup(string playerName)
        {
            _payoutLineup[_currentIndex] = "";
            _currentIndex--;
        }

        public static List<string> GetLineup()
        {
            var list = new List<string>();

            foreach (var player in _payoutLineup)
            {
                if (!string.IsNullOrWhiteSpace(player))
                {
                    list.Add(player);
                }
            }

            return list;
        }

        public static void MoveLineup()
        {
            var firstPerson = "";

            for (int i = 0; i < _currentIndex; i++)
            {
                // Move first to last and everyone up one
                if (i == 0)
                {
                    firstPerson = _payoutLineup[i];
                    _payoutLineup[i] = _payoutLineup[i + 1];

                }
                else if (i == _currentIndex - 1)
                {
                    _payoutLineup[i] = firstPerson;
                }
                else
                {
                    _payoutLineup[i] = _payoutLineup[i + 1];
                } 
            }
        }

        public async Task WriteLineupAsync(string message)
        {
            var channel = ChannelService.GetMainChannel();

            await channel.SendMessageAsync(message);

            var order = 0;
            var lineup = GetLineup();

            foreach (var player in lineup)
            {
                order++;
                await channel.SendMessageAsync(order + ": " + player);
            }
        }
    }
}
