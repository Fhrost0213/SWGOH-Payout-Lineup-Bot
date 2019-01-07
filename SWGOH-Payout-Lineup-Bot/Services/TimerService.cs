using SWGOH_Payout_Lineup_Bot.Modules;
using System;
using System.Timers;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class TimerService
    {
        public static bool HasPosted { get; private set; }
        public static DateTime LastMoved { get; private set; } = DateTime.Now;
        public static DateTime LastPosted { get; private set; }
        public static string PayoutMessageOffset { get; set; } = "00:00";
        public static string PayoutTimeStart { get; set; } = "15:30";
        public static string PayoutTimeEnd { get; set; } = "17:00";

        public Timer Timer { get; private set; }

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

            if (LastMoved.Date != DateTime.Today.Date)
            {
                PayoutLineupService.MoveLineup();
                await new PayoutLineupModule().WriteLineupAsync("Tomorrow's lineup is: ");

                LastMoved = DateTime.Now;
                HasPosted = false;
            }
        }

        private async void CheckForPayoutMessage()
        {
            Console.WriteLine("CheckForPayoutMessage at time: " + DateTime.Now.TimeOfDay);

            if (DateTime.Today.Date != LastPosted.Date)
            {
                var start = TimeSpan.Parse(PayoutTimeStart); // 3:30pm
                var end = TimeSpan.Parse(PayoutTimeEnd); // 5:00pm
                var now = DateTime.Now.TimeOfDay.Subtract(TimeSpan.Parse(PayoutMessageOffset));

                if ((now > start) && (now < end))
                {
                    if (!HasPosted)
                    {
                        await new PayoutLineupModule().WriteLineupAsync("It's payout time fellas! Today's current lineup is: ");

                        HasPosted = true;
                        LastPosted = DateTime.Now;
                    }
                }
            }
        }
    }
}
