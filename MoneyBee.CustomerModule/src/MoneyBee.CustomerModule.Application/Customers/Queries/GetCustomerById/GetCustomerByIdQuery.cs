using MediatR;
using MoneyBee.CustomerModule.Application.DTOs;

namespace MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomerById
{
    /// <summary>Gets a single customer by Id.</summary>
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
}