using Microsoft.Extensions.Options;
using MoneyMe.Application.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MoneyMe.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly string _key;

        private readonly Settings _moneyMeSettings;

        public SecurityService(IOptions<Settings> options)
        {
            _moneyMeSettings = options.Value;
            _key = _moneyMeSettings.Secret;
        }

        public string Encrypt(string text)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(_key);
            aes.IV = Encoding.ASCII.GetBytes(_key);

            byte[] encrypted = EncryptStringToBytes_Aes(text, aes.Key, aes.IV);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(_key);
            aes.IV = Encoding.ASCII.GetBytes(_key);

            return DecryptStringFromBytes_Aes(Convert.FromBase64String(encryptedText), aes.Key, aes.IV);
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }

            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new MemoryStream(cipherText);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }

            return plaintext;
        }
    }
}