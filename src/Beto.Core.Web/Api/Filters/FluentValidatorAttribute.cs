//-----------------------------------------------------------------------
// <copyright file="FluentValidatorAttribute.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// A fluent validator filter which allow responding with a Bad Request when the model is wrong formed
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class FluentValidatorAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On action executing
        /// </summary>
        /// <param name="context">the context</param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = new ApiErrorModel();
                error.Code = "BadArgument";
                error.Message = "Argumento invalido";

                foreach (var key in context.ModelState.Keys)
                {
                    var errorState = context.ModelState[key];

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

                context.Result = new BadRequestObjectResult(new BaseApiErrorModel() { Error = error });
            }

            base.OnActionExecuting(context);
        }
    }
}