using MediatR;
using MoneyBee.CustomerModule.Application.DTOs;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.CreateCustomer
{
    /// <summary>Creates a new customer.</summary>
    public record CreateCustomerCommand(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        DateTime DateOfBirth,
        string IdNumber
    ) : IRequest<CustomerDto>;
}