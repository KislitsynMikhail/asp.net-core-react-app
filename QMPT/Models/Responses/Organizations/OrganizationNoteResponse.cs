using QMPT.Data.Models.Organizations;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses.Organizations
{
    public class OrganizationNoteResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Note { get; set; }
        public int OrganizationId { get; set; }

        #region EditableResponse
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        #endregion EditableResponse
        #region RemovableResponse
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        #endregion RemovableResponse

        public OrganizationNoteResponse(OrganizationNote organizationNote) : base(organizationNote)
        {
            Note = organizationNote.Note;
            OrganizationId = organizationNote.OrganizationId;

            EditableModelResponseFiller.Fill(this, organizationNote);
            RemovableModelResponseFiller.Fill(this, organizationNote);
        }
    }
}
