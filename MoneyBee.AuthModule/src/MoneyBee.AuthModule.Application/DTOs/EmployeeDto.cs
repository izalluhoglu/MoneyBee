namespace MoneyBee.AuthModule.Application.DTOs
{
    /// <summary>Employee data transfer object.</summary>
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAtUtc { get; set; }
        
        public DateTime? UpdatedAtUtc { get; set; }
    }
}