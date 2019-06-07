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

        private readonly IServiceProvider serviceProvider;

        public ExceptionsMiddlewareLogger(
            RequestDelegate next,
            ILogger<ExceptionsMiddlewareLogger> logger,
            IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
            this.serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var messageformat = $"[{DateTime.UtcNow}]\n {ex.ToString()} \n Url: {context?.Request?.Query}";

                Console.WriteLine(messageformat);
                this.logger.LogError(ex, messageformat);
                this.logger.LogDebug(JsonConvert.SerializeObject(LogDetailedFactory.GetModel(ex, context)));

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