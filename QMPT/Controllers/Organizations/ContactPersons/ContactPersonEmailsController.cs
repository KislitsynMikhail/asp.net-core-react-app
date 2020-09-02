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
    public class ContactPersonEmailsController : ControllerBase
    {
        private readonly ContactPersonEmailsService contactPersonEmailsService;

        public ContactPersonEmailsController(ContactPersonEmailsService contactPersonEmailsService)
        {
            this.contactPersonEmailsService = contactPersonEmailsService;
        }

        [HttpPost]
        public IActionResult PostNewContactPersonEmail([FromBody] ContactPersonEmailRequest contactPersonEmailRequest)
        {
            var newContactPersonEmail = new ContactPersonEmail(contactPersonEmailRequest, User.GetId());
            ModelEditingHandler.Insert(contactPersonEmailsService, newContactPersonEmail);

            return Created(
                $"api/ContactPersonEmails/{newContactPersonEmail.Id}", 
                new ContactPersonEmailResponse(newContactPersonEmail));
        }

        [HttpGet("{contactPersonEmailId}")]
        public IActionResult GetContactPersonEmail(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonEmailId)
        {
            var contactPersonEmail = contactPersonEmailsService.Get(contactPersonEmailId);

            return Ok(new ContactPersonEmailResponse(contactPersonEmail));
        }

        [HttpPatch("{contactPersonEmailId}")]
        public IActionResult PatchContactPerson(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int contactPersonEmailId,
            [FromBody] ContactPersonEmailRequest contactPersonEmailRequest)
        {
            var contactPersonEmail = contactPersonEmailsService.Get(contactPersonEmailId);

            ModelEditingHandler.Update(contactPersonEmailsService, contactPersonEmailRequest, contactPersonEmail, User.GetId());
        
            return Created($"contactPersons/{contactPersonEmail.Id}", 
                new ContactPersonEmailResponse(contactPersonEmail));
        }

        [HttpDelete("{contactPersonEmailId}")]
        public IActionResult DeleteContactPersonEmail(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int contactPersonEmailId)
        {
            var contactPersonEmail = contactPersonEmailsService.Get(contactPersonEmailId);
            ModelRemovingHandler.OnRemove(contactPersonEmail, User.GetId());
            contactPersonEmailsService.Update(contactPersonEmail);

            return NoContent();
        }
    }
}
