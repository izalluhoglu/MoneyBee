using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyBee.CustomerModule.Application.Customers.Commands.CreateCustomer;
using MoneyBee.CustomerModule.Application.Customers.Commands.DeleteCustomer;
using MoneyBee.CustomerModule.Application.Customers.Commands.UpdateCustomer;
using MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomerById;
using MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomers;

namespace MoneyBee.CustomerModule.Controllers
{
    /// <summary>Customer endpoints.</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CustomersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Gets a customer by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
        }

        /// <summary>Gets a page of customers.</summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            return Ok(await _mediator.Send(new GetCustomersQuery(page, pageSize)));
        }

        /// <summary>Creates a new customer.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Updates an existing customer.</summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id) 
                return BadRequest("Route id and body id must match.");
            
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Deletes a customer.</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerCommand(id)));
        }
    }
}