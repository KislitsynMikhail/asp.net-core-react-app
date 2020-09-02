using QMPT.Data.Models.Organizations;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses.Organizations
{
    public class OrganizationResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Name { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OGRN { get; set; }
        public string OKPO { get; set; }
        public string LegalAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SettlementAccount { get; set; }
        public string CorporateAccount { get; set; }
        public string BIK { get; set; }
        public string ManagerPosition { get; set; }
        public string Base { get; set; }
        public string SupervisorFIO { get; set; }
        public string ChiefAccountant { get; set; }
        public bool IsUSN { get; set; }
        public string OrganizationType { get; set; }

        #region EditableResponse
        public int? EditorId { get;set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        #endregion EditableResponse
        #region RemovableResponse
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        #endregion RemovableResponse

        public OrganizationResponse(Organization organization) : base(organization)
        {
            OrganizationType = organization is Customer ? nameof(Customer) : nameof(Provider);

            Name = organization.Name;
            INN = organization.INN;
            KPP = organization.KPP;
            OGRN = organization.OGRN;
            OKPO = organization.OKPO;
            LegalAddress = organization.LegalAddress;
            Email = organization.Email;
            PhoneNumber = organization.PhoneNumber;
            SettlementAccount = organization.SettlementAccount;
            CorporateAccount = organization.CorporateAccount;
            BIK = organization.BIK;
            ManagerPosition = organization.ManagerPosition;
            Base = organization.Base;
            SupervisorFIO = organization.SupervisorFIO;
            ChiefAccountant = organization.ChiefAccountant;
            IsUSN = organization.IsUSN;

            CreatorId = organization.CreatorId;

            EditableModelResponseFiller.Fill(this, organization);
            RemovableModelResponseFiller.Fill(this, organization);
        }
    }
}
