using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User")
        {

        }
    }
}
