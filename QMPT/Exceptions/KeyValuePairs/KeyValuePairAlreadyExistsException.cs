using QMPT.Exceptions.Bases;

namespace QMPT.Exceptions.KeyValuePairs
{
    public class KeyValuePairAlreadyExistsException : AlreadyExistsException
    {
        public KeyValuePairAlreadyExistsException() : base("Key value pair")
        {

        }
    }
}
