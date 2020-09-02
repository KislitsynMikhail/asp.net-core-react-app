using QMPT.Data.ModelRestrictions;
using QMPT.Data.Models;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Prices
{
    public class PriceRequest
    {
        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        public Price.PriceType Type { get; set; }

        [MaxLength(PriceRestrictions.nameMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.invalidParameters)]
        public decimal Cost { get; set; }
    }
}
