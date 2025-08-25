using MediatR; 
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Employees.Commands.UpdateEmployee
{
    /// <summary>Updates username and optionally password.</summary>
    public record UpdateEmployeeCommand(Guid Id, string Username, string NewPassword) : IRequest<EmployeeDto>;
}