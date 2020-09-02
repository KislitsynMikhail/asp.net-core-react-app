using QMPT.Data.Models;
using QMPT.Exceptions.Bases;

namespace QMPT.Data.Services
{
    public abstract class BaseService
    {
        protected void CheckOnNull(BaseModel baseModel, NotFoundException notFoundException)
        {
            if (baseModel is null)
            {
                throw notFoundException;
            }
        }
    }
}
