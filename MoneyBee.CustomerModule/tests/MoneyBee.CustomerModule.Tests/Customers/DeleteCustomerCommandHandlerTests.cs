using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.Customers.Commands.DeleteCustomer;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Tests.Customers
{
    [TestFixture]
    public class DeleteCustomerCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Entity_Exists()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Customer { Id = id });
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new DeleteCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var ok = await handler.Handle(new DeleteCustomerCommand(id), CancellationToken.None);

            ok.Should().BeTrue();
            writeRepo.Verify(w => w.DeleteAsync(It.IsAny<Customer>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Failed_When_Entity_Not_Found()
        {
            var writeRepo = new Mock<ICustomerWriteRepository>();
            var readRepo = new Mock<ICustomerReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Customer)null);
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);

            var handler = new DeleteCustomerCommandHandler(writeRepo.Object, readRepo.Object);
            var ok = await handler.Handle(new DeleteCustomerCommand(id), CancellationToken.None);

            ok.Should().BeFalse();
            writeRepo.Verify(w => w.DeleteAsync(It.IsAny<Customer>()), Times.Never);
            uow.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}