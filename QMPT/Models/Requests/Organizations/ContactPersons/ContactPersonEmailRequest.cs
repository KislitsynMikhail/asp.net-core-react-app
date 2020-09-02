using QMPT.Data.ModelRestrictions.Organizations.ContactPersons;
using QMPT.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Organizations.ContactPersons
{
    public class ContactPersonEmailRequest
    {
        //[EmailAddress(                                              ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(ContactPersonEmailRestrictions.emailMinLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(ContactPersonEmailRestrictions.emailMaxLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        public string Value { get; set; }

        [Required(                                  ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 1, maximum: int.MaxValue,   ErrorMessage = ErrorMessages.invalidParameters)]
        public int ContactPersonId { get; set; }
    }
}
