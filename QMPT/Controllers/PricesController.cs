using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.Models;
using QMPT.Data.Services;
using QMPT.Exceptions.Prices;
using QMPT.Helpers;
using QMPT.Models.Requests.Prices;
using QMPT.Models.Responses.Prices;

namespace QMPT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private PricesService pricesService;

        public PricesController(PricesService pricesService)
        {
            this.pricesService = pricesService;
        }

        [HttpPost]
        public IActionResult PostPrice([FromBody] PriceRequest priceRequest)
        {
            var price = new Price(priceRequest, User.GetId());

            ModelEditingHandler.Insert(pricesService, price);

            return Created($"api/prices/${price.Id}", new PriceResponse(price));
        }

        [HttpGet("{priceId}")]
        public IActionResult GetPrice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int priceId)
        {
            var price = pricesService.Get(priceId);
            if (!price.IsRelevant || price.IsRemoved)
            {
                throw new PriceNotFoundException();
            }

            return Ok(new PriceResponse(price));
        }

        [HttpGet]
        public IActionResult GetPrices([FromQuery] PricesGetParameters pricesGetParameters)
        {
            var prices = pricesService.Get(pricesGetParameters);
            var pricesCount = pricesService.GetAllCount(pricesGetParameters);

            return Ok(new PricesResponse(prices, pricesCount));
        }

        [HttpPatch("{priceId}")]
        public IActionResult PatchPrice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int priceId, 
            [FromBody] PriceRequest priceRequest)
        {
            var price = pricesService.Get(priceId);

            ModelEditingHandler.Update(pricesService, priceRequest, price, User.GetId());

            return Created($"prices/${price.Id}", new PriceResponse(price));
        }

        [HttpDelete("{priceId}")]
        public IActionResult DeletePrice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int priceId)
        {
            var price = pricesService.Get(priceId);

            ModelRemovingHandler.OnRemove(price, User.GetId());

            pricesService.Update(price);

            return NoContent();
        }
    }
}
