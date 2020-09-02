using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Organizations;
using QMPT.Models.Requests.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Organizations
{
    public class OrganizationNote : BaseModel, IEditable<OrganizationNote>, IRemovable
    {
        [Required]
        [MaxLength(OrganizationNoteRestrictions.noteMaxLength)]
        public string Note { get; set; }
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

        public OrganizationNote Original { get; set; }
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

        public static void OnModelCreating(EntityTypeBuilder<OrganizationNote> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(on => on.Creator)
                .WithMany(u => u.CreatedOrganizationNotes)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(on => on.Editor)
                .WithMany(u => u.EditedOrganizationNotes)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(on => on.Remover)
                .WithMany(u => u.RemovedOrganizationNotes)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public string CurrentVersionKey => $"{nameof(OrganizationNote)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public OrganizationNote() { }

        public OrganizationNote(OrganizationNoteRequest organizationNoteRequest)
        {
            Note = organizationNoteRequest.Note;
            OrganizationId = organizationNoteRequest.OrganizationId;
        }

        private void OnEditModel(int editorId, int version)
            => ModelEditingHandler.OnEdit(this, editorId, version);

        public void CreateNewVersionValue()
        {
            KeyValuePair.CreateNewKeyValuePair(CurrentVersionKey, "1");
        }

        public void ChangeData(object newData, int editorId, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}
