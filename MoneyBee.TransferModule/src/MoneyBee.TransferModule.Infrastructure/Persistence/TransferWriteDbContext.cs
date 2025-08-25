using Microsoft.EntityFrameworkCore;
using MoneyBee.TransferModule.Domain.Entities;
using MoneyBee.TransferModule.Application.Abstractions;
using System.Threading.Tasks;

namespace MoneyBee.TransferModule.Infrastructure.Persistence
{
    /// <summary>Write DbContext (commands) and UnitOfWork.</summary>
    public class TransferWriteDbContext : DbContext, IUnitOfWork
    {
        public TransferWriteDbContext(DbContextOptions<TransferWriteDbContext> options) : base(options) { }
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

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
