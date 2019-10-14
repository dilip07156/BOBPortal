using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CardHolder.Utility
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class EncryptDecryptQueryString
    {
        /// <summary>
        /// 
        /// </summary>
        private static byte[] key = { };
        /// <summary>
        /// 
        /// </summary>
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        /// <summary>
        /// Decrypts the specified string to decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <param name="sEncryptionKey">The s encryption key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Encrypts the specified string to encrypt.
        /// </summary>
        /// <param name="stringToEncrypt">The string to encrypt.</param>
        /// <param name="SEncryptionKey">The S encryption key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
