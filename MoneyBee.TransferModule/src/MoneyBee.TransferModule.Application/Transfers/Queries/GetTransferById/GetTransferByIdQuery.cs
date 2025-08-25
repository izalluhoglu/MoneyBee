using MediatR;
using MoneyBee.TransferModule.Application.DTOs;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetTransferById
{
    /// <summary>Gets a transfer by Id.</summary>
    public record GetTransferByIdQuery(Guid Id) : IRequest<TransferDto>;
}
