using System;

namespace PhoneRegistryDDD.Utilization.Exceptions
{
    public class CannotUsePurchasedDeviceException : Exception
    {
        public CannotUsePurchasedDeviceException() : base("Owner cannot use the purchased device.") { }
    }
}

