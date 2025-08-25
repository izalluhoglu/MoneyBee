using Microsoft.EntityFrameworkCore;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.Enums;
using MoneyBee.TransferModule.Domain.Entities;
using MoneyBee.TransferModule.Infrastructure.Persistence;

namespace MoneyBee.TransferModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based read repository for transfers.</summary>
    public class TransferReadRepository : ITransferReadRepository
    {
        private readonly TransferReadDbContext _ctx;
        
        public TransferReadRepository(TransferReadDbContext ctx) => _ctx = ctx;

        public async Task<List<Transfer>> GetAllAsync(int page, int pageSize)
        {
            if (page < 1) 
                page = 1;
            
            if (pageSize < 1) 
                pageSize = 50;
            
            return await _ctx.Transfers
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Transfer> GetByIdAsync(Guid id) =>
            _ctx.Transfers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<decimal> GetTotalSentTodayAsync(Guid senderCustomerId)
        {
            var today = DateTime.UtcNow.Date;
            
            return await _ctx.Transfers.AsNoTracking()
                .Where(x => 
                    x.SenderCustomerId == senderCustomerId && 
                    x.CreatedAtUtc >= today && 
                    x.Status == (int)TransactionStatus.Active)
                .SumAsync(x => (decimal?)x.Amount) ?? 0m;
        }

        public async Task<(int Count, decimal TotalAmount, decimal TotalFee)> GetDailyStatsBySenderAsync(Guid senderCustomerId)
        {
            var today = DateTime.UtcNow.Date;

            var q = _ctx.Transfers
                .AsNoTracking()
                .Where(x =>
                    x.SenderCustomerId == senderCustomerId &&
                    x.CreatedAtUtc >= today &&
                    x.Status == (int)TransactionStatus.Active);

            var count = await q.CountAsync();
            var totalAmount = await q.SumAsync(x => (decimal?)x.Amount) ?? 0m;
            var totalFee = await q.SumAsync(x => (decimal?)x.Fee) ?? 0m;
            
            return (count, totalAmount, totalFee);
        }
    }
}
