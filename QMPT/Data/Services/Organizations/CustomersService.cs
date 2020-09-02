using QMPT.Data.Models.Organizations;
using QMPT.Data.Services.Organizations;
using QMPT.Exceptions.Organizations;
using QMPT.Models.Requests;
using QMPT.Models.Requests.Organizations;
using System.Linq;

namespace QMPT.Data.Services
{
    public class CustomersService : BaseService, IOrganizationsService
    {
        public void Insert(Organization organization)
        {
            var customer = organization as Customer;
            
            CheckOnNull(customer);

            using var db = new DatabaseContext();

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void Update(Organization organization)
        {
            var customer = organization as Customer;

            CheckOnNull(customer);

            using var db = new DatabaseContext();

            db.Customers.Update(customer);
            db.SaveChanges();
        }

        public Organization Get(int customerId)
        {
            using var db = new DatabaseContext();

            var customer = db.Customers
                .FirstOrDefault(c => c.Id == customerId);

            CheckOnNull(customer);

            return customer;
        }

        public bool IsExists(int customerId)
        {
            using var db = new DatabaseContext();

            return db.Customers
                .Any(c => c.Id == customerId);
        }

        public void CheckExistence(int customerId)
        {
            if (!IsExists(customerId))
            {
                throw new CustomerNotFoundException();
            }
        }

        public Organization[] GetOrganizations(GetParameters getParameters)
        {
            return GetCustomers(getParameters);
        }

        public int GetOrganizationsCount(string name)
        {
            return GetCustomersAllCount(name);
        }

        public void Insert(object model)
        {
            var customer = model as Customer;
            CheckOnNull(customer);

            Insert(customer);
        }

        private Customer[] GetCustomers(GetParameters getParameters)
        {
            var name = getParameters.Name.ToLower();
            var page = getParameters.Page;
            var count = getParameters.Count;

            using var db = new DatabaseContext();

            return db.Customers
                .Where(customer => customer.Name.ToLower().Contains(name) && customer.IsRelevant && !customer.IsRemoved)
                .OrderBy(customer => customer.Name)
                .Skip(page * count)
                .Take(count)
                .ToArray();
        }

        private int GetCustomersAllCount(string name)
        {
            name = name.ToLower();

            using var db = new DatabaseContext();

            return db.Customers
                .Where(customer => customer.Name.ToLower().Contains(name) && customer.IsRelevant && !customer.IsRemoved)
                .Count();
        }

        private void CheckOnNull(Customer customer)
        {
            CheckOnNull(customer, new CustomerNotFoundException());
        }
    }
}
