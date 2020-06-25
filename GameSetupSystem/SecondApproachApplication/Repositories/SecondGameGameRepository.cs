using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecondApproachDomain;

namespace SecondApproachApplication.Repositories
{
    public interface ISecondGameGameRepository
    {
        Task<SecondGame> GetGameAsync(Guid guid);
        Task SaveGameAsync(SecondGame newGame);
    }

    public class SecondGameGameRepository : ISecondGameGameRepository
    {
        private readonly List<SecondGame> _store = new List<SecondGame>();

        public Task<SecondGame> GetGameAsync(Guid guid)
        {
            var game = _store.SingleOrDefault(x => x.Guid == guid);

            if (game == null)
            {
                throw new BusinessLogicException($"Game with guid [{guid}] was not found.");
            }

            return Task.FromResult(game);
        }

        public Task SaveGameAsync(SecondGame newGame)
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