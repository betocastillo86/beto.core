namespace Beto.Core.Web.Middleware
{
    using Beto.Core.Web.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public sealed class ExceptionsMiddlewareLogger
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionsMiddlewareLogger> logger;

        private readonly IHostingEnvironment env;

        private readonly JsonSerializerOptions options;

        public ExceptionsMiddlewareLogger(
            RequestDelegate next,
            ILogger<ExceptionsMiddlewareLogger> logger,
            IHostingEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
            this.options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var requestDetails = JsonSerializer.Serialize(LogDetailedFactory.GetModel(ex, context));

                this.logger.LogError(ex, $"{ex} RequestData: {{requestDetails}}".ToString(), requestDetails);

                var jsonResponse = new ApiErrorModel()
                {
                    Code = "ServerError",
                    Message = this.env.IsDevelopment() ? ex.ToString() : "Error inesperado, intene de nuevo"
                };

                string jsonString = JsonSerializer.Serialize<ApiErrorModel>(jsonResponse, this.options);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonString, System.Text.Encoding.UTF8);
            }
        }
    }
}