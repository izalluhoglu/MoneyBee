using MediatR;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.DTOs;
using MoneyBee.CustomerModule.Application.Mappings;

namespace MoneyBee.CustomerModule.Application.Customers.Queries.GetCustomerById
{
    /// <summary>Handles retrieval of a customer by Id.</summary>
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerReadRepository _readRepo;
        
        public GetCustomerByIdQueryHandler(ICustomerReadRepository readRepo) => _readRepo = readRepo;

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id);
            
            return entity?.ToDto();
        }
    }
}