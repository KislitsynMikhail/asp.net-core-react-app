
namespace QMPT.Exceptions.Users
{
    public class UserWasRemovedException : HttpResponseException
    {
        public override string Title => "An attempt to gain access to a removed resource";
        public override string Data => "User";
    }
}
