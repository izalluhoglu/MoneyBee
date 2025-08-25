using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.Transfers.Commands.CreateTransfer;
using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Tests.Transfers
{
    [TestFixture]
    public class CreateTransferCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Input_Is_Valid()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var uow = new Mock<IUnitOfWork>();
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new CreateTransferCommandHandler(writeRepo.Object);
            var cmd = new CreateTransferCommand(Guid.NewGuid(), Guid.NewGuid(), 100m, 5m, "ABC123");

            var dto = await handler.Handle(cmd, CancellationToken.None);

            dto.Should().NotBeNull();
            dto.Amount.Should().Be(100m);
            writeRepo.Verify(w => w.AddAsync(It.IsAny<Transfer>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void Should_Failed_When_Amount_Is_Not_Positive()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var handler = new CreateTransferCommandHandler(writeRepo.Object);
            var cmd = new CreateTransferCommand(Guid.NewGuid(), Guid.NewGuid(), 0m, 5m, "ABC123");

            var act = async () => await handler.Handle(cmd, CancellationToken.None);
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*positive*");
        }

        [Test]
        public void Should_Failed_When_Sender_Equals_Receiver()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var id = Guid.NewGuid();
            var handler = new CreateTransferCommandHandler(writeRepo.Object);
            var cmd = new CreateTransferCommand(id, id, 100m, 5m, "ABC123");

            var act = async () => await handler.Handle(cmd, CancellationToken.None);
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*cannot be the same*");
        }
    }
}