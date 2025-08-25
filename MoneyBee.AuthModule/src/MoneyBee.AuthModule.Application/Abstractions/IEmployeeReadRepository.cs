using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Application.Abstractions
{
    /// <summary>Read-side repository for employees.</summary>
    public interface IEmployeeReadRepository
    {
        /// <summary>Gets an employee by Id.</summary>
        Task<Employee> GetByIdAsync(Guid id);
        
        /// <summary>Gets an employee by username.</summary>
        Task<Employee> GetByUsernameAsync(string username);
        
        /// <summary>Gets a page of employees.</summary>
        Task<List<Employee>> GetAllAsync(int page, int pageSize);
    }
}