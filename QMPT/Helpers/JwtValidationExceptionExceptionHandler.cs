using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QMPT.Exceptions;
using QMPT.Exceptions.AccessTokens;

namespace QMPT.Helpers
{
    public static class JwtValidationExceptionExceptionHandler
    {
        public static void HandleAuthorizationException(this AuthenticationFailedContext context)
        {
            context.Response.OnStarting(async () =>
            {
                HttpResponseException exception;
                if (context.Exception is SecurityTokenExpiredException)
                {
                    exception = new ExpiredAccessTokenException();
                }
                else if (context.Exception is SecurityTokenInvalidSignatureException)
                {
                    exception = new InvalidSignatureException();
                }
                else
                {
                    exception = new UndefinedAuthorizationException();
                }

                context.Response.StatusCode = exception.StatusCode.ToInt();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            });
        }
    }
}
