using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Users
{
    public class UserLoginAlreadyExistsException : AlreadyExistsException
    {
        public UserLoginAlreadyExistsException() : base("User login")
        {

        }
    }
}
