using MoneyMe.Application.Contracts;
using Crypto.AES;

namespace MoneyMe.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private const string Key = "m0n3ym3_r3dir3ct";

        public string Encrypt(string text)
        {
            using AES aes = new AES(Key);
            var encrypted = aes.Encrypt(text);

            return encrypted;
        }

        public string Decrypt(string text)
        {
            using AES aes = new AES(Key);
            var decrypted = aes.Decrypt(text);

            return decrypted;
        }
    }
}