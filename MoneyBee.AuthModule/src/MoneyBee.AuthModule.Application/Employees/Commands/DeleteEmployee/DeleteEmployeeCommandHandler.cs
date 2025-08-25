using MediatR;
using MoneyBee.AuthModule.Application.Abstractions;

namespace MoneyBee.AuthModule.Application.Employees.Commands.DeleteEmployee
{
    /// <summary>Handles employee deletion.</summary>
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeWriteRepository _writeRepo; 
        private readonly IEmployeeReadRepository _readRepo;

        public DeleteEmployeeCommandHandler(IEmployeeWriteRepository writeRepo, IEmployeeReadRepository readRepo)
        {
            _writeRepo = writeRepo; 
            _readRepo = readRepo;
        }
        
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id); 
            
            if (entity is null) 
                return false;
            
            await _writeRepo.DeleteAsync(entity); 
            
            var affected = await _writeRepo.UnitOfWork.SaveChangesAsync(); 
            
            return affected > 0;
        }
    }
}