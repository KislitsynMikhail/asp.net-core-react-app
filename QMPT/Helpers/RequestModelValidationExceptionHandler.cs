using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QMPT.Exceptions;
using QMPT.Exceptions.Validations;
using System.Collections.Generic;

namespace QMPT.Helpers
{
    public static class RequestModelValidationExceptionHandler
    {
        public static BadRequestObjectResult HandleValidationException(this ModelStateDictionary modelState)
        {
            var keys = new List<string>();
            string exceptionName = null;
            foreach (var parameter in modelState)
            {
                var errorMessage = parameter.Value.Errors[0].ErrorMessage;
                if (exceptionName is null)
                    exceptionName = errorMessage;

                if (errorMessage == exceptionName)
                    keys.Add(parameter.Key);
            }

            HttpResponseException exception;
            if (exceptionName == ErrorMessages.missingParameters)
            {
                exception = new MissingParametersException(keys);
            }
            else if (exceptionName == ErrorMessages.invalidParameters)
            {
                exception = new InvalidParametersException(keys);
            }
            else
            {
                exception = new InternalException();
            }

            return new BadRequestObjectResult(exception)
            {
                StatusCode = exception.StatusCode.ToInt()
            };
        }
    }
}
