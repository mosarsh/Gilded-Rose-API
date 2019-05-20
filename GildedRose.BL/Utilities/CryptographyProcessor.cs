using System;
using System.Security.Cryptography;
using System.Text;

namespace GildedRose.BL.Utilities
{
    /// <summary>
    /// Cryptography Processor static class
    /// </summary>
    public static class CryptographyProcessor
    {
        /// <summary>
        /// Create Salt method
        /// </summary>
        /// <param name="size">Size of salt</param>
        /// <returns>Salt as a string</returns>
        public static string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Generate Hash methos
        /// </summary>
        /// <param name="input">Input parameter such as password</param>
        /// <param name="salt">salt that has been created</param>
        /// <returns>Hash value as a string</returns>
        public static string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Check if hash asre equal method
        /// </summary>
        /// <param name="plainTextInput">Plain text such as password</param>
        /// <param name="hashedInput">hash input such as password hashed</param>
        /// <param name="salt">Salt</param>
        /// <returns>True/falses</returns>
        public static bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
