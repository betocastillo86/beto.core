//-----------------------------------------------------------------------
// <copyright file="SeoHelper.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Common
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SeoHelper : ISeoHelper
    {
        // <summary>
        /// Generates the name of the friendly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>
        /// the value
        /// </returns>
        public string GenerateFriendlyName(string name, IQueryable<ISeoEntity> query = null, int maxLength = 280)
        {
            ////Convierte la cadena en Seo friendly
            string textnorm = name.Trim().Normalize(NormalizationForm.FormD);
            var reg = new Regex("[^a-zA-Z0-9 ]");
            string friendlyname = reg.Replace(textnorm, string.Empty)
                .Trim()
                .Replace(" ", "-")
                .ToLower();

            var regexMultipleSpaces = new Regex("[-]{2,}", RegexOptions.None);
            friendlyname = regexMultipleSpaces.Replace(friendlyname, "-");

            if (friendlyname.Length > maxLength)
            {
                ////Valida que el nombre no pase del tamaño permitido, pero agrega las palabras completas en el titulo
                if (friendlyname.IndexOf("-", maxLength - 1) != -1)
                {
                    friendlyname = friendlyname.Substring(0, friendlyname.IndexOf("-", maxLength - 1));
                }
            }

            ////Realiza una ultima validación para verificar que el texto no se vaya ir muy largo.
            ////Ejemplo: cuando meten cadenas muy largas sin espacios
            if (friendlyname.Length > maxLength + 20)
            {
                friendlyname = friendlyname.Substring(0, maxLength + 20);
            }

            if (query != null)
            {
                var available = !query.Any(c => c.FriendlyName.Equals(friendlyname));

                ////Si el nombre no está disponible genera un numero aleatorio para completar la URL
                if (!available)
                {
                    friendlyname = $"{friendlyname}-{new Random().Next(1000000).ToString()}";
                }
            }

            return friendlyname;
        }
    }
}