using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{

    public class WireHouseObjects : Repo<WireHouseObject> { }

    public class WireHouseObjectVolumeIsTooBig : ArgumentException
    {
        public WireHouseObjectVolumeIsTooBig(WireHouse wireHouse, WireHouseObject wireHouseObject)
            : base($"Volume of wire house object {wireHouseObject.Name}({wireHouseObject.Volume}) is too big for wire house on the {wireHouse.Address}") { }
    }

    public class WireHouse : Building
    {
        private WireHouseObjects _objects = new();

        public float CapacityByFloor { get; }
        private float _currentCapacity;

        public float CurrentCapacity { get => _currentCapacity; }

        public override string Info => $"Wire house on {Address} with capacity {CapacityByFloor * FloorsNumber}(current capacity: {CurrentCapacity})";

        public WireHouse()
        {
            _currentCapacity = FloorsNumber * CapacityByFloor;
        }

        public override void ChangeFloorsNumber(int delta)
        {
            throw new NotImplementedException();
        }

        public void AddObject(WireHouseObject wireHouseObject)
        {
            if (_currentCapacity - wireHouseObject.Volume < 0.0f)
            {
                throw new WireHouseObjectVolumeIsTooBig(this, wireHouseObject);
            }
            _objects.Add(wireHouseObject);
            _currentCapacity -= wireHouseObject.Volume;
        }

        public IEnumerable<WireHouseObject> FindObjects(Predicate<WireHouseObject> comparator)
        {
            return _objects.Find(comparator);
        }

        public WireHouseObject? FindObjectById(Guid guid)
        {
            return _objects.FindById(guid);
        }

        public bool DeleteObject(WireHouseObject wireHouseObject)
        {
            bool result = _objects.Delete(wireHouseObject);
            if (result)
            {
                _currentCapacity += wireHouseObject.Volume;
            }
            return result;
        }

    }
}
