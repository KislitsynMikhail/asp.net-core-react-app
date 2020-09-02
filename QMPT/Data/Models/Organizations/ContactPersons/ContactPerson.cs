using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Organizations.ContactPersons;
using QMPT.Models.Requests.Organizations.ContactPersons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Organizations.ContactPersons
{
    public class ContactPerson : BaseModel, IEditable<ContactPerson>, IRemovable, ICloneable
    {
        [MaxLength(ContactPersonRestrictions.fioMaxLength)]
        public string Name { get; set; }
        [MaxLength(ContactPersonRestrictions.positionMaxLength)]
        public string Position { get; set; }
        [ForeignKey(nameof(Organization))]
        public int OrganizationId { get; set; }

        #region Editable
        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        [ForeignKey(nameof(Original))]
        public int? OriginalId { get; set; }
        public bool IsRelevant { get; set; }

        public ContactPerson Original { get; set; }
        public User Editor { get; set; }
        #endregion Editable

        #region Removable
        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public User Remover { get; set; }
        #endregion Removable

        public Organization Organization { get; set; }

        public ICollection<ContactPersonEmail> Emails { get; set; }
        public ICollection<ContactPersonPhoneNumber> PhoneNumbers { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<ContactPerson> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(cp => cp.Creator)
                .WithMany(u => u.CreatedContactPeople)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cp => cp.Editor)
                .WithMany(u => u.EditedContactPeople)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(cp => cp.Remover)
                .WithMany(u => u.RemovedContactPeople)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public string CurrentVersionKey => $"{nameof(ContactPerson)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public ContactPerson() { }

        public ContactPerson(ContactPerson contactPerson) : base(contactPerson.CreatorId)
        {
            Name = contactPerson.Name;
            Position = contactPerson.Position;
            OrganizationId = contactPerson.OrganizationId;

            ModelEditingHandler.OnCopy(this, contactPerson);
            ModelRemovingHandler.Copy(this, contactPerson);
        }

        public ContactPerson(ContactPersonRequest contactPersonRequest, int creatorId) : base(creatorId)
        {
            CopyData(contactPersonRequest);
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
            CopyData(newData as ContactPersonRequest);

            ModelEditingHandler.OnEdit(this, editorId, version);
        }

        private void CopyData(ContactPersonRequest contactPersonRequest)
        {
            Name = contactPersonRequest.Name;
            Position = contactPersonRequest.Position;
            OrganizationId = contactPersonRequest.OrganizationId;
        }

        public object Clone()
        {
            return new ContactPerson(this);
        }
    }
}
