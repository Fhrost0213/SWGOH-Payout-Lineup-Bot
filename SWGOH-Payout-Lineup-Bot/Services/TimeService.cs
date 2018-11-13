using SWGOH_Payout_Lineup_Bot.Modules;
using System;
using System.Timers;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class TimeService
    {
        bool HasPosted = false;
        bool HasMoved = false;
        DateTime _lastMoved = DateTime.Now;
        DateTime _lastPosted;
        Timer _timer;

        public void Start()
        {
            _timer = new Timer();
            _timer.Interval = 300000;
            _timer.Elapsed += new ElapsedEventHandler(TimerEventHandler);
            _timer.Start();
        }

        private void TimerEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Checking at " + DateTime.Now);
            CheckForPayoutMessage();
            CheckForLineupMove();
        }

        private void CheckForLineupMove()
        {
            if (DateTime.Today.Date != _lastMoved.Date)
            {
                TimeSpan start = TimeSpan.Parse("00:00"); // midnight
                TimeSpan end = TimeSpan.Parse("2:00"); // 2 o'clock
                TimeSpan now = DateTime.Now.TimeOfDay;

                if ((now > start) && (now < end))
                {
                    if (!HasMoved)
                    {
                        new PayoutLineup().MoveLineupAsync();
                        HasMoved = true;
                        _lastMoved = DateTime.Now;
                    }
                }
            }
        }

        private void CheckForPayoutMessage()
        {
            if (DateTime.Today.Date != _lastPosted.Date)
            {
                TimeSpan start = TimeSpan.Parse("15:00"); // 3 o'clock
                TimeSpan end = TimeSpan.Parse("17:00"); // 5 o'clock
                TimeSpan now = DateTime.Now.TimeOfDay;

                if ((now > start) && (now < end))
                {
                    if (!HasPosted)
                    {
                        new PayoutLineup().GetLineupAsync();
                        HasPosted = true;
                        _lastPosted = DateTime.Now;
                    }
                }
            }
        }
    }
}
