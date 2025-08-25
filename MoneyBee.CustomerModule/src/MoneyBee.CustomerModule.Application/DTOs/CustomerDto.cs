namespace MoneyBee.CustomerModule.Application.DTOs
{
    /// <summary>Lightweight customer data transfer object.</summary>
    public class CustomerDto
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        public string PhoneNumber { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; }
        
        public string IdNumber { get; set; } = string.Empty;
    }
}