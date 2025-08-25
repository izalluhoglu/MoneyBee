using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Application.Abstractions
{
    /// <summary>Write-side repository for employees.</summary>
    public interface IEmployeeWriteRepository
    {
        /// <summary>Adds a new employee entity.</summary>
        Task AddAsync(Employee entity);
        
        /// <summary>Updates an existing employee entity.</summary>
        Task UpdateAsync(Employee entity);
        
        /// <summary>Deletes an existing employee entity.</summary>
        Task DeleteAsync(Employee entity);
        
        /// <summary>Unit-of-work accessor.</summary>
        IUnitOfWork UnitOfWork { get; }
    }
}