using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MiCasaSegura.Models.Core.RecurrencyAndPrice;
using MiCasaSegura.Models.Enums;

namespace MiCasaSegura.Models.Core
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public List<Service> Services { get; set; }
        public List<ServiceRecurrencyAndPrice> RecurrencyAndPrices { get; set; }
    }
}
