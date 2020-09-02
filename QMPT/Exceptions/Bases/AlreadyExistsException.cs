using System.Collections.Generic;

namespace QMPT.Exceptions.Bases
{
    public class AlreadyExistsException : HttpResponseException
    {
        public override string Title => "Already exists";

        protected AlreadyExistsException(string parameter)
        {
            Data = parameter;
        }

        protected AlreadyExistsException(List<string> parameters)
        {
            Data = string.Join(", ", parameters);
        }
    }
}
