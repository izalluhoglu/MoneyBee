namespace MoneyBee.AuthModule.Infrastructure.Auth
{
    /// <summary>JWT configuration options.</summary>
    public class JwtOptions
    {
        public string Issuer { get; set; }
        
        public string Audience { get; set; }
        
        public string Key { get; set; }
        
        public int ExpiresMinutes { get; set; } = 60;
        
        public bool ValidateIssuer { get; set; } = true;
        
        public bool ValidateAudience { get; set; } = true;
        
        public bool ValidateLifetime { get; set; } = true;
        
        public bool ValidateIssuerSigningKey { get; set; } = true;
    }
}