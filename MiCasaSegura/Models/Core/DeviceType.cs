using System;
using System.Collections.Generic;
using MiCasaSegura.Models.Enums;

namespace MiCasaSegura.Models.Core
{
    public class DeviceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }

        public List<Device> Devices { get; set; }
    }
}