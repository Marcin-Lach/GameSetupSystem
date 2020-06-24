using System;

namespace ThirdApproachDomain.GameAggregate.GameTypes
{
    public class SoccerGame : ThirdGame
    {
        public SoccerGame(
            DateTimeOffset gameDate, 
            string description, 
            DateTimeOffset registrationEndDate) 
            : base(new ZeroPositiveInt(22), new ZeroPositiveInt(10), gameDate, description, registrationEndDate)
        {
        }

        public override ThirdGameDiscriminator Discriminator => ThirdGameDiscriminator.Soccer;
    }
}