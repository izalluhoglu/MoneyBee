using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Employees.Commands.CreateEmployee
{
    /// <summary>Creates a new employee.</summary>
    public record CreateEmployeeCommand(string Username, string Password, bool IsActive = true) : IRequest<EmployeeDto>;
}
