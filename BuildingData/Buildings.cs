using BuildingData.Models;

namespace BuildingData
{
    public class BuildingsRepo
    {

        public List<Building> Buildings { get; } = new List<Building>();

        public int Count { get => Buildings.Count; private set { } }

        private uint maxBuildings = 0;

        public uint MaxBuildings
        {
            get => maxBuildings;

            set
            {
                if (value < Buildings.Count)
                {
                    throw new ArgumentException("The maximum number of buildings cannot be less than the existing one");
                }
                maxBuildings = value;
            }
        }

        public void Add(Building building)
        {
            building.Id = Guid.NewGuid();
            Buildings.Add(building);
        }

        //public delegate bool Comparator(Building building);

        public List<Building> Find(Predicate<Building> comparator)
        {
            List<Building> result = new();
            foreach (Building building in Buildings)
            {
                if (comparator(building))
                {
                    result.Add(building);
                }
            }
            return result;
        }

        public Building? FindById(Guid id)
        {

            foreach (var building in Buildings)
            {
                if (building.Id.Equals(id)) {
                    return building;
                }
            }

            return null;

        }

        public bool Delete(Guid id)
        {
            Building building = FindById(id);
            if (building == null) return false;
            return Buildings.Remove(building);
        }

    }
}