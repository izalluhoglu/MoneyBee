namespace MoneyBee.AuthModule.Domain.Entities
{
    /// <summary>Represents a branch employee who can log into the system.</summary>
    public class Employee
    {
        /// <summary>Primary key.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>Unique username.</summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>Password hash.</summary>
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        
        /// <summary>Password salt.</summary>
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        
        /// <summary>Whether the employee account is active.</summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>UTC creation time.</summary>
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        
        /// <summary>UTC last update time.</summary>
        public DateTime? UpdatedAtUtc { get; set; }
    }
}