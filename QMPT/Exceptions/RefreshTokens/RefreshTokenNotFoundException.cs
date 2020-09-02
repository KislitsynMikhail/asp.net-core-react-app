using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.RefreshTokens
{
    public class RefreshTokenNotFoundException : NotFoundException
    {
        public RefreshTokenNotFoundException() : base("Refresh token")
        {

        }
    }
}
