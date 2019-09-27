using System;
using MiCasaSegura.Models.Enums;

namespace MiCasaSegura.Models.Core
{
    public class Service
    {
        public int Id { get; set; }
        public ServiceStatus Status { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

    }
}
