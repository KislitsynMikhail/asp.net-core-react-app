using QMPT.Data.Models.Devices;
using QMPT.Models.Responses.Helpers.EditingModel;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;
using System.Collections.Generic;

namespace QMPT.Models.Responses.Devices
{
    public class DeviceResponse : BaseModelResponse, IEditableResponse, IRemovableResponse
    {
        public string Number { get; set; }
        public string MeasurementRange { get; set; }
        public string PermissibleSystematicErrorMax { get; set; }
        public string AdmissibleRandomErrorsMaxJson { get; set; }
        public List<AdmissibleRandomErrorMax> AdmissibleRandomErrorsMax { get; set; }
        public string MagnetometerReadingsVariation { get; set; }
        public string GradientResistance { get; set; }
        public string SignalAmplitude { get; set; }
        public string RelaxationTime { get; set; }
        public string OptimalCycle { get; set; }
        public int CreatorId { get; set; }

        #region Editable
        public int? OriginalId { get; set; }
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        #endregion Editable

        #region Removable
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        #endregion Removable

        public DeviceResponse(Device device) : base(device)
        {
            EditableModelResponseFiller.Fill(this, device);
            RemovableModelResponseFiller.Fill(this, device);

            Number = device.Number;
            MeasurementRange = device.MeasurementRange;
            PermissibleSystematicErrorMax = device.PermissibleSystematicErrorMax;
            AdmissibleRandomErrorsMax = device.AdmissibleRandomErrorsMax is null 
                ? new List<AdmissibleRandomErrorMax>() 
                : device.AdmissibleRandomErrorsMax;
            MagnetometerReadingsVariation = device.MagnetometerReadingsVariation;
            GradientResistance = device.GradientResistance;
            SignalAmplitude = device.SignalAmplitude;
            RelaxationTime = device.RelaxationTime;
            OptimalCycle = device.OptimalCycle;
        }
    }
}
