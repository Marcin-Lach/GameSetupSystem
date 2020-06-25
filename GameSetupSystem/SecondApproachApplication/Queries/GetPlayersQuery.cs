using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecondApproachApplication.Repositories;
using SecondApproachDomain;

namespace SecondApproachApplication.Queries
{
    public class GetPlayersQuery : IRequest<IEnumerable<Player>>
    {
    }

    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, IEnumerable<Player>>
    {
        private readonly ISecondPlayerRepository _repository;

        public GetPlayersQueryHandler(ISecondPlayerRepository repository)
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
