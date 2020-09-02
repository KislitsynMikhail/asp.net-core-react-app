using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations
{
    public class ProviderNotFoundException : NotFoundException
    {
        public ProviderNotFoundException() : base("Provider")
        {

        }
    }
}
