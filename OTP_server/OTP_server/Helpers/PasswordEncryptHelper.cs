using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace OTP_server.Helpers
{
    public static class PasswordEncryptHelper
    {
        //private const int SaltSize = 16; // 16 bytes for salt
        //private const int HashSize = 20; // 20 bytes for hash
        //private const int Iterations = 10000; // number of iterations

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        //const int keySize = 64;
        //const int iterations = 350000;
        //public static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        //public static string HashPasword(string password, out byte[] salt)
        //{
        //    salt = RandomNumberGenerator.GetBytes(keySize);
        //    var hash = Rfc2898DeriveBytes.Pbkdf2(
        //        Encoding.UTF8.GetBytes(password),
        //        salt,
        //        iterations,
        //        hashAlgorithm,
        //        keySize);
        //    return Convert.ToHexString(hash);
        //}

        //public static bool VerifyPassword(string password, string hash, byte[] salt)
        //{
        //    var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        //    return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        //}

    }
}
