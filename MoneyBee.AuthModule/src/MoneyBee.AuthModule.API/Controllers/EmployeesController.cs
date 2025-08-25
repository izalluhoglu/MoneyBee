using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using MediatR;
using MoneyBee.AuthModule.Application.Employees.Commands.CreateEmployee; 
using MoneyBee.AuthModule.Application.Employees.Commands.UpdateEmployee;
using MoneyBee.AuthModule.Application.Employees.Commands.DeleteEmployee; 
using MoneyBee.AuthModule.Application.Employees.Queries.GetEmployeeById;
using MoneyBee.AuthModule.Application.Employees.Queries.GetEmployees;

namespace MoneyBee.AuthModule.API.Controllers
{
    /// <summary>Employee management endpoints.</summary>
    [ApiController] 
    [Route("api/[controller]")] 
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator; 
        
        public EmployeesController(IMediator mediator) => _mediator = mediator;

        /// <summary>Gets an employee by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
        }

        /// <summary>Gets a page of employees.</summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            return Ok(await _mediator.Send(new GetEmployeesQuery(page, pageSize)));
        }

        /// <summary>Creates a new employee.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        /// <summary>Updates an employee.</summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand cmd)
        {
            if (id != cmd.Id) 
                return BadRequest("Route id and body id must match.");

            return Ok(await _mediator.Send(cmd));
        }

        /// <summary>Deletes an employee.</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand(id)));
        }
    }
}