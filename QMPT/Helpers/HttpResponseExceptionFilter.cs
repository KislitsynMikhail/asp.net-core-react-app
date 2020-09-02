using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QMPT.Exceptions;

namespace QMPT.Helpers
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null) return;

            var exception =
                  context.Exception is HttpResponseException
                ? context.Exception as HttpResponseException
                : new InternalException();

            context.Result = new ObjectResult(exception)
            {
                StatusCode = exception.StatusCode.ToInt()
            };

            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
