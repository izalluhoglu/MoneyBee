using MediatR;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.DTOs;
using MoneyBee.CustomerModule.Application.Mappings;

namespace MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomers
{
    /// <summary>Handles customer page retrieval.</summary>
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IReadOnlyList<CustomerDto>>
    {
        private readonly ICustomerReadRepository _readRepo;
        
        public GetCustomersQueryHandler(ICustomerReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IReadOnlyList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetAllAsync(request.Page, request.PageSize);
            
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}