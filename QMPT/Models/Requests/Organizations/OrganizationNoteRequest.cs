using QMPT.Data.ModelRestrictions.Organizations;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Organizations
{
    public class OrganizationNoteRequest
    {
        [Required(                                              ErrorMessage = ErrorMessages.missingParameters)]
        [MinLength(OrganizationNoteRestrictions.noteMinLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(OrganizationNoteRestrictions.noteMaxLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        public string Note { get; set; }

        [Required(                                  ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 1, maximum: int.MaxValue,   ErrorMessage = ErrorMessages.invalidParameters)]
        public int OrganizationId { get; set; }
    }
}
