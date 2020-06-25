using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecondApproachApplication.Repositories;
using SecondApproachDomain;

namespace SecondApproachApplication.Queries
{
    public class GetGameQuery : IRequest<SecondGame>
    {
        public Guid GameGuid { get; }

        public GetGameQuery(Guid gameGuid)
        {
            GameGuid = gameGuid;
        }
    }

    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, SecondGame>
    {
        private readonly ISecondGameGameRepository _repository;

        public GetGameQueryHandler(ISecondGameGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<SecondGame> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetGameAsync(request.GameGuid);
            return game;
        }
    }
}
