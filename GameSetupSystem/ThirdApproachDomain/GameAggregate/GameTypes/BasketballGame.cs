using System;

namespace ThirdApproachDomain.GameAggregate.GameTypes
{
    public class BasketballGame : ThirdGame
    {
        public BasketballGame(
            DateTimeOffset gameDate, 
            string description, 
            DateTimeOffset registrationEndDate) 
            : base(new ZeroPositiveInt(10), new ZeroPositiveInt(6), gameDate, description, registrationEndDate)
        {
        }

        public override ThirdGameDiscriminator Discriminator => ThirdGameDiscriminator.Basketball;
    }
}