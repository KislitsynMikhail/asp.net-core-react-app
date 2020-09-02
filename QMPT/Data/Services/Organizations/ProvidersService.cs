using QMPT.Data.Models.Organizations;
using QMPT.Exceptions.Organizations;
using QMPT.Models.Requests;
using System.Linq;

namespace QMPT.Data.Services.Organizations
{
    public class ProvidersService : BaseService, IOrganizationsService
    {
        public Organization Get(int providerId)
        {
            using var db = new DatabaseContext();

            var provider = db.Providers
                .FirstOrDefault(p => p.Id == providerId);
            CheckOnNull(provider);

            return provider;
        }

        public Organization[] GetOrganizations(GetParameters getParameters)
        {
            return GetProviders(getParameters);
        }

        public int GetOrganizationsCount(string name)
        {
            return GetProvidersAllCount(name);
        }

        public void Insert(Organization organization)
        {
            var provider = organization as Provider;
            CheckOnNull(provider);

            using var db = new DatabaseContext();

            db.Providers.Add(provider);
            db.SaveChanges();
        }

        public void Update(Organization organization)
        {
            var provider = organization as Provider;
            CheckOnNull(provider);

            using var db = new DatabaseContext();

            db.Providers.Update(provider);
            db.SaveChanges();
        }

        public void Insert(object model)
        {
            var provider = model as Provider;
            CheckOnNull(provider);

            Insert(provider);
        }

        private Provider[] GetProviders(GetParameters getParameters)
        {
            var name = getParameters.Name.ToLower();
            var page = getParameters.Page;
            var count = getParameters.Count;

            using var db = new DatabaseContext();

            return db.Providers
                .Where(provider => provider.Name.ToLower().Contains(name) && provider.IsRelevant && !provider.IsRemoved)
                .OrderBy(provider => provider.Name)
                .Skip(page * count)
                .Take(count)
                .ToArray();
        }

        private int GetProvidersAllCount(string name)
        {
            name = name.ToLower();

            using var db = new DatabaseContext();

            return db.Providers
                .Where(provider => provider.Name.ToLower().Contains(name) && provider.IsRelevant && !provider.IsRemoved)
                .Count();
        }

        private void CheckOnNull(Provider provider)
        {
            CheckOnNull(provider, new ProviderNotFoundException());
        }
    }
}
