using MediatR;
using MoneyBee.AuthModule.Application.Abstractions;
using MoneyBee.AuthModule.Application.DTOs;
using MoneyBee.AuthModule.Application.Mappings;

namespace MoneyBee.AuthModule.Application.Employees.Queries.GetEmployeeById
{
    /// <summary>Handles retrieval of employee by Id.</summary>
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeReadRepository _readRepo;
        
        public GetEmployeeByIdQueryHandler(IEmployeeReadRepository readRepo) => _readRepo = readRepo;

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await _readRepo.GetByIdAsync(request.Id); 
            
            return e?.ToDto();
        }
    }
}