using QMPT.Exceptions.Bases;
using System.Net;

namespace QMPT.Exceptions.AccessTokens
{
    public class ExpiredAccessTokenException : ExpiredParametersException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public ExpiredAccessTokenException() : base("Access token")
        {

        }
    }
}
