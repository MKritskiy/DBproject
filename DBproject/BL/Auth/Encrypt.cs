using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DBproject.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes("SecretKey"),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
                ));
        }
    }
}
