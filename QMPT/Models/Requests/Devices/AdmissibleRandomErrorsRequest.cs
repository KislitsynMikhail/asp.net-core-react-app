using QMPT.Data.ModelRestrictions.Devices;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Devices
{
    public class AdmissibleRandomErrorsRequest
    {
        //[Required(                                                              ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.admissibleRandomErrorsMaxKeyMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.admissibleRandomErrorsMaxKeyMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Seconds { get; set; }
        //[Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(DeviceRestrictions.admissibleRandomErrorsMaxValueMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(DeviceRestrictions.admissibleRandomErrorsMaxValueMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Value { get; set; }
    }
}
