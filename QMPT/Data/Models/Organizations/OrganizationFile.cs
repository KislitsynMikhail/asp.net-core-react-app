using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Organizations
{
    public class OrganizationFile : BaseModel, IEditable<OrganizationFile>, IRemovable
    {
        [Required]
        [MaxLength(OrganizationFileRestrictions.nameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(OrganizationFileRestrictions.pathMaxLength)]
        public string Path { get; set; }
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

        public OrganizationFile Original { get; set; }
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

        public static void OnModelCreating(EntityTypeBuilder<OrganizationFile> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(of => of.Creator)
                .WithMany(u => u.CreatedOrganizationFiles)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(of => of.Editor)
                .WithMany(u => u.EditedOrganizationFiles)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(of => of.Remover)
                .WithMany(u => u.RemovedOrganizationFiles)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public string CurrentVersionKey => $"{nameof(OrganizationFile)}_{nameof(CurrentVersionKey)}_{OriginalId}";

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
