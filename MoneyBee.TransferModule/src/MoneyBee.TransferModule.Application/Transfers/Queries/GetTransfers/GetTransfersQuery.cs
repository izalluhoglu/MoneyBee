using MediatR;
using MoneyBee.TransferModule.Application.DTOs;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetTransfers
{
    /// <summary>Gets a page of transfers.</summary>
    public record GetTransfersQuery(int Page = 1, int PageSize = 50) : IRequest<IReadOnlyList<TransferDto>>;
}
