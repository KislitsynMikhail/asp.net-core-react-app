using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.AccessTokens
{
    public class AccessTokenNotFoundException : NotFoundException
    {
        public AccessTokenNotFoundException() : base("Access token")
        {

        }
    }
}
