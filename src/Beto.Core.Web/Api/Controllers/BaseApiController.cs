//-----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api.Controllers
{
    using System.Collections.Generic;
    using Beto.Core.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Base Controller for API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseApiController : Controller
    {
        /// <summary>
        /// The message exception finder
        /// </summary>
        private readonly IMessageExceptionFinder messageExceptionFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public BaseApiController(IMessageExceptionFinder messageExceptionFinder)
        {
            this.messageExceptionFinder = messageExceptionFinder;
        }

        #region BadRequest

        /// <summary>
        /// Creates an <see cref="T:Microsoft.AspNetCore.Mvc.BadRequestObjectResult" /> that produces a Bad Request (400) response.
        /// </summary>
        /// <param name="modelState">the model state</param>
        /// <returns>
        /// The created <see cref="T:Microsoft.AspNetCore.Mvc.BadRequestObjectResult" /> for the response.
        /// </returns>
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var error = new ApiErrorModel();
            error.Code = "BadArgument";
            error.Message = "Argumento invalido";

            foreach (var key in modelState.Keys)
            {
                var errorState = modelState[key];

                foreach (var detailError in errorState.Errors)
                {
                    error.Details.Add(new ApiErrorModel()
                    {
                        Code = "BadArgument",
                        Message = detailError.ErrorMessage,
                        Target = key
                    });
                }
            }

            return base.BadRequest(new BaseApiErrorModel() { Error = error });
        }

        /// <summary>
        /// A bad request response.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        /// <returns>the action</returns>
        protected IActionResult BadRequest<T>(CoreException<T> ex, string message = null)
        {
            var error = new ApiErrorModel();
            error.Code = ex.Code.ToString();
            error.Message = message ?? ex.Message;
            error.Target = ex.Target;
            return this.StatusCode(400, new BaseApiErrorModel() { Error = error });
        }

        /// <summary>
        /// A bad request response.
        /// </summary>
        /// <typeparam name="T">Exception type</typeparam>
        /// <param name="code">The code.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>the action</returns>
        protected IActionResult BadRequest<T>(CoreException<T> code, IList<ApiErrorModel> errors, string target = null)
        {
            var error = new ApiErrorModel()
            {
                Code = code.ToString(),
                Message = this.messageExceptionFinder.GetErrorMessage(code),
                Target = target,
                Details = errors == null || errors.Count == 0 ? null : errors
            };

            return this.StatusCode(400, new BaseApiErrorModel() { Error = error });
        }

        /// <summary>
        /// A bad request response.
        /// </summary>
        /// <typeparam name="T">Exception type</typeparam>
        /// <param name="code">The code.</param>
        /// <param name="error">The error.</param>
        /// <returns>the action</returns>
        protected IActionResult BadRequest<T>(T code, string error)
        {
            var apiError = new ApiErrorModel()
            {
                Code = code.ToString(),
                Message = this.messageExceptionFinder.GetErrorMessage(code),
                Target = null
            };

            return this.StatusCode(400, new BaseApiErrorModel() { Error = apiError });
        }

        /// <summary>
        /// A bad request response.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>the action result</returns>
        protected IActionResult BadRequest(IList<ApiErrorModel> errors, string target = null)
        {
            var error = new ApiErrorModel()
            {
                Code = "BadArgument",
                Message = "BadArgument",
                Target = target,
                Details = errors == null || errors.Count == 0 ? null : errors
            };

            return this.StatusCode(400, new BaseApiErrorModel() { Error = error });
        }

        #endregion BadRequest

        #region Ok

        /// <summary>
        /// Ok the specified paged list
        /// </summary>
        /// <typeparam name="T">The Entity</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="hasNextPage">if set to <c>true</c> [has next page].</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>the value</returns>
        protected IActionResult Ok<T>(IList<T> list, bool hasNextPage, int totalCount) where T : class
        {
            var model = new PaginationResponseModel<T>()
            {
                Meta = new PaginationInformationModel { Count = list.Count, HasNextPage = hasNextPage, TotalCount = totalCount },
                Results = list
            };

            return this.StatusCode(200, model);
        }

        #endregion Ok

        #region Created        
        /// <summary>
        /// The own created method
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        protected IActionResult Created(string name, int id)
        {
            var uri = this.Url.Link(name, new { id = id });
            return this.Created(uri, new { Id = id });
        }
        #endregion
    }
}