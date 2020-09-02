using QMPT.Data.ModelRestrictions.Organizations.ContactPersons;
using QMPT.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Models.Requests.Organizations.ContactPersons
{
    public class ContactPersonRequest
    {
        //[MinLength(ContactPersonRestrictions.fioMinLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(ContactPersonRestrictions.fioMaxLength,  ErrorMessage = ErrorMessages.invalidParameters)]
        public string Name { get; set; }
        //[MinLength(ContactPersonRestrictions.positionMinLength, ErrorMessage = ErrorMessages.invalidParameters)]
        [MaxLength(ContactPersonRestrictions.positionMaxLength, ErrorMessage = ErrorMessages.invalidParameters)]
        public string Position { get; set; }
        [Required(                                  ErrorMessage = ErrorMessages.missingParameters)]
        [Range(minimum: 1, maximum: int.MaxValue,   ErrorMessage = ErrorMessages.invalidParameters)]
        public int OrganizationId { get; set; }
    }
}
