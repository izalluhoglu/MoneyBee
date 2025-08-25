using MediatR;
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Auth.Commands.Login
{
    /// <summary>Handles user login and token generation.</summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
    {
        private readonly IEmployeeReadRepository _readRepo; 
        private readonly IPasswordHasher _hasher; 
        private readonly IJwtTokenService _jwt;
        
        public LoginCommandHandler(IEmployeeReadRepository readRepo, IPasswordHasher hasher, IJwtTokenService jwt)
        {
            _readRepo = readRepo; 
            _hasher = hasher; 
            _jwt = jwt;
        }
        
        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _readRepo.GetByUsernameAsync(request.Username.Trim());
            
            if (user is null || !user.IsActive) 
                throw new InvalidOperationException("Invalid credentials.");
            
            var isVerified = _hasher.Verify(request.Password, user.PasswordSalt, user.PasswordHash);
            
            if (!isVerified) 
                throw new InvalidOperationException("Invalid credentials.");
            
            return _jwt.GenerateToken(user);
        }
    }
}