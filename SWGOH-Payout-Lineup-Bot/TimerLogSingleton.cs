using System.Collections.Generic;

namespace SWGOH_Payout_Lineup_Bot
{
    public sealed class TimerLogSingleton
    {
        private static TimerLogSingleton instance = null;
        private static readonly object padlock = new object();

        public static TimerLogSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new TimerLogSingleton();
                        }
                    }
                }

                return instance;
            }
        }

        public List<string> Log { get; } = new List<string>();

        private const int MaxLogValue = 10000;

        public void Add(string value)
        {
            if (Log.Count >= MaxLogValue)
            {
                Log.RemoveAt(0);
            }

            Log.Add(value);
        }

    }
}
