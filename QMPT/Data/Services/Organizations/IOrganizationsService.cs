using QMPT.Data.Models.Organizations;
using QMPT.Models.Requests;

namespace QMPT.Data.Services.Organizations
{
    public interface IOrganizationsService : IBaseOperation<Organization>
    {
        public void Insert(Organization organization);
        public Organization[] GetOrganizations(GetParameters organizationsGetParameters);
        public int GetOrganizationsCount(string name);
    }
}
