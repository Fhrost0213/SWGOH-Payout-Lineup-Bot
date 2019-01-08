using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SWGOH_Payout_Lineup_Bot.Services
{
    public class PlayerDataService
    {
        private IEnvironmentVariablesService _environmentVariablesService { get; }

        private readonly string _dataPath;
        private readonly string _playerDataFileName = "playerdata.xml";
        private readonly string _fullPath;

        public PlayerDataService(IEnvironmentVariablesService environmentVariablesService)
        {
            _environmentVariablesService = environmentVariablesService;
            _dataPath = _environmentVariablesService.Data;
            _fullPath = _dataPath + _playerDataFileName;
        }

        public List<string> GetPayoutLineup()
        {
            var lineup = new List<string>();

            if (!string.IsNullOrWhiteSpace(_dataPath))
            {
                CreateFileIfNotExists(_fullPath, _dataPath);

                var xml = XDocument.Load(_fullPath);
                var list = xml?.Root?.Elements("PlayerName")
                    .Select(x => x.Value)
                    .ToList();

                lineup.AddRange(list);
            }

            return lineup;
        }

        private void CreateFileIfNotExists(string fullPath, string dataPath)
        {
            if (!File.Exists(fullPath))
            {
                Directory.CreateDirectory(dataPath);
                var xml = new XDocument();
                xml.Add(new XElement("Lineup",
                    new XElement("PlayerName")));

                xml.Save(fullPath);
            }  
        }

        public void SetPayoutLineup(string[] lineup)
        {
            if (!string.IsNullOrWhiteSpace(_dataPath))
            {
                var doc = new XDocument();
                var lineupElement = new XElement("Lineup");

                foreach (var player in lineup)
                {
                    lineupElement.Add(new XElement("PlayerName", player));
                }

                doc.Add(lineupElement);

                doc.Save(_fullPath);
            }
        }
    }
}
