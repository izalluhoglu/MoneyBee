using MediatR; 
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Application.DTOs; 
using MoneyBee.AuthModule.Application.Mappings;

namespace MoneyBee.AuthModule.Application.Employees.Queries.GetEmployees
{
    /// <summary>Handles page retrieval of employees.</summary>
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeReadRepository _readRepo; 
        public GetEmployeesQueryHandler(IEmployeeReadRepository readRepo) => _readRepo = readRepo;

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetAllAsync(request.Page, request.PageSize); 
            
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}