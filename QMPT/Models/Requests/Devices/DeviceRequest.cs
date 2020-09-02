using QMPT.Data.ModelRestrictions.Devices;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Devices
{
    public class DeviceRequest
    {
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.numberMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.numberMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Number { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.measurementRangeMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.measurementRangeMInLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string MeasurementRange { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.permissibleSystematicErrorMaxMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.permissibleSystematicErrorMaxMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string PermissibleSystematicErrorMax { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        public AdmissibleRandomErrorsRequest[] AdmissibleRandomErrorsMax { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.magnetometerReadingsVariationMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.magnetometerReadingsVariationMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string MagnetometerReadingsVariation { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.gradientResistenceMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.gradientResistenceMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string GradientResistance { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.signalAmplitudeMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.signalAmplitudeMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string SignalAmplitude { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.relaxtionTimeMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.relaxtionTimeMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string RelaxationTime { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.optimalCycleMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.optimalCycleMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string OptimalCycle { get; set; }
    }
}
