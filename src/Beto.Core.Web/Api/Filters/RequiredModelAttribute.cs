//-----------------------------------------------------------------------
// <copyright file="RequiredModelAttribute.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Filter that validates if a model is null or not
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class RequiredModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        /// <value>
        /// The name of the argument.
        /// </value>
        public string ArgumentName { get; set; } = "model";

        /// <summary>
        /// Action executing
        /// </summary>
        /// <param name="context">the context</param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var argument = context.ActionArguments[this.ArgumentName];

            if (argument == null)
            {
                context.Result = new BadRequestObjectResult(new BaseApiErrorModel()
                {
                    Error = new ApiErrorModel()
                    {
                        Code = "BadArgument",
                        Message = "El modelo no puede ser nulo",
                        Target = "model"
                    }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}