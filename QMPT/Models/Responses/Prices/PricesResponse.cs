using QMPT.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Models.Responses.Prices
{
    public class PricesResponse
    {
        public List<PriceResponse> Prices { get; set; }
        public int Count { get; set; }

        public PricesResponse(Price[] prices, int count)
        {
            Prices = prices
                .Select(price => new PriceResponse(price))
                .ToList();
            Count = count;
        }
    }
}
