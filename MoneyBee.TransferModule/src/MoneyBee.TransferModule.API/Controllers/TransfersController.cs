using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyBee.TransferModule.Application.Transfers.Commands.CancelTransfer;
using MoneyBee.TransferModule.Application.Transfers.Commands.CreateTransfer;
using MoneyBee.TransferModule.Application.Transfers.Queries.GetDailyStatsBySender;
using MoneyBee.TransferModule.Application.Transfers.Queries.GetTransferById;
using MoneyBee.TransferModule.Application.Transfers.Queries.GetTransfers;

namespace MoneyBee.TransferModule.API.Controllers
{
    /// <summary>Transfer endpoints (secured).</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransfersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TransfersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Gets a transfer by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetTransferByIdQuery(id)));
        }

        /// <summary>Gets a page of transfers.</summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            return Ok(await _mediator.Send(new GetTransfersQuery(page, pageSize)));
        }

        /// <summary>Creates a new transfer.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransferCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Cancels a transfer.</summary>
        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            return Ok(await _mediator.Send(new CancelTransferCommand(id)));
        }

        /// <summary>Gets today's totals for a sender.</summary>
        [HttpGet("stats/sender/{senderId:guid}/today")]
        public async Task<IActionResult> GetDailyStats(Guid senderId)
        {
            return Ok(await _mediator.Send(new GetDailyStatsBySenderQuery(senderId)));
        }
    }
}