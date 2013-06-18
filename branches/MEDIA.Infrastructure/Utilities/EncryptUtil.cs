using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEDIA.Infrastructure.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class EncryptUtil
    {
        static string saltValue = "MEDIA2013";
        static string pwdValue = "MEDIA2013";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(input);
            byte[] salt = System.Text.UTF8Encoding.UTF8.GetBytes(saltValue);

            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            System.Security.Cryptography.Rfc2898DeriveBytes rfc = new System.Security.Cryptography.Rfc2898DeriveBytes(pwdValue, salt);
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            System.Security.Cryptography.ICryptoTransform encrypt = aes.CreateEncryptor();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream encryptor = new System.Security.Cryptography.CryptoStream
            (stream, encrypt, System.Security.Cryptography.CryptoStreamMode.Write);

            encryptor.Write(data, 0, data.Length);
            encryptor.Close();
            return Convert.ToBase64String(stream.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            byte[] encryptBytes = Convert.FromBase64String(input);
            byte[] salt = Encoding.UTF8.GetBytes(saltValue);

            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            System.Security.Cryptography.Rfc2898DeriveBytes rfc = new System.Security.Cryptography.Rfc2898DeriveBytes(pwdValue, salt);
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            System.Security.Cryptography.ICryptoTransform transform = aes.CreateDecryptor();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream decryptor = new System.Security.Cryptography.CryptoStream
            (stream, transform, System.Security.Cryptography.CryptoStreamMode.Write);
            decryptor.Write(encryptBytes, 0, encryptBytes.Length);
            decryptor.Close();
            byte[] decryptBytes = stream.ToArray();
            return UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
        }
    }
}