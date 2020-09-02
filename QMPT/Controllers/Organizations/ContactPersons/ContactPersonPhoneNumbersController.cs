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

namespace QMPT.Controllers.Organizations.ContactPersons
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPersonPhoneNumbersController : ControllerBase
    {
        private readonly ContactPersonPhoneNumbersService contactPersonPhoneNumbersService;

        public ContactPersonPhoneNumbersController(ContactPersonPhoneNumbersService contactPersonPhoneNumbersService)
        {
            this.contactPersonPhoneNumbersService = contactPersonPhoneNumbersService;
        }

        [HttpPost]
        public IActionResult PostNewContactPersonPhoneNumber([FromBody] ContactPersonPhoneNumberRequest contactPersonPhoneNumberRequest)
        {
            var newContactPersonPhoneNumber = new ContactPersonPhoneNumber(contactPersonPhoneNumberRequest, User.GetId());
            ModelEditingHandler.Insert(contactPersonPhoneNumbersService, newContactPersonPhoneNumber);

            return Created(
                $"api/ContactPersonPhoneNumbers/{newContactPersonPhoneNumber.Id}", 
                new ContactPersonPhoneNumberResponse(newContactPersonPhoneNumber));
        }

        [HttpGet("{contactPersonPhoneNumberId}")]
        public IActionResult GetContactPersonPhoneNumber(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonPhoneNumberId)
        {
            var contactPersonPhoneNumber = contactPersonPhoneNumbersService
                .Get(contactPersonPhoneNumberId);

            return Ok(new ContactPersonPhoneNumberResponse(contactPersonPhoneNumber));
        }

        [HttpPatch("{contactPersonPhoneNumberId}")]
        public IActionResult PatchContactPersonPhoneNumber(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int contactPersonPhoneNumberId,
            [FromBody] ContactPersonPhoneNumberRequest contactPersonPhoneNumberRequest)
        {
            var contactPersonPhoneNumber = contactPersonPhoneNumbersService.Get(contactPersonPhoneNumberId);

            ModelEditingHandler.Update(
                contactPersonPhoneNumbersService, 
                contactPersonPhoneNumberRequest, 
                contactPersonPhoneNumber, 
                User.GetId());

            return Created($"contactPersonPhoneNumbers/{contactPersonPhoneNumber.Id}",
                new ContactPersonPhoneNumberResponse(contactPersonPhoneNumber));
        }

        [HttpDelete("{contactPersonPhoneNumberId}")]
        public IActionResult DeleteContactPersonPhoneNumber(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonPhoneNumberId)
        {
            var contactPersonPhoneNumber = contactPersonPhoneNumbersService
                .Get(contactPersonPhoneNumberId);
            ModelRemovingHandler.OnRemove(contactPersonPhoneNumber, User.GetId());
            contactPersonPhoneNumbersService.Update(contactPersonPhoneNumber);

            return NoContent();
        }
    }
}
