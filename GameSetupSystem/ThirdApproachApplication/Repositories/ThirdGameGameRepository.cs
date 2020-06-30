using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdApproachDomain;
using ThirdApproachDomain.GameAggregate;

namespace ThirdApproachApplication.Repositories
{
    public interface IThirdGameGameRepository
    {
        Task<ThirdGame> GetGameAsync(Guid guid);
        Task SaveGameAsync(ThirdGame newGame);
    }

    public class ThirdGameGameRepository : IThirdGameGameRepository
    {
        private readonly List<ThirdGame> _store = new List<ThirdGame>();

        public Task<ThirdGame> GetGameAsync(Guid guid)
        {
            var game = _store.SingleOrDefault(x => x.Guid == guid);

            if (game == null)
            {
                throw new BusinessLogicException($"Game with guid [{guid}] was not found.");
            }

            return Task.FromResult(game);
        }

        public Task SaveGameAsync(ThirdGame newGame)
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