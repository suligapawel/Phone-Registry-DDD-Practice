using System;

namespace PhoneRegistryDDD.Availability.Application.Events
{
    public class AssortmentUnblocked
    {
        private readonly Guid[] _ids;

        public AssortmentUnblocked(Guid[] ids)
        {
            _ids = ids;
        }
    }
}