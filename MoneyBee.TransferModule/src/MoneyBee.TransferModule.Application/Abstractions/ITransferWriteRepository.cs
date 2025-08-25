using MoneyBee.TransferModule.Domain.Entities;

namespace MoneyBee.TransferModule.Application.Abstractions
{
    /// <summary>Write-side repository for transfers.</summary>
    public interface ITransferWriteRepository
    {
        /// <summary>Adds a new transfer entity.</summary>
        Task AddAsync(Transfer entity);

        /// <summary>Updates an existing transfer entity.</summary>
        Task UpdateAsync(Transfer entity);

        /// <summary>Unit-of-work accessor.</summary>
        IUnitOfWork UnitOfWork { get; }
    }
}