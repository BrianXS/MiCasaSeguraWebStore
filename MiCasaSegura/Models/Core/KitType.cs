using System.Collections.Generic;
using MiCasaSegura.Models.Core.RecurrencyAndPrice;

namespace MiCasaSegura.Models.Core
{
    public class KitType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public List<Kit> Kits { get; set; }
        public List<KitRecurrencyAndPrice> RecurrencyAndPrices { get; set; }
    }
}