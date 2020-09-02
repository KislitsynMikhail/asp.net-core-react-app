using QMPT.Data.Models;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses.Prices
{
    public class PriceResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }

        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public PriceResponse(Price price) : base(price)
        {
            Name = price.Name;
            Cost = price.Cost;
            Type = price.Type.ToString();

            EditableModelResponseFiller.Fill(this, price);
            RemovableModelResponseFiller.Fill(this, price);
        }
    }
}
