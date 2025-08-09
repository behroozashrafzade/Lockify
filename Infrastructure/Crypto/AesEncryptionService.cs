using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Crypto
{
    public class AesEncryptionService: IEncryptionService
    {
        public string Encrypt(string plainText, string key)
        {
            using var aes = Aes.Create();
            aes.Key = CreateKey(key, aes.KeySize / 8);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText, string key)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = CreateKey(key, aes.KeySize / 8);

            byte[] iv = new byte[aes.BlockSize / 8];
            Array.Copy(fullCipher, iv, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }

        public string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private byte[] CreateKey(string password, int keyBytes)
        {
            var salt = Encoding.UTF8.GetBytes("MySaltValue1234"); // بهتره بعداً داینامیک باشه
            using var key = new Rfc2898DeriveBytes(password, salt, 10000);
            return key.GetBytes(keyBytes);
        }

    }
}
