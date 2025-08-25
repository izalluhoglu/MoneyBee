using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Domain.Entities;
using MoneyBee.CustomerModule.Infrastructure.Persistence;

namespace MoneyBee.CustomerModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based write repository.</summary>
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        private readonly CustomerWriteDbContext _ctx;
        
        public CustomerWriteRepository(CustomerWriteDbContext ctx) => _ctx = ctx;

        public IUnitOfWork UnitOfWork => _ctx;

        public async Task AddAsync(Customer entity)
        {
            _ctx.Customers.Add(entity); 
            
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _ctx.Customers.Update(entity); 
            
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer entity)
        {
            _ctx.Customers.Remove(entity);

            await _ctx.SaveChangesAsync();
        }
    }
}