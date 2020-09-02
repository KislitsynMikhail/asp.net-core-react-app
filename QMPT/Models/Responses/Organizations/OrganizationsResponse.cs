using QMPT.Data.Models.Organizations;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Models.Responses.Organizations
{
    public class OrganizationsResponse
    {
        public List<OrganizationResponse> Organizations { get; set; }
        public int Count { get; set; }

        public OrganizationsResponse(Organization[] organizations, int count)
        {
            Organizations = organizations
                .Select(organization => new OrganizationResponse(organization))
                .ToList();
            Count = count;
        }
    }
}
