using MediatR;
using MoneyBee.TransferModule.Application.DTOs;

namespace MoneyBee.TransferModule.Application.Transfers.Commands.CreateTransfer
{
    /// <summary>Creates a new transfer.</summary>
    public record CreateTransferCommand(
        Guid SenderCustomerId,
        Guid ReceiverCustomerId,
        decimal Amount,
        decimal Fee,
        string Code
    ) : IRequest<TransferDto>;
}
