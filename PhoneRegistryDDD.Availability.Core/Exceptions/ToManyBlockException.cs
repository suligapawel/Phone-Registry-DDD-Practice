using System;

namespace PhoneRegistryDDD.Availability.Core.Exceptions
{
    public class ToManyBlockException : Exception
    {
        public ToManyBlockException() : base("The blocks array can have a maximum of two blocks.") { }
    }
}
