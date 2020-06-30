using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SecondApproachApplication.Commands
{
    public class CreateSoccerGameCommand : IRequest<CreateSoccerGameCommandResult>
    {
    }

    public class CreateSoccerGameCommandResult
    { }

    public class CreateSoccerGameCommandHandler : IRequestHandler<CreateSoccerGameCommand, CreateSoccerGameCommandResult>
    {
        public Task<CreateSoccerGameCommandResult> Handle(CreateSoccerGameCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
