using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Data.Services;
using QMPT.Helpers;
using QMPT.Models.Requests.Organizations.ContactPersons;
using QMPT.Models.Responses.Organizations.ContactPersons;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QMPT.Controllers.Organizations.ContactPersons
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPersonsController : ControllerBase
    {
        private readonly ContactPersonsService contactPersonsService;
        private readonly ContactPersonEmailsService contactPersonEmailsService;
        private readonly ContactPersonPhoneNumbersService contactPersonPhoneNumbersService;

        public ContactPersonsController(ContactPersonsService contactPersonsService, 
            ContactPersonEmailsService contactPersonEmailsService,
            ContactPersonPhoneNumbersService contactPersonPhoneNumbersService)
        {
            this.contactPersonsService = contactPersonsService;
            this.contactPersonEmailsService = contactPersonEmailsService;
            this.contactPersonPhoneNumbersService = contactPersonPhoneNumbersService;
        }

        [HttpPost]
        public IActionResult PostNewContactPerson([FromBody] ContactPersonRequest contactPersonRequest)
        {
            var newContactPerson = new ContactPerson(contactPersonRequest, User.GetId());
            ModelEditingHandler.Insert(contactPersonsService, newContactPerson);

            return Created(
                $"api/contactPersons/{newContactPerson.Id}", 
                new ContactPersonResponse(newContactPerson));
        }

        [HttpGet("{organizationId}")]
        public IActionResult GetContactPeople(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int organizationId)
        {
            var contactPeople = contactPersonsService.GetContactPeople(organizationId);

            var response = contactPeople
                .Select(contactPerson => new ContactPersonResponse(contactPerson))
                .ToArray();
            return Ok(response);
        }

        [HttpGet("items/{contactPersonId}")]
        public IActionResult GetContactItems(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonId)
        {
            var emails = contactPersonEmailsService.GetMany(contactPersonId);
            var phoneNumbers = contactPersonPhoneNumbersService.GetMany(contactPersonId);

            return Ok(new ContactItemsResponse(emails, phoneNumbers));
        }

        [HttpGet("items")]
        public IActionResult GetContactItems([FromQuery] ContactPeopleItemsGetParameters contactPeopleItemsGetParameters)
        {
            var emails = contactPersonEmailsService.GetMany(contactPeopleItemsGetParameters.ContactPeopleId);
            var phoneNumbers = contactPersonPhoneNumbersService.GetMany(contactPeopleItemsGetParameters.ContactPeopleId);

            return Ok(new ContactItemsResponse(emails, phoneNumbers));
        }

        [HttpPatch("{contactPersonId}")]
        public IActionResult PatchContactPerson(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int contactPersonId,
            [FromBody] ContactPersonRequest contactPersonRequest)
        {
            var contactPerson = contactPersonsService.Get(contactPersonId);

            ModelEditingHandler.Update(contactPersonsService, contactPersonRequest, contactPerson, User.GetId());

            return Created($"contactPersons/{contactPerson.Id}", new ContactPersonResponse(contactPerson));
        }

        /*[HttpGet("{contactPersonId}")]
        public IActionResult GetContactPerson(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonId)
        {
            var contactPerson = contactPersonsService.Get(contactPersonId);
            contactPerson.Emails = contactPersonEmailsService.GetMany(contactPersonId);
            contactPerson.PhoneNumbers = contactPersonPhoneNumbersService.GetMany(contactPersonId);

            return Ok(new ContactPersonResponse(contactPerson));
        }*/

        [HttpDelete("{contactPersonId}")]
        public IActionResult DeleteContactPerson(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonId)
        {
            var contactPerson = contactPersonsService.Get(contactPersonId);
            ModelRemovingHandler.OnRemove(contactPerson, User.GetId());
            contactPersonsService.Update(contactPerson);

            return NoContent();
        }
    }
}
