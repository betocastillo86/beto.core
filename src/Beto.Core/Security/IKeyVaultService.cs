namespace Beto.Core.Security
{
    public interface IKeyVaultService
    {
        string FetchSecret(string secret);
    }
}