using System;
using MiCasaSegura.Models.Enums;

namespace MiCasaSegura.Models.Core.RecurrencyAndPrice
{
    public class ServiceRecurrencyAndPrice
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public PaymentDuration Recurrency { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
