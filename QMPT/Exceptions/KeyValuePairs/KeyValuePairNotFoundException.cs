using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.KeyValuePairs
{
    public class KeyValuePairNotFoundException : NotFoundException
    {
        public KeyValuePairNotFoundException() : base("Key value pair")
        {

        }
    }
}
