using System.Collections.Generic;

namespace QMPT.Exceptions.Bases
{
    public class ExpiredParametersException : HttpResponseException
    {
        public override string Title => "Expired parameters";

        protected ExpiredParametersException(string parameter)
        {
            Data = parameter;
        }

        protected ExpiredParametersException(List<string> parameters)
        {
            Data = string.Join(", ", parameters);
        }
    }
}
