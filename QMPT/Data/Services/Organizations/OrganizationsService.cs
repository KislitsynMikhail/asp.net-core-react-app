using QMPT.Data.Models.Organizations;
using QMPT.Exceptions.Organizations;
using QMPT.Models.Requests;
using QMPT.Models.Requests.Organizations;
using System.Linq;

namespace QMPT.Data.Services.Organizations
{
    public class OrganizationsService : BaseService, IOrganizationsService
    {
        public void Update(Organization organization)
        {
            CheckExistence(organization.Id);
            CheckUsersExistence(organization.CreatorId, organization.EditorId);

            using var db = new DatabaseContext();
            db.Organizations.Update(organization);
            db.SaveChanges();
        }

        public Organization Get(int organizationId)
        {
            using var db = new DatabaseContext();

            var organization = db.Organizations
                .FirstOrDefault(o => o.Id == organizationId);
            CheckOnNull(organization);

            return organization;
        }

        public bool IsExists(int organizationId)
        {
            using var db = new DatabaseContext();

            return db.Organizations
                .Any(o => o.Id == organizationId);
        }

        public void CheckExistence(int organizationId)
        {
            if (!IsExists(organizationId))
            {
                throw new OrganizationNotFoundException();
            }
        }

        private void CheckUsersExistence(int? creatorId, int? editorId = null, int? removerId = null)
        {
            var usersService = new UsersService();

            if (creatorId != null)
                usersService.CheckExistence((int)creatorId);
            if (editorId != null && editorId != creatorId)
                usersService.CheckExistence((int)editorId);
            if (removerId != null && removerId != creatorId)
                usersService.CheckExistence((int)removerId);
        }

        public void Insert(Organization organization)
        {
            CheckUsersExistence(organization.CreatorId, organization.EditorId);

            using var db = new DatabaseContext();
            db.Organizations.Add(organization);
            db.SaveChanges();
        }

        public Organization[] GetOrganizations(GetParameters GetParameters)
        {
            var name = GetParameters.Name.ToLower();
            var page = GetParameters.Page;
            var count = GetParameters.Count;

            using var db = new DatabaseContext();

            return db.Organizations
                .Where(organization => organization.Name.ToLower().Contains(name) && organization.IsRelevant && !organization.IsRemoved)
                .OrderBy(organization => organization.Name)
                .Skip(page * count)
                .Take(count)
                .ToArray();
        }

        public int GetOrganizationsCount(string name)
        {
            name = name.ToLower();

            using var db = new DatabaseContext();

            return db.Organizations
                .Where(organization => organization.Name.ToLower().Contains(name) && organization.IsRelevant && !organization.IsRemoved)
                .Count();
        }

        public void Insert(object model)
        {
            var organization = model as Organization;
            CheckOnNull(organization);

            Insert(organization);
        }

        private void CheckOnNull(Organization organization)
        {
            CheckOnNull(organization, new OrganizationNotFoundException());
        }
    }
}
