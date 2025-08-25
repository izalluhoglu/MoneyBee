using System.Security.Cryptography; 
using MoneyBee.AuthModule.Application.Abstractions;

namespace MoneyBee.AuthModule.Infrastructure.Security
{
    /// <summary>PBKDF2 SHA256 password hasher.</summary>
    public class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 100_000; 
        private const int HashSize = 32; 
        private const int SaltSize = 16;

        public byte[] GenerateSalt(int size = SaltSize)
        {
            var s = new byte[size];
            
            RandomNumberGenerator.Fill(s); 
            
            return s;
        }

        public byte[] ComputeHash(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            
            return pbkdf2.GetBytes(HashSize);
        }

        public bool Verify(string password, byte[] salt, byte[] expectedHash)
        {
            var actual = ComputeHash(password, salt); 
            
            return CryptographicOperations.FixedTimeEquals(actual, expectedHash);
        }
    }
}