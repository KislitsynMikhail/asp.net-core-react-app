using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations
{
    public class OrganizationNotFoundException : NotFoundException
    {
        public OrganizationNotFoundException() : base("Ogranization")
        {

        }
    }
}
