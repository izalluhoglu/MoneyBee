using Microsoft.EntityFrameworkCore;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Infrastructure.Persistence
{
    /// <summary>Read-only DbContext (queries).</summary>
    public class CustomerReadDbContext : DbContext
    {
        public CustomerReadDbContext(DbContextOptions<CustomerReadDbContext> options) : base(options) { }
        
        public DbSet<Customer> Customers => Set<Customer>();
        
        public override int SaveChanges() => throw new InvalidOperationException("Read-only context does not support SaveChanges.");
        
        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default) => 
            throw new InvalidOperationException("Read-only context does not support SaveChanges.");
    }
}