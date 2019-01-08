using System;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class EnvironmentVariablesService : IEnvironmentVariablesService
    {
        public string Data => Environment.GetEnvironmentVariable("DATA");
    }
}
