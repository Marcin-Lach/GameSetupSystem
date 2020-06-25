using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SecondApproachApplication.Commands
{
    public class CreateBasketballGameCommand : IRequest<CreateBasketballGameCommandResult>
    {
    }

    public class CreateBasketballGameCommandResult
    { }

    public class CreateBasketballGameCommandHandler : IRequestHandler<CreateBasketballGameCommand, CreateBasketballGameCommandResult>
    {
        public Task<CreateBasketballGameCommandResult> Handle(CreateBasketballGameCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
