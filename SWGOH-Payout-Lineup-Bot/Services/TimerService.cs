using Discord;
using Discord.WebSocket;
using SWGOH_Payout_Lineup_Bot.Modules;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class TimerService
    {
        static bool _hasPosted;
        static bool _hasMoved;
        static DateTime _lastMoved = DateTime.Now;
        static DateTime _lastPosted;

        public Timer Timer { get; private set; }

        DiscordSocketClient _client;


        public TimerService(DiscordSocketClient client)
        {
            _client = client;
        }

        public void Start()
        {
            Timer = new Timer {Interval = 30000};
            Timer.Elapsed += TimerEventHandler;
            Timer.Start();
        }

        private void TimerEventHandler(object sender, EventArgs e)
        {
            CheckForPayoutMessage();
            CheckForLineupMove();
        }

        private async void CheckForLineupMove()
        {
            Console.WriteLine("CheckForLineupMove at time: " + DateTime.Now.TimeOfDay);

            if (DateTime.Today.Date != _lastMoved.Date)
            {
                var start = TimeSpan.Parse("00:00"); // midnight
                var end = TimeSpan.Parse("2:00"); // 2 o'clock
                var now = DateTime.Now.TimeOfDay.Subtract(TimeSpan.Parse("06:00")); // Add 6 hour offset

                if ((now > start) && (now < end))
                {
                    if (!_hasMoved)
                    {
                        PayoutLineupService.MoveLineup();
                        await WritePayout("The new lineup for today is: ");
                        _hasMoved = true;
                        _lastMoved = DateTime.Now;
                    }
                }
            }
        }

        private Task WritePayout(string message)
        {
            int order = 0;
            var lineup = PayoutLineupService.GetLineup();

            var channel = _client.GetChannel(511939319704059912) as IMessageChannel;
            channel.SendMessageAsync(message);

            foreach (var player in lineup)
            {
                order++;
                channel.SendMessageAsync(order + ": " + player);
            }

            return Task.CompletedTask;
        }

        private async void CheckForPayoutMessage()
        {
            Console.WriteLine("CheckForPayoutMessage at time: " + DateTime.Now.TimeOfDay);

            if (DateTime.Today.Date != _lastPosted.Date)
            {
                var start = TimeSpan.Parse("16:00"); // 4pm
                var end = TimeSpan.Parse("17:00"); // 5pm
                var now = DateTime.Now.TimeOfDay.Subtract(TimeSpan.Parse("06:00")); // Add 6 hour offset

                if ((now > start) && (now < end))
                {
                    if (!_hasPosted)
                    {
                        await WritePayout("Today's current lineup is: ");

                        _hasPosted = true;
                        _lastPosted = DateTime.Now;
                    }
                }
            }
        }
    }
}
