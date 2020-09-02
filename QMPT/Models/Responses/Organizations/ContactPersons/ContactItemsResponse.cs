using QMPT.Data.Models.Organizations.ContactPersons;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Models.Responses.Organizations.ContactPersons
{
    public class ContactItemsResponse
    {
        public List<ContactPersonEmailResponse> Emails { get; set; }
        public List<ContactPersonPhoneNumberResponse> PhoneNumbers { get; set; }

        public ContactItemsResponse(List<ContactPersonEmail> emails, List<ContactPersonPhoneNumber> phoneNumbers)
        {
            Emails = emails.Select(email => new ContactPersonEmailResponse(email)).ToList();
            PhoneNumbers = phoneNumbers.Select(phoneNumber => new ContactPersonPhoneNumberResponse(phoneNumber)).ToList();
        }
    }
}
