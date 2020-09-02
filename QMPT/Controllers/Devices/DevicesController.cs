using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.Models.Devices;
using QMPT.Data.Services.Devices;
using QMPT.Exceptions.Devices;
using QMPT.Helpers;
using QMPT.Models.Requests;
using QMPT.Models.Requests.Devices;
using QMPT.Models.Responses.Devices;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Controllers.Devices
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DevicesService devicesService;

        public DevicesController(DevicesService devicesService)
        {
            this.devicesService = devicesService;
        }

        [HttpPost]
        public IActionResult PostDevice([FromBody] DeviceRequest deviceRequest)
        {
            var device = new Device(deviceRequest, User.GetId());
            ModelEditingHandler.Insert(devicesService, device);

            return Created($"Devices/{device.Id}", new DeviceResponse(device));
        }

        [HttpGet]
        public IActionResult GetDevices([FromQuery] GetParameters getParameters)
        {
            var devices = devicesService.GetDevices(getParameters);
            var devicesCount = devicesService.GetDevicesCount(getParameters.Name);

            return Ok(new DevicesResponse(devices, devicesCount));
        }

        [HttpGet("{deviceId}")]
        public IActionResult GetDevice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int deviceId)
        {
            var device = devicesService.Get(deviceId);

            if (!device.IsRelevant || device.IsRemoved)
            {
                throw new DevicesNotFoundExeption();
            }

            return Ok(new DeviceResponse(device));
        }

        [HttpPatch("{deviceId}")]
        public IActionResult PatchDevice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int deviceId,
            [FromBody] DeviceRequest deviceRequest)
        {
            var device = devicesService.Get(deviceId);
            ModelEditingHandler.Update(devicesService, deviceRequest, device, User.GetId());

            return Created($"prices/${device.Id}", new DeviceResponse(device));
        }

        [HttpDelete("{deviceId}")]
        public IActionResult DeleteDevice(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)] int deviceId)
        {
            var device = devicesService.Get(deviceId);
         
            ModelRemovingHandler.OnRemove(device, User.GetId());
            
            devicesService.Update(device);

            return NoContent();
        }
    }
}
