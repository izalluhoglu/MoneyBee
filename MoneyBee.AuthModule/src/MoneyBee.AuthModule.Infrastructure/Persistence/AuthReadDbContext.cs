using Microsoft.EntityFrameworkCore; 
using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Infrastructure.Persistence
{
    /// <summary>Read-only DbContext (queries).</summary>
    public class AuthReadDbContext : DbContext
    {
        public AuthReadDbContext(DbContextOptions<AuthReadDbContext> options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();

        public override int SaveChanges() => throw new InvalidOperationException("Read-only context does not support SaveChanges.");
        
        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default) => 
            throw new InvalidOperationException("Read-only context does not support SaveChanges.");
    }
}
