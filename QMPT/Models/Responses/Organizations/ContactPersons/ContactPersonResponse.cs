using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QMPT.Models.Responses.Organizations.ContactPersons
{
    public class ContactPersonResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int OrganizationId { get; set; }
        public List<ContactPersonEmailResponse> Emails { get; set; }
        public List<ContactPersonPhoneNumberResponse> PhoneNumbers { get; set; }

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

        public ContactPersonResponse(ContactPerson contactPerson) : base(contactPerson)
        {
            Name = contactPerson.Name;
            Position = contactPerson.Position;
            OrganizationId = contactPerson.OrganizationId;
            Emails = contactPerson.Emails?
                .Select(e => new ContactPersonEmailResponse(e))
                .ToList();
            PhoneNumbers = contactPerson.PhoneNumbers?
                .Select(pn => new ContactPersonPhoneNumberResponse(pn))
                .ToList();

            EditableModelResponseFiller.Fill(this, contactPerson);
            RemovableModelResponseFiller.Fill(this, contactPerson);
        }
    }
}
