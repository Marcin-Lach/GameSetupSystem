using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirdApproachApplication.Commands;
using ThirdApproachApplication.Queries;
using ThirdApproachDomain.GameAggregate;

namespace GameSetupSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ThirdApproachController : ControllerBase
    {
        private readonly ILogger<ThirdApproachController> _logger;
        private readonly IMediator _mediator;

        public ThirdApproachController(
            ILogger<ThirdApproachController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChessGame()
        {
            var result = await _mediator.Send(
                new CreateChessGameCommand(
                    DateTimeProvider.Now.AddDays(7),
                    "",
                    DateTimeProvider.Now.AddDays(6)));
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSoccerGame()
        {
            var result = await _mediator.Send(
                new CreateSoccerGameCommand(
                    DateTimeProvider.Now.AddDays(7),
                    "",
                    DateTimeProvider.Now.AddDays(6)));
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketballGame()
        {
            var result = await _mediator.Send(
                new CreateBasketballGameCommand(
                    DateTimeProvider.Now.AddDays(7),
                    "",
                    DateTimeProvider.Now.AddDays(6)));
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGame([FromQuery] Guid gameGuid)
        {
            var result = await _mediator.Send(
                new GetGameQuery(gameGuid));
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPlayerForGame([FromBody] RegisterPlayerForGame request)
        {
            await _mediator.Send(
                new RegisterPlayerForGameCommand(
                    request.GameGuid,
                    request.PlayerGuid));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnregisterPlayerFromGame([FromBody] UnregisterPlayerFromGameRequest request)
        {
            await _mediator.Send(
                new UnregisterPlayerFromGameCommand(
                    request.GameGuid,
                    request.PlayerGuid));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var result = await _mediator.Send(new GetPlayersQuery());
            return new JsonResult(result);
        }
    }

    public class RegisterPlayerForGame
    {
        public Guid GameGuid { get; set; }
        public Guid PlayerGuid { get; set; }
    }

    public class UnregisterPlayerFromGameRequest
    {
        public Guid GameGuid { get; set; }
        public Guid PlayerGuid { get; set; }
    }
}
