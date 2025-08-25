using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using MediatR;
using MoneyBee.AuthModule.Application.Auth.Commands.Login; 
using MoneyBee.AuthModule.Application.Auth.Queries.ValidateToken;

namespace MoneyBee.AuthModule.API.Controllers
{
    /// <summary>Authentication endpoints.</summary>
    [ApiController] 
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AuthController(IMediator mediator) => _mediator = mediator;

        /// <summary>Authenticates a user and returns a JWT.</summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Validates a JWT and returns basic claim info.</summary>
        [HttpPost("validate")]
        [AllowAnonymous]
        public async Task<IActionResult> Validate([FromBody] ValidateTokenQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}