using System;

namespace PhoneRegistryDDD.Disposal.Exceptions
{
    public class CannotUsePurchasedDeviceException : Exception
    {
        public CannotUsePurchasedDeviceException() : base("Owner cannot use the purchased device.") { }
    }
}

