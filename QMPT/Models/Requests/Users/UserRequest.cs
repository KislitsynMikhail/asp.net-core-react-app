using QMPT.Data.ModelRestrictions;
using QMPT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Users
{
    public class UserRequest
    {
        [Required(                                  ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.loginMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.loginMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Login { get; set; }

        [Required(                                      ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.firstNameMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.firstNameMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string FirstName { get; set; }

        [Required(                                      ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.lastNameMaxLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.lastNameMinLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        public string LastName { get; set; }

        [Required(                                      ErrorMessage = ErrorMessages.missingParameters)]
        [MaxLength(UserRestrictions.passwordMaxLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        [MinLength(UserRestrictions.passwordMinLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        public string Password { get; set; }
    }
}
