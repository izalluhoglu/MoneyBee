using Microsoft.EntityFrameworkCore;
using MoneyBee.CustomerModule.Domain.Entities;
using MoneyBee.CustomerModule.Application.Abstractions;

namespace MoneyBee.CustomerModule.Infrastructure.Persistence
{
    /// <summary>Write DbContext (commands) and UnitOfWork.</summary>
    public class CustomerWriteDbContext : DbContext, IUnitOfWork
    {
        public CustomerWriteDbContext(DbContextOptions<CustomerWriteDbContext> options) : base(options) { }
        
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(b =>
            {
                b.ToTable("Customers");
                b.HasKey(x => x.Id);
                b.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
                b.Property(x => x.LastName).HasMaxLength(128).IsRequired();
                b.Property(x => x.PhoneNumber).HasMaxLength(32).IsRequired();
                b.Property(x => x.Address).HasMaxLength(256).IsRequired();
                b.Property(x => x.IdNumber).HasMaxLength(11).IsRequired();
                b.HasIndex(x => x.IdNumber).IsUnique();
            });
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}