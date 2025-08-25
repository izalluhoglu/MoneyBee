using MediatR;

namespace MoneyBee.TransferModule.Application.Transfers.Commands.CancelTransfer
{
    /// <summary>Cancels an existing transfer.</summary>
    public record CancelTransferCommand(Guid Id) : IRequest<bool>;
}
