using System.Collections.Generic;

namespace QMPT.Exceptions.Bases
{
    public class NotFoundException : HttpResponseException
    {
        public override string Title => "Not found";

        protected NotFoundException(string data)
        {
            Data = data;
        }

        protected NotFoundException(List<string> data)
        {
            Data = string.Join(", ", data);
        }
    }
}
