using MediatR;
using MoneyBee.CustomerModule.Application.DTOs;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.UpdateCustomer
{
    /// <summary>Updates core fields of an existing customer.</summary>
    public record UpdateCustomerCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        DateTime DateOfBirth
    ) : IRequest<CustomerDto>;
}