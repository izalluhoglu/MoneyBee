namespace MoneyBee.AuthModule.Application.DTOs
{
    /// <summary>Token response DTO.</summary>
    public class AuthResultDto
    {
        public string AccessToken { get; set; }
        
        public DateTime ExpiresAtUtc { get; set; }
        
        public string TokenType { get; set; } = "Bearer";
    }
}
