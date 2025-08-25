using MoneyBee.AuthModule.Domain.Entities;
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Abstractions
{
    /// <summary>Generates and validates JWT tokens.</summary>
    public interface IJwtTokenService
    {
        /// <summary>Creates a JWT for the given employee.</summary>
        AuthResultDto GenerateToken(Employee employee);
        
        /// <summary>Validates a JWT and returns principal info if valid.</summary>
        TokenValidationResultDto ValidateToken(string token);
    }
}