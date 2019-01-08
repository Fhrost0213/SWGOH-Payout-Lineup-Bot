using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SWGOH_Payout_Lineup_Bot.Services;

namespace SWGOH_Payout_Lineup_Bot.Tests.Services
{
    [TestClass]
    public class PlayerDataServiceTests
    {
        [TestMethod]
        public void SetPayoutLineup_Equals_3()
        {
            // Arrange
            var environmentVariablesMock = new Mock<IEnvironmentVariablesService>();
            environmentVariablesMock.Setup(x => x.Data).Returns("/data/");

            var playerLineup = new string[3];
            playerLineup[0] = "Test1";
            playerLineup[1] = "Test1";
            playerLineup[2] = "Test1";

            var sut = new PlayerDataService(environmentVariablesMock.Object);

            // Act
            sut.SetPayoutLineup(playerLineup);

            var lineup = sut.GetPayoutLineup();

            // Assert
            Assert.AreEqual(lineup.Count, 3);
        }
    }
}
