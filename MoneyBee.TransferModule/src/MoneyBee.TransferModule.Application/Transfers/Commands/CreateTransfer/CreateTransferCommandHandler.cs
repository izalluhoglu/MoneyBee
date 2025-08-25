using MediatR;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.DTOs;
using MoneyBee.TransferModule.Application.Enums;
using MoneyBee.TransferModule.Application.Mappings;
using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Application.Transfers.Commands.CreateTransfer
{
    /// <summary>Handles transfer creation.</summary>
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, TransferDto>
    {
        private readonly ITransferWriteRepository _writeRepo;

        public CreateTransferCommandHandler(ITransferWriteRepository writeRepo)
        {
            _writeRepo = writeRepo;
        }

        public async Task<TransferDto> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0) 
                throw new InvalidOperationException("Amount must be positive.");
            
            if (request.Fee < 0) 
                throw new InvalidOperationException("Fee cannot be negative.");
            
            if (request.SenderCustomerId == request.ReceiverCustomerId) 
                throw new InvalidOperationException("Sender and receiver cannot be the same.");

            var entity = new Transfer
            {
                SenderCustomerId = request.SenderCustomerId,
                ReceiverCustomerId = request.ReceiverCustomerId,
                Amount = request.Amount,
                Fee = request.Fee,
                Code = request.Code.Trim(),
                Status = (int)TransactionStatus.Active
            };

            await _writeRepo.AddAsync(entity);
            
            await _writeRepo.UnitOfWork.SaveChangesAsync();
            
            return entity.ToDto();
        }
    }
}