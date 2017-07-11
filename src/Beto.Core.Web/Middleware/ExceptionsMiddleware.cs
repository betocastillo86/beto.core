//-----------------------------------------------------------------------
// <copyright file="ExceptionsMiddleware.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Middleware for responding API Requests and register errors
    /// </summary>
    public sealed class ExceptionsMiddleware
    {
        /// <summary>
        /// The delegate
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionsMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="env">The environment.</param>
        public ExceptionsMiddleware(
            RequestDelegate next,
            ILoggerService loggerService,
            IHostingEnvironment env)
        {
            this.next = next;
            this.loggerService = loggerService;
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
                this.loggerService.Insert(ex.Message, ex.ToString());

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