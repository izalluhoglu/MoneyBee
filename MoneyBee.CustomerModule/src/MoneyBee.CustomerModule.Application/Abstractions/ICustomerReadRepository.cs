using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Application.Abstractions
{
    /// <summary>Read-side operations for querying customers.</summary>
    public interface ICustomerReadRepository
    {
        /// <summary>Gets a customer by its unique identifier.</summary>
        Task<Customer> GetByIdAsync(Guid id);
        
        /// <summary>Gets a customer by unique ID number.</summary>
        Task<Customer> GetByIdNumberAsync(string idNumber);
        
        /// <summary>Gets a paged list of customers.</summary>
        Task<IReadOnlyList<Customer>> GetAllAsync(int page, int pageSize);
    }
}