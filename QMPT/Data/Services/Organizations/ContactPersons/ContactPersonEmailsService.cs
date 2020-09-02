using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.Models;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Exceptions.Organizations.ContactPersons.Emails;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Data.Services
{
    public class ContactPersonEmailsService : BaseService, IBaseOperation<ContactPersonEmail>
    {
        public void Insert(ContactPersonEmail contactPersonEmail)
        {
            CheckContactPersonExistence(contactPersonEmail.ContactPersonId);

            using var db = new DatabaseContext();
            db.ContactPersonEmails.Add(contactPersonEmail);
            db.SaveChanges();
        }

        public void Update(ContactPersonEmail contactPersonEmail)
        {
            using var db = new DatabaseContext();
            db.ContactPersonEmails.Update(contactPersonEmail);
            db.SaveChanges();
        }

        public ContactPersonEmail Get(int contactPersonEmailId)
        {
            using var db = new DatabaseContext();

            var contactPersonEmail = db.ContactPersonEmails
                .FirstOrDefault(cpe => cpe.Id == contactPersonEmailId);
            CheckOnNull(contactPersonEmail);

            return contactPersonEmail;
        }

        public List<ContactPersonEmail> GetMany(int contactPersonId)
        {
            CheckContactPersonExistence(contactPersonId);

            using var db = new DatabaseContext();

            return db.ContactPersonEmails
                .Where(cpe => cpe.ContactPersonId == contactPersonId)
                .ToList();
        }

        public List<ContactPersonEmail> GetMany(int[] contactPeopleId)
        {
            using var db = new DatabaseContext();

            return db.ContactPersonEmails
                .Where(cpe => contactPeopleId.Contains(cpe.ContactPersonId))
                .ToList();
        }

        public void Delete(ContactPersonEmail contactPersonEmail)
        {
            CheckExistence(contactPersonEmail.Id);

            using var db = new DatabaseContext();
            db.ContactPersonEmails.Remove(contactPersonEmail);
            db.SaveChanges();
        }

        public bool IsExists(int contactPersonEmailId)
        {
            using var db = new DatabaseContext();

            return db.ContactPersonEmails
                 .Any(cpe => cpe.Id == contactPersonEmailId);
        }

        public void CheckExistence(int contactPersonEmailId)
        {
            if (!IsExists(contactPersonEmailId))
            {
                throw new ContactPersonEmailNotFoundException();
            }
        }

        public void Insert(object model)
        {
            var contactPersonEmail = model as ContactPersonEmail;
            CheckOnNull(contactPersonEmail);

            Insert(contactPersonEmail);
        }

        private void CheckContactPersonExistence(int contactPersonId)
        {
            var contactPersonsService = new ContactPersonsService();
            contactPersonsService.CheckExistence(contactPersonId);
        }

        private void CheckOnNull(ContactPersonEmail contactPersonEmail)
        {
            CheckOnNull(contactPersonEmail, new ContactPersonEmailNotFoundException());
        }
    }
}
