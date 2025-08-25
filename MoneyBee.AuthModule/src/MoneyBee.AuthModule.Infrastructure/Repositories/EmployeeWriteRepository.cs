using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Domain.Entities; 
using MoneyBee.AuthModule.Infrastructure.Persistence;

namespace MoneyBee.AuthModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based write repository for employees.</summary>
    public class EmployeeWriteRepository : IEmployeeWriteRepository
    {
        private readonly AuthWriteDbContext _ctx;
        
        public EmployeeWriteRepository(AuthWriteDbContext ctx) => _ctx = ctx;
        
        public IUnitOfWork UnitOfWork => _ctx;

        public async Task AddAsync(Employee entity)
        {
            await _ctx.Employees.AddAsync(entity);
            
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _ctx.Employees.Update(entity);
            
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee entity)
        {
            _ctx.Employees.Remove(entity);
            
            await _ctx.SaveChangesAsync();
        }
    }
}