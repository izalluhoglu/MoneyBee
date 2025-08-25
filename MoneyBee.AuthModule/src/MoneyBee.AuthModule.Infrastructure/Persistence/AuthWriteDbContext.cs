using Microsoft.EntityFrameworkCore; 
using MoneyBee.AuthModule.Domain.Entities; 
using MoneyBee.AuthModule.Application.Abstractions;

namespace MoneyBee.AuthModule.Infrastructure.Persistence
{
    /// <summary>Write DbContext (commands) and UnitOfWork.</summary>
    public class AuthWriteDbContext : DbContext, IUnitOfWork
    {
        public AuthWriteDbContext(DbContextOptions<AuthWriteDbContext> options) : base(options) { }
        public DbSet<Employee> Employees => Set<Employee>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(b =>
            {
                b.ToTable("Employees"); b.HasKey(x => x.Id);
                b.Property(x => x.Username).HasMaxLength(100).IsRequired(); b.HasIndex(x => x.Username).IsUnique();
                b.Property(x => x.PasswordHash).HasColumnType("varbinary(32)").IsRequired();
                b.Property(x => x.PasswordSalt).HasColumnType("varbinary(16)").IsRequired();
                b.Property(x => x.IsActive).IsRequired();
            });
        }
        
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
