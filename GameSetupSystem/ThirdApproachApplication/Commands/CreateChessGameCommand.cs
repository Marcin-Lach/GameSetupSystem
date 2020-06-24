using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;
using ThirdApproachDomain.GameAggregate.GameTypes;

namespace ThirdApproachApplication.Commands
{
    public class CreateChessGameCommand : IRequest<CreateChessGameCommandResult>
    {
        public DateTimeOffset GameDate { get; }
        public string Description { get; }
        public DateTimeOffset RegistrationEndDate { get; }

        public CreateChessGameCommand(
            DateTimeOffset gameDate, 
            string description, 
            DateTimeOffset registrationEndDate)
        {
            GameDate = gameDate;
            Description = description;
            RegistrationEndDate = registrationEndDate;
        }
    }

    public class CreateChessGameCommandResult
    {
        public Guid GameGuid { get; }

        public CreateChessGameCommandResult(Guid gameGuid)
        {
            GameGuid = gameGuid;
        }
    }

    public class CreateChessGameCommandHandler : IRequestHandler<CreateChessGameCommand, CreateChessGameCommandResult>
    {
        private readonly IThirdGameGameRepository _gameGameRepository;

        public CreateChessGameCommandHandler(IThirdGameGameRepository gameGameRepository)
        {
            _gameGameRepository = gameGameRepository;
        }

        public async Task<CreateChessGameCommandResult> Handle(CreateChessGameCommand request, CancellationToken cancellationToken)
        {
            var chessGame = new ChessGame(request.GameDate, request.Description, request.RegistrationEndDate);
            await _gameGameRepository.SaveGameAsync(chessGame);
            return new CreateChessGameCommandResult(chessGame.Guid);
        }
    }
}
