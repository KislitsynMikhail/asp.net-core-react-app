using System.Net;

namespace QMPT.Exceptions
{
    public class InternalException : HttpResponseException
    {
        public override string Title => "Internal exception";

        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    }
}
