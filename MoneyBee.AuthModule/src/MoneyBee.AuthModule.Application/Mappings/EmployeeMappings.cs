using MoneyBee.AuthModule.Domain.Entities;
using MoneyBee.AuthModule.Application.DTOs;

namespace MoneyBee.AuthModule.Application.Mappings
{
    /// <summary>Basic mapper for Employee - DTO.</summary>
    public static class EmployeeMappings
    {
        /// <summary>Maps entity to DTO.</summary>
        public static EmployeeDto ToDto(this Employee e) => new EmployeeDto
        {
            Id = e.Id, 
            Username = e.Username, 
            IsActive = e.IsActive,
            CreatedAtUtc = e.CreatedAtUtc, 
            UpdatedAtUtc = e.UpdatedAtUtc
        };
    }
}