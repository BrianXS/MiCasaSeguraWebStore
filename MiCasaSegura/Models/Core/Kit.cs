using System;
using System.Collections.Generic;

namespace MiCasaSegura.Models.Core
{
    public class Kit
    {
        public int Id { get; set; }
        public List<SerialNumber> Serials { get; set; }

        public int KitTypeId { get; set; }
        public KitType KitType { get; set; }
    }
}