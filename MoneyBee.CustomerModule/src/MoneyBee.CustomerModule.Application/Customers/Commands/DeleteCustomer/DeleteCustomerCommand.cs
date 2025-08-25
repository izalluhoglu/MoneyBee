using MediatR;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.DeleteCustomer
{
    /// <summary>Deletes a customer.</summary>
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}