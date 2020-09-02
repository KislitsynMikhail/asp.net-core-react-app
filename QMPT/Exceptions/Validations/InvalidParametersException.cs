using QMPT.Helpers;
using System.Collections.Generic;

namespace QMPT.Exceptions.Validations
{
    public class InvalidParametersException : HttpResponseException
    {
        public override string Title => ErrorMessages.invalidParameters;

        public InvalidParametersException(string parameter)
        {
            Data = parameter;
        }

        public InvalidParametersException(List<string> parameters)
        {
            Data = string.Join(", ", parameters);
        }
    }
}
