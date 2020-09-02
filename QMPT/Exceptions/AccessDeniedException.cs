using System.Net;

namespace QMPT.Exceptions
{
    public class AccessDeniedException : HttpResponseException
    {
        public override string Title => "Access Denied";

        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    }
}
