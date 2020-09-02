using QMPT.Models.Requests.Organizations;

namespace QMPT.Data.Models.Organizations
{
    public class Provider : Organization
    {
        public Provider() { }

        public Provider(OrganizationRequest organizationRequest, int creatorId) : base(organizationRequest, creatorId) { }

        public Provider(Organization organization) : base(organization) { }

        public override Organization GetCopiedOrganization()
        {
            return new Provider(this);
        }
    }
}
