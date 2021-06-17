namespace Beto.Core.Web.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Web.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public sealed class ExceptionsMiddlewareLogger
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionsMiddlewareLogger> logger;

        private readonly IHostingEnvironment env;

        public ExceptionsMiddlewareLogger(
            RequestDelegate next,
            ILogger<ExceptionsMiddlewareLogger> logger,
            IHostingEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var requestDetails = JsonConvert.SerializeObject(LogDetailedFactory.GetModel(ex, context));

                this.logger.LogError(ex, $"{ex} RequestData: {{requestDetails}}".ToString(), requestDetails);

                var jsonResponse = new ApiErrorModel()
                {
                    Code = "ServerError",
                    Message = this.env.IsDevelopment() ? ex.ToString() : "Error inesperado, intene de nuevo"
                };

                string jsonString = JsonConvert.SerializeObject(jsonResponse, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(jsonString, System.Text.Encoding.UTF8);
            }
        }
    }
}