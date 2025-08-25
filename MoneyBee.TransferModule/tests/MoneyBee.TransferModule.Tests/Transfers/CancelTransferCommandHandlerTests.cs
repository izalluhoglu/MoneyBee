using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.Enums;
using MoneyBee.TransferModule.Application.Transfers.Commands.CancelTransfer;
using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Tests.Transfers
{
    [TestFixture]
    public class CancelTransferCommandHandlerTests
    {
        [Test]
        public async Task Should_Success_When_Entity_Exists_And_Not_Cancelled()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var readRepo = new Mock<ITransferReadRepository>();
            var uow = new Mock<IUnitOfWork>();
            
            writeRepo.SetupGet(w => w.UnitOfWork).Returns(uow.Object);
            uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var id = Guid.NewGuid();
            readRepo.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new Transfer { Id = id, Status = (int)TransactionStatus.Active });

            var handler = new CancelTransferCommandHandler(writeRepo.Object, readRepo.Object);
            var result = await handler.Handle(new CancelTransferCommand(id), CancellationToken.None);

            result.Should().BeTrue();
            writeRepo.Verify(w => w.UpdateAsync(It.IsAny<Transfer>()), Times.Once);
            uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Failed_When_Not_Found()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var readRepo = new Mock<ITransferReadRepository>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Transfer)null);

            var handler = new CancelTransferCommandHandler(writeRepo.Object, readRepo.Object);
            var result = await handler.Handle(new CancelTransferCommand(id), CancellationToken.None);

            result.Should().BeFalse();
            writeRepo.Verify(w => w.UpdateAsync(It.IsAny<Transfer>()), Times.Never);
        }

        [Test]
        public async Task Should_Failed_When_Already_Cancelled()
        {
            var writeRepo = new Mock<ITransferWriteRepository>();
            var readRepo = new Mock<ITransferReadRepository>();
            var id = Guid.NewGuid();

            readRepo.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new Transfer { Id = id, Status = (int)TransactionStatus.Canceled });

            var handler = new CancelTransferCommandHandler(writeRepo.Object, readRepo.Object);
            var result = await handler.Handle(new CancelTransferCommand(id), CancellationToken.None);

            result.Should().BeFalse();
            writeRepo.Verify(w => w.UpdateAsync(It.IsAny<Transfer>()), Times.Never);
        }
    }
}