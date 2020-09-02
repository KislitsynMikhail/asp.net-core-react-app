using QMPT.Helpers;
using System.Collections.Generic;

namespace QMPT.Exceptions.Validations
{
    public class MissingParametersException : HttpResponseException
    {
        public override string Title => ErrorMessages.missingParameters;

        public MissingParametersException(string parameter)
        {
            Data = parameter;
        }

        public MissingParametersException(List<string> parameters)
        {
            Data = string.Join(", ", parameters);
        }
    }
}
