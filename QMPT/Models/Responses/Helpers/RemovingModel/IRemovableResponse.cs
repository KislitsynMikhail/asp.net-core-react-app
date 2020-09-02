using System;

namespace QMPT.Models.Responses.Helpers.RemovingModel
{
    public interface IRemovableResponse
    {
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
    }
}
