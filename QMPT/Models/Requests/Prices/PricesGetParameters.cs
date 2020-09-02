using QMPT.Data.ModelRestrictions;
using QMPT.Data.Models;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Prices
{
    public class PricesGetParameters
    {
        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        public Price.PriceType Type { get; set; }

        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        public int Page { get; set; }

        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        public int Count { get; set; }

        [MaxLength(PriceRestrictions.nameMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Name { get; set; } = string.Empty;
    }
}
