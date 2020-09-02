using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations.ContactPersons
{
    public class ContactPersonNotFoundException : NotFoundException
    {
        public ContactPersonNotFoundException() : base("Contact person")
        {

        }
    }
}
