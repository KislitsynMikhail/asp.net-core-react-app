using QMPT.Data.ModelRestrictions.Organizations.ContactPersons;
using QMPT.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Organizations.ContactPersons
{
    public class ContactPersonPhoneNumberRequest
    {
        //[RegularExpression("+?\\d+",                                            ErrorMessage = ErrorMessages.invalidParameters)]
        //[MinLength(ContactPersonPhoneNumberRestrictions.phoneNumberMinLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(ContactPersonPhoneNumberRestrictions.phoneNumberMaxLength,   ErrorMessage = ErrorMessages.invalidParameters)]
        public string Value { get; set; }
        [Required(                                  ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 1, maximum: int.MaxValue,   ErrorMessage = ErrorMessages.invalidParameters)]
        public int ContactPersonId { get; set; }
    }
}
