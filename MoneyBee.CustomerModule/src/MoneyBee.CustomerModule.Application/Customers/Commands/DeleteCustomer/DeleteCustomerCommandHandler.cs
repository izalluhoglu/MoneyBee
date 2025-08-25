using MediatR;
using MoneyBee.CustomerModule.Application.Abstractions;

namespace MoneyBee.CustomerModule.Application.Customers.Commands.DeleteCustomer
{
    /// <summary>Handles customer deletion.</summary>
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerWriteRepository _writeRepo;
        private readonly ICustomerReadRepository _readRepo;

        public DeleteCustomerCommandHandler(ICustomerWriteRepository writeRepo, ICustomerReadRepository readRepo)
        {
            _writeRepo = writeRepo;
            _readRepo = readRepo;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _readRepo.GetByIdAsync(request.Id);
            
            if (customer is null) 
                return false;

            await _writeRepo.DeleteAsync(customer);
            
            var affected = await _writeRepo.UnitOfWork.SaveChangesAsync();
            
            return affected > 0;
        }
    }
}