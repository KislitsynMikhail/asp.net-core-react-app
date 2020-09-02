
namespace QMPT.Data.ModelRestrictions.Organizations
{
    public static class OrganizationRestrictions
    {
        public const int nameMaxLength = 200;
        public const int nameMinLength = 1;

        public const int innMaxLength = 12;
        public const int innMinLength = 10;

        public const int kppLength = 9;
        public const int ogrnLength = 13;

        public const int okpoMaxLength = 10;
        public const int okpoMinLength = 8;

        public const int legalAddressMaxLength = 200;
        public const int legalAddressMinLength = 5;

        public const int emailMaxLength = 100;
        public const int emailMinLength = 5;

        public const int phoneNumberMaxLength = 20;
        public const int phoneNumberMinLength = 5;

        public const int settlementAccountMaxLength = 100;
        public const int settlementAccountMinLength = 5;

        public const int corporateAccountMaxLength = 100;
        public const int corporateAccountMinLength = 5;

        public const int bikLength = 9;

        public const int managerPositionMaxLength = 100;
        public const int managerPositionMinLength = 5;

        public const int baseMaxLength = 100;
        public const int baseMinLength = 2;

        public const int supervisorFIOMaxLength = 100;
        public const int supervisorFIOMinLength = 5;

        public const int chiefAccountantMaxLength = 100;
        public const int chiefAccountantMinLength = 5;

        public const int discriminatorLength = 8;
    }
}
