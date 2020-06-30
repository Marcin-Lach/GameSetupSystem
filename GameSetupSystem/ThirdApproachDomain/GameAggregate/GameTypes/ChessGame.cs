using System;

namespace ThirdApproachDomain.GameAggregate.GameTypes
{
    public class ChessGame : ThirdGame
    {
        public ChessGame(
            DateTimeOffset gameDate,
            string description,
            DateTimeOffset registrationEndDate)
            : base(new ZeroPositiveInt(2), new ZeroPositiveInt(2), gameDate, description, registrationEndDate)
        {
        }

        public override ThirdGameDiscriminator Discriminator => ThirdGameDiscriminator.Chess;
    }
}