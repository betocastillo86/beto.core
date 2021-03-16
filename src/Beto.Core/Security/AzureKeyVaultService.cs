using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;

namespace Beto.Core.Security
{
    public class AzureKeyVaultService : IKeyVaultService
    {
        private readonly SecretClient client;

        private readonly ILogger<AzureKeyVaultService> logger;

        public AzureKeyVaultService(
            string keyVaultUrl,
            ILogger<AzureKeyVaultService> logger)
        {
            this.client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            this.logger = logger;
        }

        public string FetchSecret(string secret)
        {
            var value = this.client.GetSecret(secret)?.Value?.Value ?? null;

            if (value == null)
            {
                var message = $"No se encuentra configurada el secret en prod {secret}";
                this.logger.LogError(message);
                throw new Exception(message);
            }

            return this.client.GetSecret(secret)?.Value?.Value ?? null;
        }
    }
}