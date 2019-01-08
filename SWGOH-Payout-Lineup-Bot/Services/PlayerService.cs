using System.Collections.Generic;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class PlayerService
    {
        public PlayerDataService PlayerDataService { get; set; }

        private readonly List<string> _players = new List<string>();

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
        }

        public void RemovePlayer(string playerName)
        {
            _players.Remove(playerName);
        }

        public List<string> GetPlayers()
        {
            return _players;
        }

        public void InitializePlayers()
        {
        }
    }
}
