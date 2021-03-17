using Beto.Core.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beto.Core.Registers
{
    public interface IServiceRegister
    {
        void Register(IServiceCollection services, IConfigurationRoot configuration, IKeyVaultService keyVaultService);
    }
}