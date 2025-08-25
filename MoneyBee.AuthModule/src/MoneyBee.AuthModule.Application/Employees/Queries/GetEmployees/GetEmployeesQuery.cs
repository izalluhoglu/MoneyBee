using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Employees.Queries.GetEmployees
{
    /// <summary>Gets a page of employees.</summary>
    public record GetEmployeesQuery(int Page = 1, int PageSize = 50) : IRequest<List<EmployeeDto>>;
}