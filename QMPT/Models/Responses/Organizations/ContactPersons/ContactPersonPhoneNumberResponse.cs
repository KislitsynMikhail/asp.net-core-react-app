using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses.Organizations.ContactPersons
{
    public class ContactPersonPhoneNumberResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
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

        public ContactPersonPhoneNumberResponse(ContactPersonPhoneNumber contactPersonPhoneNumber) : base(contactPersonPhoneNumber)
        {
            Value = contactPersonPhoneNumber.PhoneNumber;
            ContactPersonId = contactPersonPhoneNumber.ContactPersonId;

            EditableModelResponseFiller.Fill(this, contactPersonPhoneNumber);
            RemovableModelResponseFiller.Fill(this, contactPersonPhoneNumber);
        }
    }
}
