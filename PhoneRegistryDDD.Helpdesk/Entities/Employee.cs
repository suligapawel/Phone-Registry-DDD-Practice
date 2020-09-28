using PhoneRegistryDDD.Helpdesk.Entities.Devices;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.Helpdesk.Entities
{
    public class Employee
    {
        private readonly ICollection<SimCard> _simCards;
        private SimCard _freeSimCard => _simCards.FirstOrDefault(sim => sim.IsFree());

        public Employee(ICollection<SimCard> simCards)
        {
            _simCards = simCards;
        }

        public static Employee New() => new Employee(new List<SimCard>());
        public static Employee With(ICollection<SimCard> simCards) => new Employee(simCards);

        public bool TakeNew(SimCard simCard)
        {
            if (HasFreeSimCard())
                return false;

            _simCards.Add(simCard);
            return true;
        }

        public bool HasFreeSimCard() => _freeSimCard != null;

        public bool TakeNew(Device device)
        {
            if (!HasFreeSimCard() || HasDeviceOfSameTypeAs(device))
                return false;

            _freeSimCard.SetDevice(device);
            return true;
        }

        private bool HasDeviceOfSameTypeAs(Device device)
        {
            return _simCards.Any(sim => sim.HasDeviceOfSameTypeAs(device));
        }
    }
}
