using System; 
using System.Threading; 
using System.Threading.Tasks; 
using FluentAssertions;
using Moq; 
using NUnit.Framework;
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Application.Auth.Commands.Login; 
using MoneyBee.AuthModule.Application.DTOs; 
using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Tests.Auth
{
    [TestFixture]
    public class LoginCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Credentials_Are_Valid()
        {
            var readRepo = new Mock<IEmployeeReadRepository>(); 
            var hasher = new Mock<IPasswordHasher>(); var jwt = new Mock<IJwtTokenService>();
            var user = new Employee { Id = Guid.NewGuid(), Username = "test", IsActive = true, PasswordSalt = new byte[16], PasswordHash = new byte[32] };
            
            readRepo.Setup(r => r.GetByUsernameAsync("test")).ReturnsAsync(user);
            hasher.Setup(h => h.Verify("P@ssw0rd", user.PasswordSalt, user.PasswordHash)).Returns(true);
            jwt.Setup(j => j.GenerateToken(user)).Returns(new AuthResultDto { AccessToken = "token", ExpiresAtUtc = DateTime.UtcNow.AddHours(1) });
            
            var handler = new LoginCommandHandler(readRepo.Object, hasher.Object, jwt.Object);
            
            var dto = await handler.Handle(new LoginCommand("test", "P@ssw0rd"), CancellationToken.None);
            dto.AccessToken.Should().NotBeNullOrEmpty();
        }
        
        [Test]
        public void Should_Failed_When_Wrong_Password()
        {
            var readRepo = new Mock<IEmployeeReadRepository>(); 
            var hasher = new Mock<IPasswordHasher>(); 
            var jwt = new Mock<IJwtTokenService>();
            var user = new Employee { Id = Guid.NewGuid(), Username = "test", IsActive = true, PasswordSalt = new byte[16], PasswordHash = new byte[32] };
            
            readRepo.Setup(r => r.GetByUsernameAsync("test")).ReturnsAsync(user); hasher.Setup(h => h.Verify("wrong", user.PasswordSalt, user.PasswordHash)).Returns(false);
            
            var handler = new LoginCommandHandler(readRepo.Object, hasher.Object, jwt.Object);
            
            var act = async () => await handler.Handle(new LoginCommand("test", "wrong"), CancellationToken.None);
            act.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}