using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApproachDomainLayer;

namespace FirstApproachApplicationLayer.Repositories
{
    public interface IFirstPlayerRepository
    {
        Task<Player> GetPlayerAsync(Guid guid);
        Task<IEnumerable<Player>> GetPlayersAsync();
    }

    public class FirstPlayerRepository : IFirstPlayerRepository
    {
        private readonly List<Player> _store = new List<Player>
        {
            new Player(),
            new Player(),
            new Player(),
            new Player(),
            new Player()
        };

        public Task<IEnumerable<Player>> GetPlayersAsync()
        {
            return Task.FromResult(_store.Where(x => x == x));
        }

        public Task<Player> GetPlayerAsync(Guid guid)
        {
            var player = _store.SingleOrDefault(x => x.Guid == guid);

            if (player == null)
            {
                throw new BusinessLogicException($"Player with guid [{guid}] was not found.");
            }

            return Task.FromResult(player);
        }
    }
}