using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyBee.AuthModule.Application.Abstractions;
using MoneyBee.AuthModule.Application.DTOs;
using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Infrastructure.Auth
{
    /// <summary>HS256 JWT generation and validation.</summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _options;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly SigningCredentials _credentials;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            _credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        }

        public AuthResultDto GenerateToken(Employee employee)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_options.ExpiresMinutes);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, employee.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _credentials);

            var encoded = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResultDto { AccessToken = encoded, ExpiresAtUtc = expires };
        }

        public TokenValidationResultDto ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = _options.ValidateIssuer,
                    ValidateAudience = _options.ValidateAudience,
                    ValidateLifetime = _options.ValidateLifetime,
                    ValidateIssuerSigningKey = _options.ValidateIssuerSigningKey,
                    ValidIssuer = _options.Issuer,
                    ValidAudience = _options.Audience,
                    IssuerSigningKey = _signingKey
                }, out var securityToken);

                var jwt = (JwtSecurityToken)securityToken;

                var sub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var username = principal.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

                return new TokenValidationResultDto
                {
                    IsValid = true,
                    Subject = sub,
                    Username = username,
                    ExpiresAtUtc = jwt.ValidTo
                };
            }
            catch (Exception ex)
            {
                return new TokenValidationResultDto
                {
                    IsValid = false,
                    Error = ex.Message
                };
            }
        }
    }
}