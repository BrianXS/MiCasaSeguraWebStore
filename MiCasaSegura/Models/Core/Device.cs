using System;
namespace MiCasaSegura.Models.Core
{
    public class Device
    {
        public int Id { get; set; }
        public string Serial { get; set; }

        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}