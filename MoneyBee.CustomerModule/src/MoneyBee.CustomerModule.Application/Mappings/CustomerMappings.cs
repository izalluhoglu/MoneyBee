using MoneyBee.CustomerModule.Application.DTOs;
using MoneyBee.CustomerModule.Domain.Entities;

namespace MoneyBee.CustomerModule.Application.Mappings
{
    /// <summary>Basic mapper for Customer &lt;-&gt; DTO.</summary>
    public static class CustomerMappings
    {
        /// <summary>Maps entity to DTO.</summary>
        public static CustomerDto ToDto(this Customer entity) => new CustomerDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber,
            Address = entity.Address,
            DateOfBirth = entity.DateOfBirth,
            IdNumber = entity.IdNumber
        };
    }
}