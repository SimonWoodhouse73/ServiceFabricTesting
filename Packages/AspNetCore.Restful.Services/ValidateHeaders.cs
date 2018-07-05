using Callcredit.RESTful.Standards;
using Microsoft.AspNetCore.Mvc.Filters;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class ValidateHeadersFilter : ActionFilterAttribute
    {
        private readonly IHeaderValidatorExecutor headerValidatorExecutor;

        public ValidateHeadersFilter(IHeaderValidatorExecutor headerValidatorExecutor)
        {
            Requires.NotNull(headerValidatorExecutor, nameof(headerValidatorExecutor));

            this.headerValidatorExecutor = headerValidatorExecutor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Requires.NotNull(context, nameof(context));

            headerValidatorExecutor.ValidateRequest();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Requires.NotNull(context, nameof(context));

            headerValidatorExecutor.ValidateResponse();
        }
    }
}
