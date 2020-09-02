using QMPT.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests
{
    public class GetParameters
    {
        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
        public int Page { get; set; }
        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
        public int Count { get; set; }

        [MaxLength(100, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Name { get; set; } = string.Empty;
    }
}
