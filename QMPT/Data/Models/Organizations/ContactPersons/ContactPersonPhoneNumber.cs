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
    public class ContactPersonPhoneNumber : BaseModel, IEditable<ContactPersonPhoneNumber>, IRemovable, ICloneable
    {
        [Required]
        [MaxLength(ContactPersonPhoneNumberRestrictions.phoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
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

        public ContactPersonPhoneNumber Original { get; set; }
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

        public static void OnModelCreating(EntityTypeBuilder<ContactPersonPhoneNumber> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(cppn => cppn.Creator)
                .WithMany(u => u.CreatedContactPersonPhoneNumbers)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cppn => cppn.Editor)
                .WithMany(u => u.EditedContactPersonPhoneNumbers)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cppn => cppn.Remover)
                .WithMany(u => u.RemovedContactPersonPhoneNumbers)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public string CurrentVersionKey => $"{nameof(ContactPersonPhoneNumber)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public ContactPersonPhoneNumber() { }

        public ContactPersonPhoneNumber(ContactPersonPhoneNumber contactPersonPhoneNumber) : base(contactPersonPhoneNumber.CreatorId)
        {
            PhoneNumber = contactPersonPhoneNumber.PhoneNumber;
            ContactPersonId = contactPersonPhoneNumber.ContactPersonId;

            ModelEditingHandler.OnCopy(this, contactPersonPhoneNumber);
            ModelRemovingHandler.Copy(this, contactPersonPhoneNumber);
        }

        public ContactPersonPhoneNumber(ContactPersonPhoneNumberRequest contactPersonPhoneNumberRequest, int creatorId) : base(creatorId)
        {
            CopyData(contactPersonPhoneNumberRequest);
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
            CopyData(newData as ContactPersonPhoneNumberRequest);

            ModelEditingHandler.OnEdit(this, editorId, version);
        }

        private void CopyData(ContactPersonPhoneNumberRequest contactPersonPhoneNumberRequest)
        {
            PhoneNumber = contactPersonPhoneNumberRequest.Value;
            ContactPersonId = contactPersonPhoneNumberRequest.ContactPersonId;
        }

        public object Clone()
        {
            return new ContactPersonPhoneNumber(this);
        }
    }
}
