using MediatR;
using MoneyBee.TransferModule.Application.DTOs;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetDailyStatsBySender
{
    /// <summary>Gets today's totals for a sender.</summary>
    public record GetDailyStatsBySenderQuery(Guid SenderCustomerId) : IRequest<TransferDailyStatsDto>;
}
