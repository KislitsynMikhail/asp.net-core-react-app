using QMPT.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.ModelHelpers.EditingModel
{
    public interface IEditable<T> : IEditable where T : BaseModel
    {
        public T Original { get; set; }
    }

    public interface IEditable
    {
        public int? OriginalId { get; set; }
        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        public User Editor { get; set; }
        public bool IsRelevant { get; set; }

        public string CurrentVersionKey { get; }
        public void CreateNewVersionValue();
        public void ChangeData(object newData, int editorId, int newVersion);
    }
}
