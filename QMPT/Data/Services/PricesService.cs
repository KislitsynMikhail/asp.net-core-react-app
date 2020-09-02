using QMPT.Data.Models;
using QMPT.Exceptions.Prices;
using QMPT.Models.Requests.Prices;
using System.Linq;

namespace QMPT.Data.Services
{
    public class PricesService : BaseService, IBaseOperation<Price>
    {
        public void Insert(Price price)
        {
            using var db = new DatabaseContext();

            db.Prices.Add(price);
            db.SaveChanges();
        }

        public void Update(Price price)
        {
            using var db = new DatabaseContext();

            db.Prices.Update(price);
            db.SaveChanges();
        }

        public Price Get(int priceId)
        {
            using var db = new DatabaseContext();

            var price = db.Prices
                .FirstOrDefault(price => price.Id == priceId);
            if (price is null)
            {
                throw new PriceNotFoundException();
            }

            return price;
        }

        public Price[] Get(PricesGetParameters pricesGetParameters)
        {
            var type = pricesGetParameters.Type;
            var page = pricesGetParameters.Page;
            var count = pricesGetParameters.Count;
            var name = pricesGetParameters.Name.ToLower();

            using var db = new DatabaseContext();

            return db.Prices
                .Where(price => price.Type == type && price.Name.ToLower().Contains(name) && price.IsRelevant && !price.IsRemoved)
                .OrderBy(price => price.Name)
                .Skip(page * count)
                .Take(count)
                .ToArray();
        }

        public int GetAllCount(PricesGetParameters pricesGetParameters)
        {
            var type = pricesGetParameters.Type;
            var name = pricesGetParameters.Name.ToLower();

            using var db = new DatabaseContext();

            return db.Prices
                .Where(price => price.Type == type && price.Name.ToLower().Contains(name) && price.IsRelevant && !price.IsRemoved)
                .Count();
        }

        public void Insert(object model)
        {
            var price = model as Price;
            CheckOnNull(price);

            Insert(price);
        }

        private void CheckOnNull(Price price)
        {
            CheckOnNull(price, new PriceNotFoundException());
        }
    }
}
