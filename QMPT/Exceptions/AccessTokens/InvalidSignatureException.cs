using QMPT.Exceptions.Validations;
using System.Net;

namespace QMPT.Exceptions.AccessTokens
{
    public class InvalidSignatureException : InvalidParametersException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public InvalidSignatureException() : base("Signature")
        {

        }
    }
}
