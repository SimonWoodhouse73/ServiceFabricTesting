using Callcredit.RESTful.Services.Readers;
using Callcredit.RESTful.Standards.Permitted;
using Microsoft.AspNetCore.Mvc.Filters;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class QueryStringValidationFilter : IActionFilter
    {
        private readonly IRequestReader requestReader;

        public QueryStringValidationFilter(IRequestReader requestReader)
        {
            Requires.NotNull(requestReader, nameof(requestReader));

            this.requestReader = requestReader;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var queryStrings = requestReader.GetQueryStrings();
            foreach (var validator in QueryStringParameterValidators.PermittedQueryStringParameterValidators)
            {
                validator.Validate(queryStrings);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
