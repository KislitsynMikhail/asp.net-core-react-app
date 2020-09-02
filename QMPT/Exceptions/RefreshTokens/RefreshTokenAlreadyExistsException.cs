using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.RefreshTokens
{
    public class RefreshTokenAlreadyExistsException : AlreadyExistsException
    {
        public RefreshTokenAlreadyExistsException() : base("Refresh token")
        {

        }
    }
}
