using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses.Organizations.ContactPersons
{
    public class ContactPersonEmailResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Value { get; set; }
        public int ContactPersonId { get; set; }

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

        public ContactPersonEmailResponse(ContactPersonEmail contactPersonEmail) : base(contactPersonEmail)
        {
            Value = contactPersonEmail.Email;
            ContactPersonId = contactPersonEmail.ContactPersonId;

            EditableModelResponseFiller.Fill(this, contactPersonEmail);
            RemovableModelResponseFiller.Fill(this, contactPersonEmail);
        }
    }
}
