using System;

namespace PhoneRegistryDDD.Disposal.Core.Exceptions
{
    public class CannotUsePurchasedDeviceException : Exception
    {
        public CannotUsePurchasedDeviceException() : base("Owner cannot use the purchased device.") { }
    }
}

