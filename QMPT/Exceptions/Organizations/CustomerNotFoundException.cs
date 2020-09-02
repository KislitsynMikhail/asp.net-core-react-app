using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations
{
    public class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException() : base("Customer")
        {

        }
    }
}
