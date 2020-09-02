using Microsoft.EntityFrameworkCore;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Data.Services.Organizations;
using QMPT.Exceptions.Organizations.ContactPersons;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Data.Services
{
    public class ContactPersonsService : BaseService, IBaseOperation<ContactPerson>
    {
        public void Insert(ContactPerson contactPerson)
        {
            var organizationsService = new OrganizationsService();
            organizationsService.CheckExistence(contactPerson.OrganizationId);

            using var db = new DatabaseContext();
            db.ContactPeople.Add(contactPerson);
            db.SaveChanges();
        }

        public void Update(ContactPerson contactPerson)
        {
            using var db = new DatabaseContext();
            db.ContactPeople.Update(contactPerson);
            db.SaveChanges();
        }

        public ContactPerson Get(int contactPersonId)
        {
            using var db = new DatabaseContext();

            var contactPerson = db.ContactPeople
                .FirstOrDefault(cp => cp.Id == contactPersonId);
            CheckOnNull(contactPerson);

            return contactPerson;
        }

        public List<ContactPerson> GetContactPeople(int organizationId)
        {
            using var db = new DatabaseContext();

            var contactPeople = db.ContactPeople
                .Include(contactPerson => contactPerson.Emails)
                .Include(contactPerson => contactPerson.PhoneNumbers)
                .Where(contactPerson => contactPerson.OrganizationId == organizationId && contactPerson.IsRelevant && !contactPerson.IsRemoved)
                .ToList();

            foreach(var contactPerson in contactPeople)
            {
                contactPerson.Emails = contactPerson.Emails
                    .Where(email => email.IsRelevant && !email.IsRemoved)
                    .ToList();

                contactPerson.PhoneNumbers = contactPerson.PhoneNumbers
                    .Where(phoneNumber => phoneNumber.IsRelevant && !phoneNumber.IsRemoved)
                    .ToList();
            }

            return contactPeople;
        }

        public void Delete(ContactPerson contactPerson)
        {
            CheckExistence(contactPerson.Id);

            using var db = new DatabaseContext();
            db.ContactPeople.Remove(contactPerson);
            db.SaveChanges();
        }

        public bool IsExists(int contactPersonId)
        {
            using var db = new DatabaseContext();

            return db.ContactPeople
                .Any(cp => cp.Id == contactPersonId);
        }

        public void CheckExistence(int contactPersonId)
        {
            if (!IsExists(contactPersonId))
            {
                throw new ContactPersonNotFoundException();
            }
        }

        public void Insert(object model)
        {
            var contactPerson = model as ContactPerson;
            CheckOnNull(contactPerson);

            Insert(contactPerson);
        }

        private void CheckOnNull(ContactPerson contactPerson)
        {
            CheckOnNull(contactPerson, new ContactPersonNotFoundException());
        }
    }
}
