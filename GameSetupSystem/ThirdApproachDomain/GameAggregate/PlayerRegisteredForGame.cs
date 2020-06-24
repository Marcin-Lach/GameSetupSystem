using System;

namespace ThirdApproachDomain.GameAggregate
{
    public class PlayerRegisteredForGame
    {
        public Guid GameGuid { get; }
        public Guid PlayerGuid { get; }

        public PlayerRegisteredForGame(
            Player player,
            ThirdGame game)
        {
            PlayerGuid = player.Guid;
            GameGuid = game.Guid;
        }
    }
}