using System;
using NUnit.Framework;
using ThirdApproachDomain;
using ThirdApproachDomain.GameAggregate;

namespace ThirdApproachDomainTests.GameAggregateTests
{
    [TestFixture]
    public class GameCreatingTests
    {
        [Test]
        public void MinimalRequiredPlayersCountCannotBeHigherThanMaximalPlayersCount()
        {
            Assert.Throws<BusinessLogicException>(
                () => new TestGame(
                    new ZeroPositiveInt(5),
                    new ZeroPositiveInt(10),
                    DateTimeOffset.MaxValue,
                    "",
                    DateTimeOffset.MaxValue));
        }

        public class TestGame : ThirdGame
        {
            public TestGame(
                ZeroPositiveInt maxPlayersCount,
                ZeroPositiveInt minimalRequiredPlayersCount,
                DateTimeOffset gameDate, 
                string description, 
                DateTimeOffset registrationEndDate) 
                : base(maxPlayersCount, minimalRequiredPlayersCount, gameDate, description, registrationEndDate)
            {
            }

            public override ThirdGameDiscriminator Discriminator => ThirdGameDiscriminator.None;
        }

    }
}