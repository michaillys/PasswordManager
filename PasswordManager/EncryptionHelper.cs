using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public static class EncryptionHelper
    {
        private static string EncryptionKey = "your-encryption-key";

        public static string EncryptionKey1 { get => EncryptionKey; private set => EncryptionKey = value; }
        public static void SetEncryptionKey(string key)
        {
            EncryptionKey = key;
        }



        public static string EncryptString(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.Key = SHA256.Create().ComputeHash(keyBytes);
                aes.IV = new byte[16];
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.Close();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptString(string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.Key = SHA256.Create().ComputeHash(keyBytes);
                aes.IV = new byte[16]; // initialize to zero
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

     
        
        // Extra, not used in the project
        public static void EncryptFile(string inputFile, string key)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyBytesPadded = new byte[32]; // AES-256 key size
                Array.Copy(keyBytes, keyBytesPadded, Math.Min(keyBytes.Length, keyBytesPadded.Length));

                aes.Key = keyBytesPadded;
                aes.GenerateIV();

                using (FileStream fsCrypt = new FileStream($"{inputFile}.enc", FileMode.Create))
                {
                    fsCrypt.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(fsCrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                        {
                            int data;
                            while ((data = fsIn.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
        public static void OnApplicationExit(object sender, EventArgs e)
        {
            string FilePath = DataManager.FilePath;
            EncryptFile(FilePath, EncryptionKey);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
