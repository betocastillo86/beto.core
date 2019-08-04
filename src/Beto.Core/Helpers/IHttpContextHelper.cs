namespace Beto.Core.Helpers
{
    public interface IHttpContextHelper
    {
        string GetCurrentIpAddress();

        string GetThisPageUrl(bool includeQueryString);

        string TryGetRefferUrl();
    }
}