﻿using System.Collections.Generic;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public static class PlayerService
    {
        private static readonly List<string> _players = new List<string>();

        public static void AddPlayer(string playerName)
        {
            _players.Add(playerName);
        }

        public static void RemovePlayer(string playerName)
        {
            _players.Remove(playerName);
        }

        public static List<string> GetPlayers()
        {
            return _players;
        }
    }
}
