﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace HelperLibrary.Cryptography
{
    public class HashManager
    {
        public static string HashSHA512(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be empty.", nameof(text));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashSHA512(textBytes);
        }

        public static string HashSHA512(byte[] data)
        {
            using (var sha = new SHA512Managed())
            {                
                byte[] hash = sha.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string HashSHA384(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be empty.", nameof(text));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashSHA384(textBytes);
        }

        public static string HashSHA384(byte[] data)
        {
            using (var sha = new SHA384Managed())
            {               
                byte[] hash = sha.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string HashSHA256(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be empty.", nameof(text));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashSHA256(textBytes);
        }

        public static string HashSHA256(byte[] data)
        {
            using (var sha = new SHA256Managed())
            {                
                byte[] hash = sha.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string HashSHA1(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be empty.", nameof(text));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashSHA1(textBytes);
        }

        public static string HashSHA1(byte[] data)
        {
            using (var sha = new SHA1Managed())
            {                
                byte[] hash = sha.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string HashMD5(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be empty.", nameof(text));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashMD5(textBytes);
        }

        public static string HashMD5(byte[] data)
        {
            using (MD5 md5 = MD5.Create())
            {                
                byte[] hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        private static byte[] TextToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string GenerateSecureRandomToken()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[16];
                rng.GetBytes(tokenData);

                byte[] randomGUID = new Guid(tokenData).ToByteArray();

                string base64String = Convert.ToBase64String(randomGUID).Replace("==", String.Empty) + Convert.ToBase64String(tokenData);
                return base64String.Replace("+", String.Empty).Replace("/", String.Empty);                
            }                       
        }
    }
}