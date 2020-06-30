using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecondApproachApplication.Repositories;
using SecondApproachDomain;

namespace SecondApproachApplication.Commands
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
        private readonly ISecondGameGameRepository _gameGameRepository;

        public CreateChessGameCommandHandler(ISecondGameGameRepository gameGameRepository)
        {
            _gameGameRepository = gameGameRepository;
        }

        public async Task<CreateChessGameCommandResult> Handle(CreateChessGameCommand request, CancellationToken cancellationToken)
        {

            //validate





            var chessGame = new SecondGame
            {
                RegistrationEndDate = request.RegistrationEndDate,
                MaxPlayersCount = 2,
                MinimalRequiredPlayersCount = 2,
                GameDate = request.GameDate,
                Description = request.Description,
                Guid = Guid.NewGuid()
            };

            await _gameGameRepository.SaveGameAsync(chessGame);
            return new CreateChessGameCommandResult(chessGame.Guid);
        }
    }
}
