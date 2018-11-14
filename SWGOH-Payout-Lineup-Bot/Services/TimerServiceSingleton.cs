using SWGOH_Payout_Lineup_Bot.Modules;
using System;
using System.Timers;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public sealed class TimerServiceSingleton
    {
        private static TimerServiceSingleton _instance = null;
        private static readonly object padlock = new object();

        public static TimerServiceSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    return _instance ?? (_instance = new TimerServiceSingleton());
                }
            }
        }

        bool _hasPosted;
        bool _hasMoved;
        DateTime _lastMoved = DateTime.Now;
        DateTime _lastPosted;

        public Timer Timer { get; private set; }

        public TimerLogSingleton TimerLog { get; } = new TimerLogSingleton();

        public void Start()
        {
            Timer = new Timer {Interval = 300000};
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
            TimerLog.Add("CheckForLineupMove at " + DateTime.Now);
            TimerLog.Add("_lastMoved: " + _lastMoved.Date);
            TimerLog.Add("_hasMoved: " + _hasMoved);

            if (DateTime.Today.Date != _lastMoved.Date)
            {
                var start = TimeSpan.Parse("00:00"); // midnight
                var end = TimeSpan.Parse("2:00"); // 2 o'clock
                var now = DateTime.Now.TimeOfDay;

                if ((now > start) && (now < end))
                {
                    if (!_hasMoved)
                    {
                        TimerLog.Add("new PayoutLineup().MoveLineupAsync(); at " + DateTime.Now);
                        await new PayoutLineupModule().MoveLineupAsync();
                        _hasMoved = true;
                        _lastMoved = DateTime.Now;
                    }
                }
            }
        }

        private async void CheckForPayoutMessage()
        {
            TimerLog.Add("CheckForPayoutMessage at " + DateTime.Now);
            TimerLog.Add("_lastPosted: " + _lastPosted.Date);
            TimerLog.Add("_hasPosted: " + _hasPosted);

            if (DateTime.Today.Date != _lastPosted.Date)
            {
                var start = TimeSpan.Parse("15:00"); // 3 o'clock
                var end = TimeSpan.Parse("17:00"); // 5 o'clock
                var now = DateTime.Now.TimeOfDay;

                if ((now > start) && (now < end))
                {
                    if (!_hasPosted)
                    {
                        TimerLog.Add("new PayoutLineup().GetLineupAsync(); at " + DateTime.Now);
                        await new PayoutLineupModule().GetLineupAsync();
                        _hasPosted = true;
                        _lastPosted = DateTime.Now;
                    }
                }
            }
        }
    }
}
