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
    public class Crypt
    {
        /// <summary>
        /// Converting Existing String Into Encrpted Format
        /// </summary>
        /// <param name="Text">The Text of the record to update.</param>
        /// <param name="Key">The Key of the record to update.</param>
        /// <returns>Return String Representing Encrypted Text</returns>
        /// <remarks></remarks>
        public string EncryptText(string Text, string Key)
        {
            return Encrypt(Text, Key);
        }

        /// <summary>
        /// Decrypts the text.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <param name="Key">The key.</param>
        /// <returns>String Representing Decrypted Text</returns>
        /// <remarks></remarks>
        public string DecryptText(string Text, string Key)
        {
            return Decrypt(Text, Key);
        }

        /// <summary>
        /// Encrypts the specified STR text.
        /// </summary>
        /// <param name="strText">The STR text.</param>
        /// <param name="strEncrKey">The STR encr key.</param>
        /// <returns>String Representing Encrypted Text</returns>
        /// <remarks></remarks>
        private string Encrypt(string strText, string strEncrKey)
        {
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Decrypts the specified STR text.
        /// </summary>
        /// <param name="strText">The STR text.</param>
        /// <param name="sDecrKey">The s decr key.</param>
        /// <returns>Representing Decrypted Text</returns>
        /// <remarks></remarks>
        private string Decrypt(string strText, string sDecrKey)
        {
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[strText.Length + 1];

            byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        /// <summary>
        /// Computes the MD5 hash value for the given string and converts the result into a Base64 encoded string.
        /// This string is directly comparable with and storable in the User table as a password.
        /// </summary>
        /// <param name="sToHash">String to hash and encode</param>
        /// <returns>MD5 hashed, Base64 encoded value of the given string</returns>
        /// <remarks></remarks>
        public static string CreateMD5HashedBase64String(string sToHash)
        {
            // get generic MD5 hasher
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            return Convert.ToBase64String(md5Hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sToHash)));
        }


        //To make URL Encrypted Added by Sahil on 13-Feb-2014


        /// <summary>
        /// Converting Existing String Into Encrpted Format
        /// </summary>
        /// <param name="Text">The Text of the record to update.</param>
        /// <returns>Return String Representing Encrypted Text</returns>
        /// <remarks></remarks>
        public string EncryptURL(string Text)
        {
            return EncryptQueryURL(Text);
        }

        /// <summary>
        /// Decrypts the text.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>String Representing Decrypted Text</returns>
        /// <remarks></remarks>
        public string DecryptURL(string Text)
        {
            return DecryptQueryURL(Text);
        }


        /// <summary>
        /// 
        /// </summary>
        private const string ENCRYPTION_KEY = "key";
        /// <summary>
        /// 
        /// </summary>
        /// The salt value used to strengthen the encryption.
        private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

        /// <summary>
        /// Encrypts the query URL.
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <returns></returns>
        /// Encrypts any string using the Rijndael algorithm.
        /// A Base64 encrypted string.
        /// <remarks></remarks>
        private string EncryptQueryURL(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        //return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the query URL.
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <returns></returns>
        /// Decrypts a previously encrypted string.
        /// A decrypted string.
        /// <remarks></remarks>
        private string DecryptQueryURL(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = new byte[inputText.Length + 1];
            encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }
    }
}