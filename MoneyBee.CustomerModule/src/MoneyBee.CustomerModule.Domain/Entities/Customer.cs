namespace MoneyBee.CustomerModule.Domain.Entities
{
    /// <summary>Represents a customer (sender/receiver) stored in the system.</summary>
    public class Customer
    {
        /// <summary>Primary key.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>First name.</summary>
        public string FirstName { get; set; } = string.Empty;
        
        /// <summary>Last name.</summary>
        public string LastName { get; set; } = string.Empty;
        
        /// <summary>Phone number in E.164 or local format.</summary>
        public string PhoneNumber { get; set; } = string.Empty;
        
        /// <summary>Address text.</summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>Date of birth (date component only considered).</summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>Government-issued ID number. Must be unique.</summary>
        public string IdNumber { get; set; } = string.Empty;
        
        /// <summary>UTC creation time.</summary>
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        
        /// <summary>UTC last update time.</summary>
        public DateTime? UpdatedAtUtc { get; set; }
    }
}