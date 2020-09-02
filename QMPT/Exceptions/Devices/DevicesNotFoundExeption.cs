using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.Devices
{
    public class DevicesNotFoundExeption : NotFoundException
    {
        public DevicesNotFoundExeption() : base("Device")
        {

        }
    }
}
