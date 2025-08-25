using MediatR;
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Auth.Queries.ValidateToken
{
    /// <summary>Handles token validation.</summary>
    public class ValidateTokenQueryHandler : IRequestHandler<ValidateTokenQuery, TokenValidationResultDto>
    {
        private readonly IJwtTokenService _jwt; 
        public ValidateTokenQueryHandler(IJwtTokenService jwt) => _jwt = jwt;
        
        public Task<TokenValidationResultDto> Handle(ValidateTokenQuery request, CancellationToken cancellationToken) 
            => Task.FromResult(_jwt.ValidateToken(request.Token));
    }
}
