using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Organizations.Notes
{
    public class OrganizationNoteNotFoundException : NotFoundException
    {
        public OrganizationNoteNotFoundException() : base("Organization note")
        {

        }
    }
}
