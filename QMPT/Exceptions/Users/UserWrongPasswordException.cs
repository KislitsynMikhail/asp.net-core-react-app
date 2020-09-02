
namespace QMPT.Exceptions.Users
{
    public class UserWrongPasswordException : HttpResponseException
    {
        public override string Title => "Wrong password";
    }
}
