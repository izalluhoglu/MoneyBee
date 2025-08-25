using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Application.Abstractions
{
    /// <summary>Read-side repository for transfers.</summary>
    public interface ITransferReadRepository
    {
        /// <summary>Gets a transfer by Id.</summary>
        Task<Transfer> GetByIdAsync(Guid id);

        /// <summary>Gets a page of transfers (1-based page index).</summary>
        Task<List<Transfer>> GetAllAsync(int page, int pageSize);

        /// <summary>Gets total sent amount of the sender in UTC day.</summary>
        Task<decimal> GetTotalSentTodayAsync(Guid senderCustomerId);

        /// <summary>Gets daily stats for the sender (count, total amount, total fee).</summary>
        Task<(int Count, decimal TotalAmount, decimal TotalFee)> GetDailyStatsBySenderAsync(Guid senderCustomerId);
    }
}