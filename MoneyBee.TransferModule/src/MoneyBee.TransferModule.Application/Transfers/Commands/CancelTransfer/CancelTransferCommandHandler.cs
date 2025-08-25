using MediatR;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.Enums;

namespace MoneyBee.TransferModule.Application.Transfers.Commands.CancelTransfer
{
    /// <summary>Handles transfer cancellation.</summary>
    public class CancelTransferCommandHandler : IRequestHandler<CancelTransferCommand, bool>
    {
        private readonly ITransferWriteRepository _writeRepo;
        private readonly ITransferReadRepository _readRepo;

        public CancelTransferCommandHandler(ITransferWriteRepository writeRepo, ITransferReadRepository readRepo)
        {
            _writeRepo = writeRepo;
            _readRepo = readRepo;
        }

        public async Task<bool> Handle(CancelTransferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id);
            
            if (entity is null) 
                return false;
            
            if (entity.Status == (int)TransactionStatus.Canceled) 
                return false; 

            entity.Status = (int)TransactionStatus.Canceled;
            entity.CancelledAtUtc = DateTime.UtcNow;
            
            await _writeRepo.UpdateAsync(entity);
            
            var affected = await _writeRepo.UnitOfWork.SaveChangesAsync();
            
            return affected > 0;
        }
    }
}