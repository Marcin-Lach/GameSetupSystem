using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;

namespace ThirdApproachApplication.Commands
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
        private readonly IThirdGameGameRepository _gameGameRepository;
        private readonly IThirdPlayerRepository _playerRepository;

        public RegisterPlayerForGameCommandHandler(
            IThirdGameGameRepository gameGameRepository,
            IThirdPlayerRepository playerRepository)
        {
            _gameGameRepository = gameGameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(RegisterPlayerForGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameGameRepository.GetGameAsync(request.GameGuid);
            var player = await _playerRepository.GetPlayerAsync(request.PlayerGuid);

            game.RegisterPlayer(player);
            return Unit.Value;
        }
    }
}
