//-----------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// String Helpers
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// The random
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>the text</returns>
        public static string GetRandomString(int length = 6)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._/{}%&()!#-*¡?¿";
            return new string(Enumerable.Repeat(CHARS, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Converts To the MD5.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// the <c>md5</c> value
        /// </returns>
        public static string ToMd5(string text)
        {
            //// step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            //// step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// To the <c>sha1</c>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// the hash
        /// </returns>
        public static string ToSha1(string text)
        {
            using (var sha1 = SHA1.Create())
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha1.ComputeHash(encoding.GetBytes(text));
                for (int i = 0; i < stream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", stream[i]);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// To the <c>sha1</c> with a <c>salt</c> key
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// the hash
        /// </returns>
        public static string ToSha1(string text, string salt)
        {
            return ToSha1($"{text}.{salt}");
        }
    }
}