using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.RefreshTokens
{
    public class ExpiredRefreshTokenException : ExpiredParametersException
    {
        public ExpiredRefreshTokenException() : base("Refresh token")
        {

        }
    }
}
