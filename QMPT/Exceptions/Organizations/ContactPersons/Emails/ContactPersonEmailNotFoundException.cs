using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations.ContactPersons.Emails
{
    public class ContactPersonEmailNotFoundException : NotFoundException
    {
        public ContactPersonEmailNotFoundException() : base("Contact person email")
        {

        }
    }
}
