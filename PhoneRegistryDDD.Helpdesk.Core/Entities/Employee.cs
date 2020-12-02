using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities
{
    public class Employee
    {
        public Guid Id { get; private set; }

        private readonly List<SimCard> _simCards = new();
        public IReadOnlyCollection<SimCard> SimCards => _simCards.AsReadOnly();
        private SimCard _freeSimCard => _simCards.Find(sim => sim.IsFree());


        [Obsolete("For EF", true)]
        public Employee() { }

        public Employee(Guid id, List<SimCard> simCards)
        {
            Id = id;
            _simCards = simCards;
        }

        public static Employee New(Guid id) => new Employee(id, new List<SimCard>());
        public static Employee With(Guid id, List<SimCard> simCards) => new Employee(id, simCards);

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

        public ReturnedDevice Return(Device device)
        {
            var simCard = GetSimCardWith(device);
            bool doesntExists = simCard == null;

            if (doesntExists)
                return null;

            _simCards.Remove(simCard);
            var simCardSnapshot = simCard.ToSnapshot();

            return new ReturnedDevice(Id, simCardSnapshot.DeviceId, simCardSnapshot.Id);
        }

        private SimCard GetSimCardWith(Device device)
        {
            return _simCards.Find(sim => sim.Has(device));
        }
    }
}
