using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Data.Models.Organizations;
using QMPT.Data.Services.Organizations;
using QMPT.Helpers;
using QMPT.Models.Requests.Organizations;
using QMPT.Models.Responses.Organizations;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Controllers.Organizations
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationNotesController : ControllerBase
    {
        private readonly OrganizationNotesService organizationNotesService;

        public OrganizationNotesController(OrganizationNotesService organizationNotesService)
        {
            this.organizationNotesService = organizationNotesService;
        }

        [HttpPost]
        public IActionResult PostNewOrganizationNote([FromBody] OrganizationNoteRequest organizationNoteRequest)
        {
            var newOrganizationNote = new OrganizationNote(organizationNoteRequest);
            organizationNotesService.Insert(newOrganizationNote);

            return Created(
                $"api/OrganizationNotes/{newOrganizationNote.Id}", 
                new OrganizationNoteResponse(newOrganizationNote));
        }

        [HttpGet("{organizationNoteId}")]
        public IActionResult GetCustomerNote(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int organizationNoteId)
        {
            var organizationNote = organizationNotesService.Get(organizationNoteId);

            return Ok(new OrganizationNoteResponse(organizationNote));
        }

        [HttpDelete("{organizationNoteId}")]
        public IActionResult DeleteOrganizationNote(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int organizationNoteId)
        {
            var organizationNote = organizationNotesService.Get(organizationNoteId);
            organizationNotesService.Delete(organizationNote);

            return NoContent();
        }
    }
}
