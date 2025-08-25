using MoneyBee.TransferModule.Application.DTOs;
using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Application.Mappings
{
    /// <summary>Basic mapper for Transfer &lt;-&gt; DTO.</summary>
    public static class TransferMappings
    {
        /// <summary>Maps entity to DTO.</summary>
        public static TransferDto ToDto(this Transfer entity) => new TransferDto
        {
            Id = entity.Id,
            SenderCustomerId = entity.SenderCustomerId,
            ReceiverCustomerId = entity.ReceiverCustomerId,
            Amount = entity.Amount,
            Fee = entity.Fee,
            Code = entity.Code,
            Status = entity.Status,
            CreatedAtUtc = entity.CreatedAtUtc,
            CancelledAtUtc = entity.CancelledAtUtc
        };
    }
}