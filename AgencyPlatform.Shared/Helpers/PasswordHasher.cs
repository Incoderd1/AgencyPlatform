using System.Security.Cryptography;
using System.Text;

namespace AgencyPlatform.Shared.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string inputPassword, string storedHash, string salt)
        {
            var hashInput = HashPassword(inputPassword, salt);
            return hashInput == storedHash;
        }

        public static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }

}
