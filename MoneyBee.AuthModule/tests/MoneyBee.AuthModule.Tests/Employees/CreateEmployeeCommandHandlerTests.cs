using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.AuthModule.Application.Abstractions;
using MoneyBee.AuthModule.Application.Employees.Commands.CreateEmployee;
using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Tests.Employees
{
    [TestFixture]
    public class CreateEmployeeCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Username_IsUnique_And_Password_Provided()
        {
            var writeRepo = new Mock<IEmployeeWriteRepository>();
            var readRepo = new Mock<IEmployeeReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            var hasher = new Mock<IPasswordHasher>();

            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            readRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((Employee)null);
            hasher.Setup(h => h.GenerateSalt(It.IsAny<int>())).Returns(new byte[16]);
            hasher.Setup(h => h.ComputeHash(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(new byte[32]);

            var handler = new CreateEmployeeCommandHandler(writeRepo.Object, readRepo.Object, hasher.Object);
            var dto = await handler.Handle(
                new CreateEmployeeCommand("bob", "P@ssw0rd"), CancellationToken.None);

            dto.Username.Should().Be("bob");
            writeRepo.Verify(w => w.AddAsync(It.IsAny<Employee>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void Should_Failed_When_Username_Already_Exists()
        {
            var writeRepo = new Mock<IEmployeeWriteRepository>();
            var readRepo = new Mock<IEmployeeReadRepository>();
            var hasher = new Mock<IPasswordHasher>();

            readRepo.Setup(r => r.GetByUsernameAsync("bob"))
                .ReturnsAsync(new Employee { Username = "bob" });

            var handler = new CreateEmployeeCommandHandler(writeRepo.Object, readRepo.Object, hasher.Object);

            var act = async () => await handler.Handle(
                new CreateEmployeeCommand("bob", "secret"), CancellationToken.None);
            act.Should().ThrowAsync<System.InvalidOperationException>().WithMessage("*already exists*");
        }
    }
}