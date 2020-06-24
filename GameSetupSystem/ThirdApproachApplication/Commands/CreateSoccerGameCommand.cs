using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;
using ThirdApproachDomain.GameAggregate.GameTypes;

namespace ThirdApproachApplication.Commands
{
    public class CreateSoccerGameCommand : IRequest<CreateSoccerGameCommandResult>
    {
        public DateTimeOffset GameDate { get; }
        public string Description { get; }
        public DateTimeOffset RegistrationEndDate { get; }

        public CreateSoccerGameCommand(
            DateTimeOffset gameDate, 
            string description, 
            DateTimeOffset registrationEndDate)
        {
            GameDate = gameDate;
            Description = description;
            RegistrationEndDate = registrationEndDate;
        }
    }

    public class CreateSoccerGameCommandResult
    {
        public Guid GameGuid { get; }

        public CreateSoccerGameCommandResult(Guid gameGuid)
        {
            GameGuid = gameGuid;
        }
    }

    public class CreateSoccerGameCommandHandler : IRequestHandler<CreateSoccerGameCommand, CreateSoccerGameCommandResult>
    {
        private readonly IThirdGameGameRepository _gameGameRepository;

        public CreateSoccerGameCommandHandler(IThirdGameGameRepository gameGameRepository)
        {
            _gameGameRepository = gameGameRepository;
        }

        public async Task<CreateSoccerGameCommandResult> Handle(CreateSoccerGameCommand request, CancellationToken cancellationToken)
        {
            var chessGame = new SoccerGame(request.GameDate, request.Description, request.RegistrationEndDate);
            await _gameGameRepository.SaveGameAsync(chessGame);
            return new CreateSoccerGameCommandResult(chessGame.Guid);
        }
    }
}
