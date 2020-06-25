using System;
using System.Threading.Tasks;
using FirstApproachApplicationLayer.Repositories;
using FirstApproachDomainLayer;

namespace FirstApproachApplicationLayer
{
    public interface IGamesManager
    {
        Task<Guid> CreateGame(
            FirstGameType firstGameType,
            DateTimeOffset registrationEndDate,
            DateTimeOffset gameDate,
            string description);
        Task<FirstGame> GetGame(Guid guid);
    }

    public class GamesManager : IGamesManager
    {
        private readonly IFirstGameGameRepository _gameRepository;
        private readonly IFirstPlayerRepository _playerRepository;

        public GamesManager(
            IFirstGameGameRepository gameRepository,
            IFirstPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Guid> CreateGame(
            FirstGameType firstGameType,
            DateTimeOffset registrationEndDate,
            DateTimeOffset gameDate,
            string description)
        {
            switch (firstGameType)
            {
                case FirstGameType.Basketball:
                    var basketballGame = new FirstGame
                    {
                        RegistrationEndDate = registrationEndDate,
                        MaxPlayersCount = 10,
                        MinimalRequiredPlayersCount = 6,
                        Description = description,
                        GameDate = gameDate,
                        Guid = Guid.NewGuid()
                    };
                    await _gameRepository.SaveGameAsync(basketballGame);
                    return basketballGame.Guid;
                case FirstGameType.Chess:
                    var chessGame = new FirstGame
                    {
                        RegistrationEndDate = registrationEndDate,
                        MaxPlayersCount = 10,
                        MinimalRequiredPlayersCount = 6,
                        Description = description,
                        GameDate = gameDate,
                        Guid = Guid.NewGuid()
                    };
                    await _gameRepository.SaveGameAsync(chessGame);
                    return chessGame.Guid;

                case FirstGameType.Soccer:
                    var soccerGame = new FirstGame
                    {
                        RegistrationEndDate = registrationEndDate,
                        MaxPlayersCount = 10,
                        MinimalRequiredPlayersCount = 6,
                        Description = description,
                        GameDate = gameDate,
                        Guid = Guid.NewGuid()
                    };
                    await _gameRepository.SaveGameAsync(soccerGame);
                    return soccerGame.Guid;
                default:
                    throw new ArgumentException();
            }
        }

        public async Task<FirstGame> GetGame(Guid guid)
        {
            return await _gameRepository.GetGameAsync(guid);
        }

        // all other things you will ever want to do with Game goes here \/
    }
}
