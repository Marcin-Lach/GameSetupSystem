using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecondApproachApplication.Repositories;

namespace SecondApproachApplication.Commands
{
    public class RegisterPlayerForGameCommand : IRequest
    {
        public Guid GameGuid { get; }
        public Guid PlayerGuid { get; }

        public RegisterPlayerForGameCommand(
            Guid gameGuid, 
            Guid playerGuid)
        {
            GameGuid = gameGuid;
            PlayerGuid = playerGuid;
        }
    }

    public class RegisterPlayerForGameCommandHandler : IRequestHandler<RegisterPlayerForGameCommand>
    {
        private readonly ISecondGameGameRepository _gameGameRepository;
        private readonly ISecondPlayerRepository _playerRepository;

        public RegisterPlayerForGameCommandHandler(
            ISecondGameGameRepository gameGameRepository,
            ISecondPlayerRepository playerRepository)
        {
            _gameGameRepository = gameGameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(RegisterPlayerForGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameGameRepository.GetGameAsync(request.GameGuid);
            var player = await _playerRepository.GetPlayerAsync(request.PlayerGuid);

            game.PlayersRegistered.Add(player);
            return Unit.Value;
        }
    }
}
