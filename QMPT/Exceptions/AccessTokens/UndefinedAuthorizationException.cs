using System.Net;

namespace QMPT.Exceptions.AccessTokens
{
    public class UndefinedAuthorizationException : InternalException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public override string Data => "Undefined authorization exception";
    }
}
