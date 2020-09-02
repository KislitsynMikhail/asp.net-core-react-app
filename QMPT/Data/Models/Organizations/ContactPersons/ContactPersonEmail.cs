using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Organizations.ContactPersons;
using QMPT.Models.Requests.Organizations.ContactPersons;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Organizations.ContactPersons
{
    public class ContactPersonEmail : BaseModel, IEditable<ContactPersonEmail>, IRemovable, ICloneable
    {
        [Required]
        [MaxLength(ContactPersonEmailRestrictions.emailMaxLength)]
        public string Email { get; set; }
        [ForeignKey(nameof(ContactPerson))]
        public int ContactPersonId { get; set; }

        #region Editable
        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        [ForeignKey(nameof(Original))]
        public int? OriginalId { get; set; }
        public bool IsRelevant { get; set; }

        public ContactPersonEmail Original { get; set; }
        public User Editor { get; set; }
        #endregion Editable

        #region Removable
        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public User Remover { get; set; }
        #endregion Removable

        public ContactPerson ContactPerson { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<ContactPersonEmail> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(cpe => cpe.Creator)
                .WithMany(u => u.CreatedContactPersonEmails)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cpe => cpe.Editor)
                .WithMany(u => u.EditedContactPersonEmails)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cpe => cpe.Remover)
                .WithMany(u => u.RemovedContactPersonEmails)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public string CurrentVersionKey => $"{nameof(ContactPersonEmail)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public ContactPersonEmail() { }

        public ContactPersonEmail(ContactPersonEmail contactPersonEmail) : base(contactPersonEmail.CreatorId)
        {
            Email = contactPersonEmail.Email;
            ContactPersonId = contactPersonEmail.ContactPersonId;

            ModelEditingHandler.OnCopy(this, contactPersonEmail);
            ModelRemovingHandler.Copy(this, contactPersonEmail);
        }

        public ContactPersonEmail(ContactPersonEmailRequest contactPersonEmailRequest, int creatorId) : base(creatorId)
        {
            CopyData(contactPersonEmailRequest);
            ModelEditingHandler.OnCreate(this);
        }

        private void OnEditModel(int editorId, int version)
            => ModelEditingHandler.OnEdit(this, editorId, version);

        public void CreateNewVersionValue()
        {
            KeyValuePair.CreateNewKeyValuePair(CurrentVersionKey, "1");
        }

        public void ChangeData(object newData, int editorId, int version)
        {
            CopyData(newData as ContactPersonEmailRequest);

            ModelEditingHandler.OnEdit(this, editorId, version);
        }

        private void CopyData(ContactPersonEmailRequest contactPersonEmailRequest)
        {
            Email = contactPersonEmailRequest.Value;
            ContactPersonId = contactPersonEmailRequest.ContactPersonId;
        }

        public object Clone()
        {
            return new ContactPersonEmail(this);
        }
    }
}
