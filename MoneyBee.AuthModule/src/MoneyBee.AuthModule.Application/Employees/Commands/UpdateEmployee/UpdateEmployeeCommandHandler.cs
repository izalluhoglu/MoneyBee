using MediatR; 
using MoneyBee.AuthModule.Application.Abstractions;
using MoneyBee.AuthModule.Application.DTOs; 
using MoneyBee.AuthModule.Application.Mappings;

namespace MoneyBee.AuthModule.Application.Employees.Commands.UpdateEmployee
{
    /// <summary>Handles employee update.</summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeWriteRepository _writeRepo; 
        private readonly IEmployeeReadRepository _readRepo; 
        private readonly IPasswordHasher _hasher;

        public UpdateEmployeeCommandHandler(
            IEmployeeWriteRepository writeRepo,
            IEmployeeReadRepository readRepo,
            IPasswordHasher hasher)
        {
            _writeRepo = writeRepo; 
            _readRepo = readRepo; 
            _hasher = hasher;
        }
        
        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id); 
            
            if (entity is null) 
                throw new InvalidOperationException("Employee not found.");
            
            entity.Username = request.Username.Trim();

            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                var s=_hasher.GenerateSalt(); 
                var h=_hasher.ComputeHash(request.NewPassword,s); 
                entity.PasswordSalt=s; 
                entity.PasswordHash=h;
            }
            
            entity.UpdatedAtUtc = DateTime.UtcNow;
            
            await _writeRepo.UpdateAsync(entity); 
            await _writeRepo.UnitOfWork.SaveChangesAsync(); 
            
            return entity.ToDto();
        }
    }
}