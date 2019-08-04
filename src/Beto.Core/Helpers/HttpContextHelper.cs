namespace Beto.Core.Helpers
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class HttpContextHelper : IHttpContextHelper
    {
        private readonly IHttpContextAccessor accessor;

        public HttpContextHelper(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        private HttpContext HttpContext
        {
            get
            {
                return this.accessor.HttpContext;
            }
        }

        public virtual string GetCurrentIpAddress()
        {
            if (!this.IsRequestAvailable())
            {
                return string.Empty;
            }

            return this.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            string url = string.Empty;
            if (!this.IsRequestAvailable())
            {
                return url;
            }

            if (includeQueryString)
            {
                url = this.HttpContext.Request.Path + "?" + this.HttpContext.Request.QueryString;
            }
            else
            {
                if (this.HttpContext.Request.PathBase != null)
                {
                    url = this.HttpContext.Request.PathBase;
                }
            }

            url = url.ToLowerInvariant();
            return url;
        }

        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            string path = request.Path;
            string extension = System.IO.Path.GetExtension(request.Path);

            if (extension == null)
            {
                return false;
            }

            switch (extension.ToLower())
            {
                case ".axd":
                case ".ashx":
                case ".bmp":
                case ".css":
                case ".gif":
                case ".htm":
                case ".html":
                case ".ico":
                case ".jpeg":
                case ".jpg":
                case ".js":
                case ".png":
                case ".rar":
                case ".zip":
                    return true;
            }

            return false;
        }

        protected virtual bool IsRequestAvailable()
        {
            try
            {
                if (this.accessor?.HttpContext?.Request == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public string TryGetRefferUrl()
        {
            string referrerUrl = string.Empty;

            ////URL referrer is null in some case (for example, in IE 8)
            if (this.IsRequestAvailable() && this.HttpContext.Request.Headers.ContainsKey("Referer"))
            {
                referrerUrl = this.HttpContext.Request.Headers["Referer"];
            }

            return referrerUrl;
        }
    }
}