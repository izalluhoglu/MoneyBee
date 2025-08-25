using MediatR;
using MoneyBee.CustomerModule.Application.DTOs;

namespace MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomers
{
    /// <summary>Gets a page of customers.</summary>
    public record GetCustomersQuery(int Page = 1, int PageSize = 50) : IRequest<IReadOnlyList<CustomerDto>>;
}