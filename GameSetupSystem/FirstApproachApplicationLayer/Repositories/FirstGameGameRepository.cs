using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApproachDomainLayer;

namespace FirstApproachApplicationLayer.Repositories
{
    public interface IFirstGameGameRepository
    {
        Task<FirstGame> GetGameAsync(Guid guid);
        Task SaveGameAsync(FirstGame newGame);
    }

    public class FirstGameGameRepository : IFirstGameGameRepository
    {
        private readonly List<FirstGame> _store = new List<FirstGame>();

        public Task<FirstGame> GetGameAsync(Guid guid)
        {
            var game = _store.SingleOrDefault(x => x.Guid == guid);

            if (game == null)
            {
                throw new BusinessLogicException($"Game with guid [{guid}] was not found.");
            }

            return Task.FromResult(game);
        }

        public Task SaveGameAsync(FirstGame newGame)
        {
            var storedGame = _store.SingleOrDefault(x => x.Guid == newGame.Guid);
            if (storedGame != null)
            {
                _store.Remove(storedGame);
            }

            _store.Add(newGame);
            return Task.CompletedTask;
        }
    }
}