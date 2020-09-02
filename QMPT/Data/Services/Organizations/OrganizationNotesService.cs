using QMPT.Data.Models.Organizations;
using QMPT.Exceptions.Organizations.Notes;
using System.Linq;

namespace QMPT.Data.Services.Organizations
{
    public class OrganizationNotesService
    {
        public void Insert(OrganizationNote organizationNote)
        {
            CheckOrganizationExists(organizationNote.OrganizationId);

            using var db = new DatabaseContext();
            db.OrganizationNotes.Add(organizationNote);
            db.SaveChanges();
        }

        public OrganizationNote Get(int organizationNoteId)
        {
            using var db = new DatabaseContext();

            var organizationNote = db.OrganizationNotes
                .FirstOrDefault(on => on.Id == organizationNoteId);
            if (organizationNote is null)
            {
                throw new OrganizationNoteNotFoundException();
            }

            return organizationNote;
        }

        public void Delete(OrganizationNote organizationNote)
        {
            CheckExistence(organizationNote.Id);

            using var db = new DatabaseContext();
            db.OrganizationNotes.Remove(organizationNote);
            db.SaveChanges();
        }

        public bool IsExists(int organizationNoteId)
        {
            using var db = new DatabaseContext();

            return db.OrganizationNotes
                .Any(cn => cn.Id == organizationNoteId);
        }

        public void CheckExistence(int organizationNoteId)
        {
            if (!IsExists(organizationNoteId))
            {
                throw new OrganizationNoteNotFoundException();
            }
        }

        private void CheckOrganizationExists(int organizationId)
        {
            var organizationsService = new OrganizationsService();
            organizationsService.CheckExistence(organizationId);
        }
    }
}
