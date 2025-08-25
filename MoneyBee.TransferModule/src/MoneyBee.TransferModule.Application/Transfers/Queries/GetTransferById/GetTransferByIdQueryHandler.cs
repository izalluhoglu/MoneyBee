using MediatR;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.DTOs;
using MoneyBee.TransferModule.Application.Mappings;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetTransferById
{
    /// <summary>Handles retrieval of a transfer by Id.</summary>
    public class GetTransferByIdQueryHandler : IRequestHandler<GetTransferByIdQuery, TransferDto>
    {
        private readonly ITransferReadRepository _readRepo;
        
        public GetTransferByIdQueryHandler(ITransferReadRepository readRepo) => _readRepo = readRepo;

        public async Task<TransferDto> Handle(GetTransferByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id);
            
            return entity?.ToDto();
        }
    }
}