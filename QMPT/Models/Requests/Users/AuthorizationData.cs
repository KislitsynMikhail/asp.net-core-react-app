using QMPT.Data.ModelRestrictions;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Users
{
    public class AuthorizationData
    {
        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.loginMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.loginMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Login { get; set; }

        [Required(ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.passwordMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.passwordMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Password { get; set; }
    }
}
