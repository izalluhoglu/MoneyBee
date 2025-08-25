using MediatR; 
using MoneyBee.AuthModule.Application.Abstractions;
using MoneyBee.AuthModule.Application.DTOs; 
using MoneyBee.AuthModule.Application.Mappings;
using MoneyBee.AuthModule.Domain.Entities;

namespace MoneyBee.AuthModule.Application.Employees.Commands.CreateEmployee
{
    /// <summary>Handles employee creation.</summary>
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeWriteRepository _writeRepo;
        private readonly IEmployeeReadRepository _readRepo;
        private readonly IPasswordHasher _hasher;

        public CreateEmployeeCommandHandler(
            IEmployeeWriteRepository writeRepo,
            IEmployeeReadRepository readRepo,
            IPasswordHasher hasher)
        {
            _writeRepo = writeRepo; 
            _readRepo = readRepo; 
            _hasher = hasher;
        }
        
        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username)) 
                throw new InvalidOperationException("Username is required.");
            
            if (string.IsNullOrWhiteSpace(request.Password)) 
                throw new InvalidOperationException("Password is required.");
            
            var existing = await _readRepo.GetByUsernameAsync(request.Username.Trim());
            
            if (existing is not null) 
                throw new InvalidOperationException("Username already exists.");
            
            var salt = _hasher.GenerateSalt(); 
            var hash = _hasher.ComputeHash(request.Password, salt);
            
            var entity = new Employee
            {
                Username = request.Username.Trim(), 
                PasswordSalt = salt, 
                PasswordHash = hash, 
                IsActive = request.IsActive
            };
            
            await _writeRepo.AddAsync(entity); 
            
            await _writeRepo.UnitOfWork.SaveChangesAsync(); 
            
            return entity.ToDto();
        }
    }
}