using System;
using System.Linq;
using NUnit.Framework;
using ThirdApproachDomain;
using ThirdApproachDomain.GameAggregate;
using ThirdApproachDomain.GameAggregate.GameTypes;

namespace ThirdApproachDomainTests.GameAggregateTests
{
    [TestFixture]
    public class RegisterPlayersForGameTests
    {
        private Func<DateTimeOffset> _getNowFuncOriginalValue;

        [SetUp]
        public void SetUp()
        {
            _getNowFuncOriginalValue = DateTimeProvider.GetNowFunc;
            DateTimeProvider.GetNowFunc = () => new DateTimeOffset(2020, 6, 24, 13, 20, 0,new TimeSpan(0));
        }

        [TearDown]
        public void TearDown()
        {
            DateTimeProvider.GetNowFunc = _getNowFuncOriginalValue;
        }

        [Test]
        public void PlayerCanRegisterOnlyOnce()
        {
            var game = CreateGame();
            var player = new Player();

            game.RegisterPlayer(player);
            Assert.AreEqual(1, game.PlayersRegistered.Count());

            game.RegisterPlayer(player);
            Assert.AreEqual(1, game.PlayersRegistered.Count());
            Assert.AreEqual(1, game.PlayersRegistered.Count(x => x.PlayerGuid == player.Guid));
        }

        [Test]
        public void PlayerCannotRegisterAfterRegistrationEndDate()
        {
            var game = CreateGame();

            // Set DTP.Now() to 1 second after gameRegistrationEndDate to emulate registration end
            var gameRegistrationEndDate = game.RegistrationEndDate;
            DateTimeProvider.GetNowFunc = () => gameRegistrationEndDate.AddSeconds(1);

            var player = new Player();
            
            Assert.Throws<BusinessLogicException>(() => game.RegisterPlayer(player));
        }

        [Test]
        public void PlayerCannotRegisterAfterMaxPlayersCountHaveBeenReached()
        {
            var game = CreateGame();
            // Set DTP.Now() to 1 second before gameRegistrationEndDate to emulate registration not ended
            var gameRegistrationEndDate = game.RegistrationEndDate;
            DateTimeProvider.GetNowFunc = () => gameRegistrationEndDate.AddSeconds(-1);

            // Fill all available spots
            for (var i = 0; i < game.MaxPlayersCount.Value; i++)
            {
                game.RegisterPlayer(new Player());
            }

            // check if all spots have been taken
            Assert.AreEqual(game.MaxPlayersCount.Value, game.PlayersRegistered.Count());

            var player = new Player();
            Assert.Throws<BusinessLogicException>(() => game.RegisterPlayer(player));
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
