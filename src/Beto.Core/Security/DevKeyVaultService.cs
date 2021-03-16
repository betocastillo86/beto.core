using System;
using System.Collections.Generic;

namespace Beto.Core.Security
{
    public class DevKeyVaultService : IKeyVaultService
    {
        private readonly Dictionary<string, string> secrets;

        public DevKeyVaultService(Dictionary<string, string> secrets)
        {
            this.secrets = secrets;
        }

        public string FetchSecret(string secret)
        {
            if (!this.secrets.ContainsKey(secret))
            {
                throw new Exception($"Tratando de traer secret en desarrollo que no existe {secret}");
            }

            return this.secrets[secret]
        }
    }
}