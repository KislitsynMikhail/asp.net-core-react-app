using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations.ContactPersons.PhoneNumbers
{
    public class ContactPersonPhoneNumberNotFoundException : NotFoundException
    {
        public ContactPersonPhoneNumberNotFoundException() : base("Contact person phone number")
        {

        }
    }
}
