using QMPT.Data.ModelRestrictions.Organizations;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Organizations
{
    public class OrganizationRequest
    {
        //[MinLength(OrganizationRestrictions.nameMinLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.nameMaxLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        public string Name { get; set; }

       // [MinLength(OrganizationRestrictions.innMinLength,               ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.innMaxLength,               ErrorMessage = ErrorMessages.invalidParameters)]
        public string INN { get; set; }

        //[MinLength(OrganizationRestrictions.kppLength,                  ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.kppLength,                  ErrorMessage = ErrorMessages.invalidParameters)]
        public string KPP { get; set; }

        //[MinLength(OrganizationRestrictions.ogrnLength,                 ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.ogrnLength,                 ErrorMessage = ErrorMessages.invalidParameters)]
        public string OGRN { get; set; }

        //[MinLength(OrganizationRestrictions.okpoMinLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.okpoMaxLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        public string OKPO { get; set; }

        //[MinLength(OrganizationRestrictions.legalAddressMinLength,      ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.legalAddressMaxLength,      ErrorMessage = ErrorMessages.invalidParameters)]
        public string LegalAddress { get; set; }

        [Email(                                                         ErrorMessage = ErrorMessages.invalidParameters)]
       // [MinLength(OrganizationRestrictions.emailMinLength,             ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.emailMaxLength,             ErrorMessage = ErrorMessages.invalidParameters)]
        public string Email { get; set; }

       // [MinLength(OrganizationRestrictions.phoneNumberMinLength,       ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.phoneNumberMaxLength,       ErrorMessage = ErrorMessages.invalidParameters)]
        public string PhoneNumber { get; set; }

        //[MinLength(OrganizationRestrictions.settlementAccountMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.settlementAccountMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string SettlementAccount { get; set; }

        //[MinLength(OrganizationRestrictions.corporateAccountMinLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.corporateAccountMaxLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        public string CorporateAccount { get; set; }

       // [MinLength(OrganizationRestrictions.bikLength,                  ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.bikLength,                  ErrorMessage = ErrorMessages.invalidParameters)]
        public string BIK { get; set; }

        //[MinLength(OrganizationRestrictions.managerPositionMinLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.managerPositionMaxLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        public string ManagerPosition { get; set; }

        //[MinLength(OrganizationRestrictions.baseMinLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.baseMaxLength,              ErrorMessage = ErrorMessages.invalidParameters)]
        public string Base { get; set; }

        //[MinLength(OrganizationRestrictions.supervisorFIOMinLength,     ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.supervisorFIOMaxLength,     ErrorMessage = ErrorMessages.invalidParameters)]
        public string SupervisorFIO { get; set; }

        //[MinLength(OrganizationRestrictions.chiefAccountantMinLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationRestrictions.chiefAccountantMaxLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        public string ChiefAccountant { get; set; }

        public bool IsUSN { get; set; }
    }
}
