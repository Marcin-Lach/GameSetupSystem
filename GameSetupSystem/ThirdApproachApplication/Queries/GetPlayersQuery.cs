using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThirdApproachApplication.Repositories;
using ThirdApproachDomain;

namespace ThirdApproachApplication.Queries
{
    public class GetPlayersQuery : IRequest<IEnumerable<Player>>
    {
    }

    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, IEnumerable<Player>>
    {
        private readonly IThirdPlayerRepository _repository;

        public GetPlayersQueryHandler(IThirdPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Player>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetPlayersAsync();
            return game;
        }
    }
}
