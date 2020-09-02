using QMPT.Data.ModelHelpers.EditingModel;
using System;

namespace QMPT.Data.Services
{
    public interface IBaseOperation<T> where T : IEditable, ICloneable
    {
        public void Update(T model);
        public T Get(int id);
        void Insert(object model);
    }
}
