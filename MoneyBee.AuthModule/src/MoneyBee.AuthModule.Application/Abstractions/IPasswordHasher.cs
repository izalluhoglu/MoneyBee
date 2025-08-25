namespace MoneyBee.AuthModule.Application.Abstractions
{
    /// <summary>Provides password hashing and verification.</summary>
    public interface IPasswordHasher
    {
        /// <summary>Generates a random salt.</summary>
        byte[] GenerateSalt(int size = 16);
        
        /// <summary>Computes PBKDF2 hash for a password and salt.</summary>
        byte[] ComputeHash(string password, byte[] salt);
        
        /// <summary>Verifies that a password matches the hash+salt.</summary>
        bool Verify(string password, byte[] salt, byte[] expectedHash);
    }
}