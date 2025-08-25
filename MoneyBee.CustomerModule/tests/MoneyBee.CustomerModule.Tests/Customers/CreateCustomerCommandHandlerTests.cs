using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.Customers.Commands.CreateCustomer;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Tests.Customers
{
    [TestFixture]
    public class CreateCustomerCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Input_Is_Valid_And_Id_Number_Is_Unique()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();

            readRepo.Setup(r => r.GetByIdNumberAsync(It.IsAny<string>())).ReturnsAsync((Customer)null);
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new CreateCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var cmd = new CreateCustomerCommand("Test","User","+905551112233","London", new System.DateTime(1815,12,10),"ID-123");

            var dto = await handler.Handle(cmd, CancellationToken.None);

            dto.Should().NotBeNull();
            dto.FirstName.Should().Be("Test");
            writeRepo.Verify(w => w.AddAsync(It.IsAny<Customer>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void Should_Failed_When_IdNumber_Already_Exists()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();

            readRepo.Setup(r => r.GetByIdNumberAsync(It.IsAny<string>())).ReturnsAsync(new Customer());
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);

            var handler = new CreateCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var cmd = new CreateCustomerCommand("Test","User","+905551112233","London", new System.DateTime(1815,12,10),"ID-123");

            var act = async () => await handler.Handle(cmd, CancellationToken.None);
            act.Should().ThrowAsync<System.InvalidOperationException>().WithMessage("*already exists*");
        }
    }
}