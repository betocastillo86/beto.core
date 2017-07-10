namespace Beto.Core.Web.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class FluentValidatorAttribute : ActionFilterAttribute
    {
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