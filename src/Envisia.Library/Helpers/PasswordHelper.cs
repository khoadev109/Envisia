using System.Security.Cryptography;
using System.Text;

namespace Envisia.Library.Helpers
{
    public static class PasswordHelper
    {
        public static readonly int keySize = 64;

        private static readonly int iterations = 350000;

        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static (string, byte[]) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm, keySize);

            var passwordHash = Convert.ToHexString(hash);

            return (passwordHash, salt);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
