using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;

namespace Beto.Core.Security
{
    public class AzureKeyVaultService : IKeyVaultService
    {
        private readonly SecretClient client;

        public AzureKeyVaultService(
            string keyVaultUrl)
        {
            this.client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        }

        public string FetchSecret(string secret)
        {
            var value = this.client.GetSecret(secret)?.Value?.Value ?? null;

            if (value == null)
            {
                var message = $"No se encuentra configurada el secret en prod {secret}";
                throw new Exception(message);
            }

            return this.client.GetSecret(secret)?.Value?.Value ?? null;
        }
    }
}