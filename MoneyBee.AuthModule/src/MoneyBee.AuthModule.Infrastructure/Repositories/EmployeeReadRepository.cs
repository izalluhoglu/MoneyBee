using Microsoft.EntityFrameworkCore;
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Domain.Entities; 
using MoneyBee.AuthModule.Infrastructure.Persistence;

namespace MoneyBee.AuthModule.Infrastructure.Repositories
{
    /// <summary>EF Core-based read repository for employees.</summary>
    public class EmployeeReadRepository : IEmployeeReadRepository
    {
        private readonly AuthReadDbContext _ctx;
        
        public EmployeeReadRepository(AuthReadDbContext ctx) => _ctx = ctx;
        
        public async Task<List<Employee>> GetAllAsync(int page, int pageSize)
        { 
            if (page < 1) 
                page = 1;
            
            if (pageSize < 1) 
                pageSize = 50;
            
            return await _ctx.Employees
                .OrderBy(x => x.Username)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(); 
        }
        
        public Task<Employee> GetByIdAsync(Guid id) => _ctx.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        
        public Task<Employee> GetByUsernameAsync(string username) => _ctx.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
    }
}