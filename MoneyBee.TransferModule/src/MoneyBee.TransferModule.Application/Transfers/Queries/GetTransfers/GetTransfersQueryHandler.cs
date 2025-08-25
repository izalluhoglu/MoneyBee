using MediatR;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.DTOs;
using MoneyBee.TransferModule.Application.Mappings;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetTransfers
{
    /// <summary>Handles page retrieval of transfers.</summary>
    public class GetTransfersQueryHandler : IRequestHandler<GetTransfersQuery, IReadOnlyList<TransferDto>>
    {
        private readonly ITransferReadRepository _readRepo;
        public GetTransfersQueryHandler(ITransferReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IReadOnlyList<TransferDto>> Handle(GetTransfersQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetAllAsync(request.Page, request.PageSize);
            
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}