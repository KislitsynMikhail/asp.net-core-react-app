using QMPT.Data.Models.Devices;
using QMPT.Exceptions.Devices;
using QMPT.Models.Requests;
using System.Linq;

namespace QMPT.Data.Services.Devices
{
    public class DevicesService : BaseService, IBaseOperation<Device>
    {
        public void Insert(Device device)
        {
            CheckUsersExistence(device.CreatorId, device.EditorId, device.RemoverId);

            using var db = new DatabaseContext();
            db.Devices.Add(device);
            db.SaveChanges();
        }

        public Device Get(int deviceId)
        {
            using var db = new DatabaseContext();

            var device = db.Devices
                .FirstOrDefault(e => e.Id == deviceId);
            CheckOnNull(device);

            return device;
        }

        public void Update(Device device)
        {
            CheckExistence(device.Id);

            using var db = new DatabaseContext();
            db.Devices.Update(device);
            db.SaveChanges();
        }

        public Device[] GetDevices(GetParameters getParameters)
        {
            var page = getParameters.Page;
            var count = getParameters.Count;
            var number = getParameters.Name.ToLower();

            using var db = new DatabaseContext();

            return db.Devices
                .Where(device => device.Number.ToLower().Contains(number) && device.IsRelevant && !device.IsRemoved)
                .OrderBy(device => device.Number)
                .Skip(page * count)
                .Take(count)
                .ToArray();
        }

        public int GetDevicesCount(string number)
        {
            using var db = new DatabaseContext();

            return db.Devices
                .Where(device => device.Number.ToLower().Contains(number) && device.IsRelevant && !device.IsRemoved)
                .Count();
        }

        public void Delete(Device device)
        {
            CheckExistence(device.Id);

            using var db = new DatabaseContext();
            db.Devices.Update(device);
            db.SaveChanges();
        }

        public bool IsExists(int deviceId)
        {
            using var db = new DatabaseContext();

            return db.Devices
                .Any(e => e.Id == deviceId);
        }

        public void CheckExistence(int deviceId)
        {
            if(!IsExists(deviceId))
            {
                throw new DevicesNotFoundExeption();
            }
        }

        public void Insert(object model)
        {
            var device = model as Device;
            CheckOnNull(device);

            Insert(device);
        }

        private void CheckUsersExistence(int? creatorId, int? editorId = null, int? removedId = null)
        {
            var usersService = new UsersService();

            if (creatorId != null)
                usersService.CheckExistence((int)creatorId);
            if (editorId != null && editorId != creatorId)
                usersService.CheckExistence((int)editorId);
            if (removedId != null && removedId != creatorId)
                usersService.CheckExistence((int)removedId);
        }

        private void CheckOnNull(Device device)
        {
            CheckOnNull(device, new DevicesNotFoundExeption());
        }
    }
}
