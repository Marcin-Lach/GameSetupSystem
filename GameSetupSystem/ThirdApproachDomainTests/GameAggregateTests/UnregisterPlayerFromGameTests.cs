using System;
using System.Linq;
using NUnit.Framework;
using ThirdApproachDomain;
using ThirdApproachDomain.GameAggregate;
using ThirdApproachDomain.GameAggregate.GameTypes;

namespace ThirdApproachDomainTests.GameAggregateTests
{
    [TestFixture]
    public class UnregisterPlayerFromGameTests
    {
        private Func<DateTimeOffset> _getNowFuncOriginalValue;

        [SetUp]
        public void SetUp()
        {
            _getNowFuncOriginalValue = DateTimeProvider.GetNowFunc;
            DateTimeProvider.GetNowFunc = () => new DateTimeOffset(2020, 6, 24, 13, 20, 0, new TimeSpan(0));
        }

        [TearDown]
        public void TearDown()
        {
            DateTimeProvider.GetNowFunc = _getNowFuncOriginalValue;
        }

        [Test]
        public void PlayerCanUnregisterOnlyOnce()
        {
            var game = CreateGame();
            var player = new Player();

            game.RegisterPlayer(player);
            Assert.AreEqual(1, game.PlayersRegistered.Count());

            game.UnregisterPlayer(player);
            Assert.AreEqual(0, game.PlayersRegistered.Count());

            game.UnregisterPlayer(player);
            Assert.AreEqual(0, game.PlayersRegistered.Count());
        }

        [Test]
        public void PlayerCannotUnregisterAfterRegistrationEndDate()
        {
            var game = CreateGame();
            var player = new Player();
            game.RegisterPlayer(player);

            // Set DTP.Now() to 1 second after gameRegistrationEndDate to emulate registration end
            var gameRegistrationEndDate = game.RegistrationEndDate;
            DateTimeProvider.GetNowFunc = () => gameRegistrationEndDate.AddSeconds(1);

            Assert.Throws<BusinessLogicException>(() => game.UnregisterPlayer(player));
        }

        private ThirdGame CreateGame()
        {
            return new SoccerGame(
                DateTimeProvider.Now.AddDays(3),
                "",
                DateTimeProvider.Now.AddDays(2));
        }
    }
}
