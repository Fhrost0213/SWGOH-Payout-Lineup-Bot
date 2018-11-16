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
        public static bool HasPosted { get; private set; }
        public static bool HasMoved { get; private set; }
        public static DateTime LastMoved { get; private set; } = DateTime.Now.Date;
        public static DateTime LastPosted { get; private set; }

        public Timer Timer { get; private set; }

        readonly DiscordSocketClient _client;


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

            if (LastMoved.Date != DateTime.Today)
            {
                if (!HasMoved)
                {
                    PayoutLineupService.MoveLineup();
                    await WritePayout("The new lineup for today is: ");
                    HasMoved = true;
                    LastMoved = DateTime.Now;
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

            if (DateTime.Today.Date != LastPosted.Date)
            {
                var start = TimeSpan.Parse("15:30"); // 3:30pm
                var end = TimeSpan.Parse("17:00"); // 5:00pm
                var now = DateTime.Now.TimeOfDay.Subtract(TimeSpan.Parse("06:00")); // Add 6 hour offset

                if ((now > start) && (now < end))
                {
                    if (!HasPosted)
                    {
                        await WritePayout("It's payout time fellas! Today's current lineup is: ");

                        HasPosted = true;
                        LastPosted = DateTime.Now;
                    }
                }
            }
        }
    }
}
