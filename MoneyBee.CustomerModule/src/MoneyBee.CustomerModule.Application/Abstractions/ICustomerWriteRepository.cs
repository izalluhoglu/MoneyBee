using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Application.Abstractions
{
    /// <summary>Write-side operations for creating/updating/deleting customers.</summary>
    public interface ICustomerWriteRepository
    {
        /// <summary>Adds a new customer entity to the store.</summary>
        Task AddAsync(Customer entity);
        
        /// <summary>Updates an existing customer entity.</summary>
        Task UpdateAsync(Customer entity);
        
        /// <summary>Deletes an existing customer entity.</summary>
        Task DeleteAsync(Customer entity);
        
        /// <summary>Unit of Work for committing changes.</summary>
        IUnitOfWork UnitOfWork { get; }
    }
}