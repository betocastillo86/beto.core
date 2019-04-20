namespace Beto.Core.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringHelpers
    {
        private static Random random = new Random();

        public static string GetRandomString(int length = 6)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._/{}%&()!#-*¡?¿";
            return new string(Enumerable.Repeat(CHARS, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomStringNoSpecialCharacters(int length = 7)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuv";
            return new string(Enumerable.Repeat(CHARS, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

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

        public static string ToSha1(string text, string salt)
        {
            return ToSha1($"{text}.{salt}");
        }

        public static string ToXXSFilteredString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var rgx = new Regex("(<|>|/|\\\"|;|:|\\)|\\()");
            return rgx.Replace(value, string.Empty);
        }
    }
}