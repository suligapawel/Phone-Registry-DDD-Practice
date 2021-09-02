using System;

namespace PhoneRegistryDDD.Warehouse.Core.Entities
{
    public class Smartphone
    {
        public Guid Id { get; set; }
        public string Imei { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}