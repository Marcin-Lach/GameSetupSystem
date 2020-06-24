using System;

namespace ThirdApproachDomain
{
    public class Player
    {
        public Guid Guid { get; }

        public Player()
        {
            Guid = Guid.NewGuid();
        }
    }
}