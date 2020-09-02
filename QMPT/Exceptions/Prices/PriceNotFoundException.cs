using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Prices
{
    public class PriceNotFoundException : NotFoundException
    {
        public PriceNotFoundException() : base("Price")
        {

        }
    }
}
