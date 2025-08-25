using MediatR;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.DTOs;
using MoneyBee.CustomerModule.Application.Mappings;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.CreateCustomer
{
    /// <summary>Handles customer creation.</summary>
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerWriteRepository _writeRepo;
        private readonly ICustomerReadRepository _readRepo;

        public CreateCustomerCommandHandler(ICustomerWriteRepository writeRepo, ICustomerReadRepository readRepo)
        {
            _writeRepo = writeRepo;
            _readRepo = readRepo;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var existing = await _readRepo.GetByIdNumberAsync(request.IdNumber);
            
            if (existing is not null)
                throw new InvalidOperationException("A customer with the same ID number already exists.");

            var entity = new Customer
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.PhoneNumber.Trim(),
                Address = request.Address.Trim(),
                DateOfBirth = request.DateOfBirth.Date,
                IdNumber = request.IdNumber.Trim()
            };

            await _writeRepo.AddAsync(entity);
            await _writeRepo.UnitOfWork.SaveChangesAsync();
            
            return entity.ToDto();
        }
    }
}