//-----------------------------------------------------------------------
// <copyright file="ExceptionsMiddlewareLogger.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
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

    /// <summary>
    /// Exception Middleware
    /// </summary>
    public sealed class ExceptionsMiddlewareLogger
    {
        /// <summary>
        /// The delegate
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILogger<ExceptionsMiddlewareLogger> logger;

        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionsMiddlewareLogger"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="env">The env.</param>
        public ExceptionsMiddlewareLogger(
            RequestDelegate next,
            ILogger<ExceptionsMiddlewareLogger> logger,
            IHostingEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>the async task</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var messageformat = $"[{DateTime.UtcNow}]\n {ex.ToString()} \n Url: {context?.Request?.Query}";

                this.logger.LogError(ex, messageformat);

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