using System;
using System.Collections.Generic;

namespace FirstApproachDomainLayer
{
    public enum FirstGameType
    {
        Soccer = 1,
        Basketball = 2,
        Chess = 3
    }

    public class FirstGame
    {
        public Guid Guid { get; set; }
        public DateTimeOffset GameDate { get; set; }
        public string Description { get; set; }

        public DateTimeOffset RegistrationEndDate { get; set; }
        public int MaxPlayersCount { get; set; }
        public int MinimalRequiredPlayersCount { get; set; }
        public IList<Player> PlayersRegistered { get; set; } = new List<Player>();
    }
}
