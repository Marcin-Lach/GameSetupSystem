using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;

namespace ThirdApproachApplication.Commands
{
    public class UnregisterPlayerFromGameCommand : IRequest
    {
        public Guid GameGuid { get; }
        public Guid PlayerGuid { get; }

        public UnregisterPlayerFromGameCommand(
            Guid gameGuid, 
            Guid playerGuid)
        {
            GameGuid = gameGuid;
            PlayerGuid = playerGuid;
        }
    }

    public class UnregisterPlayerFromGameCommandHandler : IRequestHandler<UnregisterPlayerFromGameCommand>
    {
        private readonly IThirdGameGameRepository _gameGameRepository;
        private readonly IThirdPlayerRepository _playerRepository;

        public UnregisterPlayerFromGameCommandHandler(
            IThirdGameGameRepository gameGameRepository,
            IThirdPlayerRepository playerRepository)
        {
            _gameGameRepository = gameGameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(UnregisterPlayerFromGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameGameRepository.GetGameAsync(request.GameGuid);
            var player = await _playerRepository.GetPlayerAsync(request.PlayerGuid);

            game.UnregisterPlayer(player);
            return Unit.Value;
        }
    }
}
