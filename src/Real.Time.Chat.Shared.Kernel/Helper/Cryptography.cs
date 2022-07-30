using System.Security.Cryptography;
using System.Text;

namespace Real.Time.Chat.Shared.Kernel.Helper
{
    public static class Cryptography
    {
        private static readonly string _key = "RealTimeChat";
        private static readonly byte[] _Iv = { 12, 20, 45, 71, 85, 99, 110, 123 };

        public static string PasswordEncrypt(string password)
        {
            using DESCryptoServiceProvider desCryptoServiceProvider = new();
            using MemoryStream memoryStream = new();

            byte[] input = Encoding.UTF8.GetBytes(password);
            byte[] key = Encoding.UTF8.GetBytes(_key.Substring(0, 8));

            using CryptoStream cryptoStream = new(memoryStream, desCryptoServiceProvider.CreateEncryptor(key, _Iv), CryptoStreamMode.Write);

            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string PasswordDecrypt(string password)
        {
            using DESCryptoServiceProvider desCryptoServiceProvider = new();
            using MemoryStream memoryStream = new();

            byte[] input = Convert.FromBase64String(password);
            byte[] key = Encoding.UTF8.GetBytes(_key[..8]);

            using CryptoStream cryptoStream = new(memoryStream, desCryptoServiceProvider.CreateDecryptor(key, _Iv), CryptoStreamMode.Write);

            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
