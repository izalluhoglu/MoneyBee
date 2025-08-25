using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Auth.Commands.Login
{
    /// <summary>Authenticates a user and returns a JWT.</summary>
    public record LoginCommand(string Username, string Password) : IRequest<AuthResultDto>;
}
