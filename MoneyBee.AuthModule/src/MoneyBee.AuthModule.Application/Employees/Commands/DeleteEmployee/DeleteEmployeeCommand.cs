using MediatR;

namespace MoneyBee.AuthModule.Application.Employees.Commands.DeleteEmployee
{
    /// <summary>Deletes an employee.</summary>
    public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
}
