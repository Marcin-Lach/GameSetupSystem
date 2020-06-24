using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;
using ThirdApproachDomain.GameAggregate.GameTypes;

namespace ThirdApproachApplication.Commands
{
    public class CreateBasketballGameCommand : IRequest<CreateBasketballGameCommandResult>
    {
        public DateTimeOffset GameDate { get; }
        public string Description { get; }
        public DateTimeOffset RegistrationEndDate { get; }

        public CreateBasketballGameCommand(
            DateTimeOffset gameDate, 
            string description, 
            DateTimeOffset registrationEndDate)
        {
            GameDate = gameDate;
            Description = description;
            RegistrationEndDate = registrationEndDate;
        }
    }

    public class CreateBasketballGameCommandResult
    {
        public Guid GameGuid { get; }

        public CreateBasketballGameCommandResult(Guid gameGuid)
        {
            GameGuid = gameGuid;
        }
    }

    public class CreateBasketballGameCommandHandler : IRequestHandler<CreateBasketballGameCommand, CreateBasketballGameCommandResult>
    {
        private readonly IThirdGameGameRepository _gameGameRepository;

        public CreateBasketballGameCommandHandler(IThirdGameGameRepository gameGameRepository)
        {
            _gameGameRepository = gameGameRepository;
        }

        public async Task<CreateBasketballGameCommandResult> Handle(CreateBasketballGameCommand request, CancellationToken cancellationToken)
        {
            var chessGame = new BasketballGame(request.GameDate, request.Description, request.RegistrationEndDate);
            await _gameGameRepository.SaveGameAsync(chessGame);
            return new CreateBasketballGameCommandResult(chessGame.Guid);
        }
    }
}
