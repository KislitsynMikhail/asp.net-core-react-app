using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.Models.Organizations;
using QMPT.Data.Services;
using QMPT.Data.Services.Organizations;
using QMPT.Helpers;
using QMPT.Models.Requests;
using QMPT.Models.Requests.Organizations;
using QMPT.Models.Responses.Organizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Controllers.Organizations
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationsService organizationsService;
        private const string controllerPath = "api/Organizations";
        private const string customersPath = "Customers";
        private const string providersPath = "Providers";
        private const string customerIdStrPath = "customerId";
        private const string providerIdStrPath = "providerId";

        public OrganizationsController(OrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        [HttpPost(customersPath)]
        public IActionResult PostNewCustomer([FromBody] OrganizationRequest organizationRequest)
        {
            var userId = User.GetId();
            var newCustomer = new Customer(organizationRequest, userId);

            ModelEditingHandler.Insert(new CustomersService(), newCustomer);

            return Created(
                $"{controllerPath}{customersPath}/{newCustomer.Id}", 
                new OrganizationResponse(newCustomer));
        }

        [HttpPost(providersPath)]
        public IActionResult PostNewProvider([FromBody] OrganizationRequest organizationRequest)
        {
            var userId = User.GetId();
            var newProvider = new Provider(organizationRequest, userId);

            ModelEditingHandler.Insert(new ProvidersService(), newProvider);

            return Created(
                $"{controllerPath}{providersPath}/{newProvider.Id}", 
                new OrganizationResponse(newProvider));
        }

        [HttpGet(customersPath)]
        public IActionResult GetCustomers([FromQuery] GetParameters getParameters)
        {
            return Ok(GetOrganizationsResponse(new CustomersService(), getParameters));
        }

        [HttpGet(providersPath)]
        public IActionResult GetProviders([FromQuery] GetParameters getParameters)
        {
            return Ok(GetOrganizationsResponse(new ProvidersService(), getParameters));
        }

        [HttpGet(customersPath + "/{" + customerIdStrPath + "}")]
        public IActionResult GetCustomer(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int customerId)
        {
            return Ok(GetOrganizationResponse(new CustomersService(), customerId));
        }

        [HttpGet(providersPath + "/{" + providerIdStrPath + "}")]
        public IActionResult GetProvider(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int providerId)
        {
            return Ok(GetOrganizationResponse(new ProvidersService(), providerId));
        }

        [HttpPatch(customersPath + "/{" + customerIdStrPath + "}")]
        public IActionResult PatchCustomer(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int customerId, 
            [FromBody] OrganizationRequest organizationRequest)
        {
            var customersService = new CustomersService();
            var customer = GetUpdatedOrganization(customersService, customerId, organizationRequest);

            return Created(
                $"{controllerPath}{customersPath}/{customer.Id}", 
                new OrganizationResponse(customer));
        }

        [HttpPatch(providersPath + "/{" + providerIdStrPath + "}")]
        public IActionResult PatchProvider(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int providerId,
            [FromBody] OrganizationRequest organizationRequest)
        {
            var providersService = new ProvidersService();
            var provider = GetUpdatedOrganization(providersService, providerId, organizationRequest);

            return Created(
                $"{controllerPath}{provider}/{provider.Id}", 
                new OrganizationResponse(provider));
        }

        [HttpDelete(customersPath + "/{" + customerIdStrPath + "}")]
        public IActionResult DeleteCustomer(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] 
            int customerId)
        {
            var customersService = new CustomersService();
            var customer = customersService.Get(customerId);
            ModelRemovingHandler.OnRemove(customer, User.GetId());
            customersService.Update(customer);

            return NoContent();
        }

        [HttpDelete(providersPath + "/{" + providerIdStrPath + "}")]
        public IActionResult DeleteProvider(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int providerId)
        {
            var providersService = new ProvidersService();
            var provider = providersService.Get(providerId);
            ModelRemovingHandler.OnRemove(provider, User.GetId());
            providersService.Update(provider);

            return NoContent();
        }

        private OrganizationsResponse GetOrganizationsResponse(IOrganizationsService organizationsService, GetParameters getParameters)
        {
            var organizations = organizationsService.GetOrganizations(getParameters);
            var organizationsCount = organizationsService.GetOrganizationsCount(getParameters.Name);

            return new OrganizationsResponse(organizations, organizationsCount);
        }

        private OrganizationResponse GetOrganizationResponse(IOrganizationsService organizationsService, int organizationId)
        {
            return new OrganizationResponse(organizationsService.Get(organizationId));
        }

        private Organization GetUpdatedOrganization(IOrganizationsService organizationsService, int organizationId, OrganizationRequest organizationRequest)
        {
            var organization = organizationsService.Get(organizationId);

            ModelEditingHandler.Update(organizationsService, organizationRequest, organization, User.GetId());

            return organization;
        }
    }
}
