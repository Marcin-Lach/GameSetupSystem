using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;
using ThirdApproachDomain.GameAggregate;

namespace ThirdApproachApplication.Queries
{
    public class GetGameQuery : IRequest<ThirdGame>
    {
        public Guid GameGuid { get; }

        public GetGameQuery(Guid gameGuid)
        {
            GameGuid = gameGuid;
        }
    }

    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, ThirdGame>
    {
        private readonly IThirdGameGameRepository _repository;

        public GetGameQueryHandler(IThirdGameGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<ThirdGame> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetGameAsync(request.GameGuid);
            return game;
        }
    }
}
