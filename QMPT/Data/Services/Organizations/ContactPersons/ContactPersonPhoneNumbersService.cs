using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.Models;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Exceptions.Organizations.ContactPersons.PhoneNumbers;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Data.Services
{
    public class ContactPersonPhoneNumbersService : BaseService, IBaseOperation<ContactPersonPhoneNumber>
    {
        public void Insert(ContactPersonPhoneNumber contactPersonPhoneNumber)
        {
            CheckContactPersonExistence(contactPersonPhoneNumber.ContactPersonId);

            using var db = new DatabaseContext();
            db.ContactPersonPhoneNumbers.Add(contactPersonPhoneNumber);
            db.SaveChanges();
        }

        public void Update(ContactPersonPhoneNumber contactPersonPhoneNumber)
        {
            using var db = new DatabaseContext();
            db.ContactPersonPhoneNumbers.Update(contactPersonPhoneNumber);
            db.SaveChanges();
        }

        public ContactPersonPhoneNumber Get(int contactPersonPhoneNumberId)
        {
            using var db = new DatabaseContext();

            var contactPersonPhoneNumber = db.ContactPersonPhoneNumbers
                .FirstOrDefault(cppn => cppn.Id == contactPersonPhoneNumberId);
            CheckOnNull(contactPersonPhoneNumber);

            return contactPersonPhoneNumber;
        }

        public List<ContactPersonPhoneNumber> GetMany(int contactPersonId)
        {
            CheckContactPersonExistence(contactPersonId);

            using var db = new DatabaseContext();

            return db.ContactPersonPhoneNumbers
                 .Where(cppn => cppn.ContactPersonId == contactPersonId)
                 .ToList();
        }

        public List<ContactPersonPhoneNumber> GetMany(int[] contactPeopleId)
        {
            using var db = new DatabaseContext();

            return db.ContactPersonPhoneNumbers
                 .Where(cppn => contactPeopleId.Contains(cppn.ContactPersonId))
                 .ToList();
        }

        public void Delete(ContactPersonPhoneNumber contactPersonPhoneNumber)
        {
            CheckExistence(contactPersonPhoneNumber.Id);

            using var db = new DatabaseContext();
            db.ContactPersonPhoneNumbers.Remove(contactPersonPhoneNumber);
            db.SaveChanges();
        }

        public bool IsExists(int contactPersonPhoneNumberId)
        {
            using var db = new DatabaseContext();

            return db.ContactPersonPhoneNumbers
                .Any(cppn => cppn.Id == contactPersonPhoneNumberId);
        }

        public void CheckExistence(int contactPersonPhoneNumberId)
        {
            if (!IsExists(contactPersonPhoneNumberId))
            {
                throw new ContactPersonPhoneNumberNotFoundException();
            }
        }

        private void CheckContactPersonExistence(int contactPersonId)
        {
            var contactPersonsService = new ContactPersonsService();
            contactPersonsService.CheckExistence(contactPersonId);
        }

        public void Insert(object model)
        {
            var contactPersonPhoneNumber = model as ContactPersonPhoneNumber;
            CheckOnNull(contactPersonPhoneNumber);

            Insert(model);
        }

        private void CheckOnNull(ContactPersonPhoneNumber contactPersonPhoneNumber)
        {
            CheckOnNull(contactPersonPhoneNumber, new ContactPersonPhoneNumberNotFoundException());
        }
    }
}
