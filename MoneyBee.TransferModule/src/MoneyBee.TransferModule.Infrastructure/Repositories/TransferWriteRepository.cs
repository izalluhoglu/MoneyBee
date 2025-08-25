using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Domain.Entities;
using MoneyBee.TransferModule.Infrastructure.Persistence;

namespace MoneyBee.TransferModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based write repository for transfers.</summary>
    public class TransferWriteRepository : ITransferWriteRepository
    {
        private readonly TransferWriteDbContext _ctx;
        
        public TransferWriteRepository(TransferWriteDbContext ctx) => _ctx = ctx;

        public IUnitOfWork UnitOfWork => _ctx;

        public async Task AddAsync(Transfer entity)
        {
            await _ctx.Transfers.AddAsync(entity);

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transfer entity)
        {
            _ctx.Transfers.Update(entity); 
            
            await _ctx.SaveChangesAsync();
        }
    }
}
