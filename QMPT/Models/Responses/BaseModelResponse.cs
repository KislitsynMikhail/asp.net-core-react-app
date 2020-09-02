using QMPT.Data.Models;
using System;

namespace QMPT.Models.Responses
{
    public abstract class BaseModelResponse
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int? CreatorId { get; set; }

        protected BaseModelResponse(BaseModel baseModel)
        {
            Id = baseModel.Id;
            CreationTime = baseModel.CreationTime;
            CreatorId = baseModel.CreatorId;
        }
    }
}
