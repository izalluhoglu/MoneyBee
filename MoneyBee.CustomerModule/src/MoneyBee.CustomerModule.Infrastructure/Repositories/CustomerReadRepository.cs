using Microsoft.EntityFrameworkCore;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Domain.Entities;
using MoneyBee.CustomerModule.Infrastructure.Persistence;

namespace MoneyBee.CustomerModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based read repository.</summary>
    public class CustomerReadRepository : ICustomerReadRepository
    {
        private readonly CustomerReadDbContext _ctx;
        
        public CustomerReadRepository(CustomerReadDbContext ctx) => _ctx = ctx;

        public async Task<IReadOnlyList<Customer>> GetAllAsync(int page, int pageSize)
        {
            if (page < 1) 
                page = 1;
            
            if (pageSize < 1) 
                pageSize = 50;
            
            return await _ctx.Customers
                .OrderBy(x => x.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Customer> GetByIdAsync(Guid id) =>
            _ctx.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public Task<Customer> GetByIdNumberAsync(string idNumber) =>
            _ctx.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.IdNumber == idNumber);
    }
}