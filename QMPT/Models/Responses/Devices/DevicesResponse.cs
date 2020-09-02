using QMPT.Data.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QMPT.Models.Responses.Devices
{
    public class DevicesResponse
    {
        public List<DeviceResponse> Devices { get; set; }
        public int Count { get; set; }

        public DevicesResponse(Device[] devices, int count)
        {
            Devices = devices
                .Select(device => new DeviceResponse(device))
                .ToList();
            Count = count;
        }
    }
}
