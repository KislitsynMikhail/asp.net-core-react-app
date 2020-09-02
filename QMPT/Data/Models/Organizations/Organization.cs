using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Organizations;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Models.Requests.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Organizations
{
    public abstract class Organization : BaseModel, IEditable<Organization>, IRemovable, ICloneable
    {
        #region Columns
        [MaxLength(OrganizationRestrictions.nameMaxLength)]
        public string Name { get; set; }
        [MaxLength(OrganizationRestrictions.innMaxLength)]
        public string INN { get; set; }
        [MaxLength(OrganizationRestrictions.kppLength)]
        public string KPP { get; set; }
        [MaxLength(OrganizationRestrictions.ogrnLength)]
        public string OGRN { get; set; }
        [MaxLength(OrganizationRestrictions.okpoMaxLength)]
        public string OKPO { get; set; }
        [MaxLength(OrganizationRestrictions.legalAddressMaxLength)]
        public string LegalAddress { get; set; }
        [MaxLength(OrganizationRestrictions.emailMaxLength)]
        public string Email { get; set; }
        [MaxLength(OrganizationRestrictions.phoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
        [MaxLength(OrganizationRestrictions.settlementAccountMaxLength)]
        public string SettlementAccount { get; set; }
        [MaxLength(OrganizationRestrictions.corporateAccountMaxLength)]
        public string CorporateAccount { get; set; }
        [MaxLength(OrganizationRestrictions.bikLength)]
        public string BIK { get; set; }
        [MaxLength(OrganizationRestrictions.managerPositionMaxLength)]
        public string ManagerPosition { get; set; }
        [MaxLength(OrganizationRestrictions.baseMaxLength)]
        public string Base { get; set; }
        [MaxLength(OrganizationRestrictions.supervisorFIOMaxLength)]
        public string SupervisorFIO { get; set; }
        [MaxLength(OrganizationRestrictions.chiefAccountantMaxLength)]
        public string ChiefAccountant { get; set; }
        public bool IsUSN { get; set; }

        #region Editable
        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        [ForeignKey(nameof(Original))]
        public int? OriginalId { get; set; }
        public bool IsRelevant { get; set; }

        public Organization Original { get; set; }
        public User Editor { get; set; }
        #endregion Editable

        #region Removable
        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public User Remover { get; set; }
        #endregion Removable

        [Required]
        [MaxLength(OrganizationRestrictions.discriminatorLength)]
        public string Discriminator { get; set; }

        #endregion Columns

        public ICollection<ContactPerson> ContactPeople { get; set; }
        public ICollection<OrganizationNote> Notes { get; set; }
        public ICollection<OrganizationFile> Files { get; set; }

        public string CurrentVersionKey => $"{nameof(Organization)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public static void OnModelCreating(EntityTypeBuilder<Organization> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(o => o.Creator)
                .WithMany(u => u.CreatedOrganizations)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(o => o.Editor)
                .WithMany(u => u.EditedOrganizations)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(o => o.Remover)
                .WithMany(u => u.RemovedOrganizations)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public Organization() { }

        public Organization(OrganizationRequest organizationRequest, int creatorId) : base(creatorId)
        {
            ModelEditingHandler.OnCreate(this);
            CopyData(organizationRequest);
        }

        public Organization(Organization organization) : base(organization.CreatorId)
        {
            ModelEditingHandler.OnCopy(this, organization);
            ModelRemovingHandler.Copy(this, organization);

            Name = organization.Name;
            INN = organization.INN;
            KPP = organization.KPP;
            OGRN = organization.OGRN;
            OKPO = organization.OKPO;
            LegalAddress = organization.LegalAddress;
            Email = organization.Email;
            PhoneNumber = organization.PhoneNumber;
            SettlementAccount = organization.SettlementAccount;
            CorporateAccount = organization.CorporateAccount;
            BIK = organization.BIK;
            ManagerPosition = organization.ManagerPosition;
            Base = organization.Base;
            SupervisorFIO = organization.SupervisorFIO;
            ChiefAccountant = organization.ChiefAccountant;
            IsUSN = organization.IsUSN;
        }

        public abstract Organization GetCopiedOrganization();

        public void CreateNewVersionValue()
        {
            KeyValuePair.CreateNewKeyValuePair(CurrentVersionKey, "1");
        }

        public void ChangeData(object organizationRequest, int editorId, int version)
        {
            CopyData(organizationRequest as OrganizationRequest);

            OnEditModel(editorId, version);
        }

        private void OnEditModel(int editorId, int version)
            => ModelEditingHandler.OnEdit(this, editorId, version);

        private void CopyData(OrganizationRequest organizationRequest)
        {
            Name = organizationRequest.Name;
            INN = organizationRequest.INN;
            KPP = organizationRequest.KPP;
            OGRN = organizationRequest.OGRN;
            OKPO = organizationRequest.OKPO;
            LegalAddress = organizationRequest.LegalAddress;
            Email = organizationRequest.Email;
            PhoneNumber = organizationRequest.PhoneNumber;
            SettlementAccount = organizationRequest.SettlementAccount;
            CorporateAccount = organizationRequest.CorporateAccount;
            BIK = organizationRequest.BIK;
            ManagerPosition = organizationRequest.ManagerPosition;
            Base = organizationRequest.Base;
            SupervisorFIO = organizationRequest.SupervisorFIO;
            ChiefAccountant = organizationRequest.ChiefAccountant;
            IsUSN = organizationRequest.IsUSN;
        }

        public object Clone()
        {
            return GetCopiedOrganization();
        }
    }
}
