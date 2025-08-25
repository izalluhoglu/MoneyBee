using Microsoft.EntityFrameworkCore;
using MoneyBee.TransferModule.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace MoneyBee.TransferModule.Infrastructure.Persistence
{
    /// <summary>Read-only DbContext (queries).</summary>
    public class TransferReadDbContext : DbContext
    {
        public TransferReadDbContext(DbContextOptions<TransferReadDbContext> options) : base(options) { }
        public DbSet<Transfer> Transfers => Set<Transfer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>(b =>
            {
                b.ToTable("Transfers");
                b.HasKey(x => x.Id);
                b.Property(x => x.Amount).HasColumnType("decimal(18,2)");
                b.Property(x => x.Fee).HasColumnType("decimal(18,2)");
                b.Property(x => x.Code).HasMaxLength(64).IsRequired();
                b.HasIndex(x => x.Code);
                b.HasIndex(x => x.SenderCustomerId);
                b.HasIndex(x => x.ReceiverCustomerId);
            });
        }

        public override int SaveChanges()
            => throw new InvalidOperationException("Read-only context does not support SaveChanges.");

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
            => throw new InvalidOperationException("Read-only context does not support SaveChanges.");
    }
}
