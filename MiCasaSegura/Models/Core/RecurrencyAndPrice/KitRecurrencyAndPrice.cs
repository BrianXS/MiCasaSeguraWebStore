using System;
using MiCasaSegura.Models.Enums;

namespace MiCasaSegura.Models.Core.RecurrencyAndPrice
{
    public class KitRecurrencyAndPrice
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public PaymentDuration Recurrency { get; set; }

        public int KitTypeId { get; set; }
        public KitType KitType { get; set; }
    }
}
