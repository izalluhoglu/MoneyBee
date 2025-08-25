using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Auth.Queries.ValidateToken
{
    /// <summary>Validates a token and returns principal info.</summary>
    public record ValidateTokenQuery(string Token) : IRequest<TokenValidationResultDto>;
}
