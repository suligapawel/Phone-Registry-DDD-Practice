using System;

namespace PhoneRegistryDDD.Warehouse.Core.Entities;

public class SimCard
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Pin { get; set; }
    public string Puk { get; set; }
}