using QMPT.Models.Requests.Organizations;

namespace QMPT.Data.Models.Organizations
{
    public class Customer : Organization
    {
        public Customer() { }

        public Customer(OrganizationRequest organizationRequest, int creatorId) : base(organizationRequest, creatorId) { }

        public Customer(Organization organization) : base(organization) { }

        public override Organization GetCopiedOrganization()
        {
            return new Customer(this);
        }
    }
}
