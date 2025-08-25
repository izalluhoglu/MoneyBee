namespace MoneyBee.AuthModule.Application.DTOs
{
    /// <summary>Token validation result.</summary>
    public class TokenValidationResultDto
    {
        public bool IsValid { get; set; }
        
        public string Subject { get; set; }
        
        public string Username { get; set; }
        
        public DateTime? ExpiresAtUtc { get; set; }
        
        public string Error { get; set; }
    }
}