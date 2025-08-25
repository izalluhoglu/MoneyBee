using MediatR;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Application.DTOs;
using MoneyBee.CustomerModule.Application.Mappings;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.UpdateCustomer
{
    /// <summary>Handles customer updates.</summary>
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerWriteRepository _writeRepo;
        private readonly ICustomerReadRepository _readRepo;

        public UpdateCustomerCommandHandler(ICustomerWriteRepository writeRepo, ICustomerReadRepository readRepo)
        {
            _writeRepo = writeRepo;
            _readRepo = readRepo;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _readRepo.GetByIdAsync(request.Id);
            
            if (customer is null)
                throw new InvalidOperationException("Customer not found.");

            customer.FirstName = request.FirstName.Trim();
            customer.LastName = request.LastName.Trim();
            customer.PhoneNumber = request.PhoneNumber.Trim();
            customer.Address = request.Address.Trim();
            customer.DateOfBirth = request.DateOfBirth.Date;
            customer.UpdatedAtUtc = DateTime.UtcNow;

            await _writeRepo.UpdateAsync(customer);
            await _writeRepo.UnitOfWork.SaveChangesAsync();

            return customer.ToDto();
        }
    }
}