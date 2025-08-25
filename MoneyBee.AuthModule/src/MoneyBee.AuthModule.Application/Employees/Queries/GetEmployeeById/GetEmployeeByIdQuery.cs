using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Employees.Queries.GetEmployeeById
{
    /// <summary>Gets an employee by Id.</summary>
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto>;
}