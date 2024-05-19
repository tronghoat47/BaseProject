using System.Security.Cryptography;
using System.Text;

namespace BaseProject.Infrastructure.Helpers
{
    public class CryptographyHelper : ICryptographyHelper
    {
        public string GenerateSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        public string HashPassword(string password, string salt)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public string GenerateHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }

        public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            return HashPassword(password, passwordSalt) == passwordHash;
        }
    }
}