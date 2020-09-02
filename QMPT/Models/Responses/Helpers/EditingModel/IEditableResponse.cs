using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QMPT.Models.Responses.Helpers.EditingModel
{
    public interface IEditableResponse
    {
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
    }
}
