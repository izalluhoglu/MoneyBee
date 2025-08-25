using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.Customers.Commands.UpdateCustomer;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Tests.Customers
{
    [TestFixture]
    public class UpdateCustomerCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Entity_Exists()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Customer { Id = id, FirstName = "A", LastName = "B" });
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new UpdateCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var cmd = new UpdateCustomerCommand(id, "Test", "User", "+90555", "London", new DateTime(1815,12,10));

            var dto = await handler.Handle(cmd, CancellationToken.None);

            dto.FirstName.Should().Be("Test");
            writeRepo.Verify(w => w.UpdateAsync(It.IsAny<Customer>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void Should_Failed_When_Entity_Not_Found()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Customer)null);
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);

            var handler = new UpdateCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var cmd = new UpdateCustomerCommand(id, "Test", "User", "+90555", "London", new DateTime(1815,12,10));

            var act = async () => await handler.Handle(cmd, CancellationToken.None);
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*not found*");
        }
    }
}