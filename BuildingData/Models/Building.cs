using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class Building
    {
        public Guid Id { get; set; }

        private BuildingType type = BuildingType.House;
        private uint floorsNumber = 1;
        private float square = 1.0f;
        private DateOnly constructionDate = DateOnly.FromDateTime(DateTime.Now);

        public BuildingType Type { get => type; set { type = value; } }
        public string? Address { get; set; } = "Street";
        public uint FloorsNumber { get => floorsNumber; private set { floorsNumber = value; } }
        public float Square { get => square; set { square = value; } }
        public DateOnly ConstructionDate { get => constructionDate; set { constructionDate = value; } }

        public string MainInfo { get => $"Building on {Address} has {FloorsNumber} floors and occupies an area of {Square} square meters"; }


        public override string ToString()
        {
            return $"id: {Id.ToString("N")}\ntype: {type}\naddress: {Address}\nfloors number: {floorsNumber}\nsquare: {square}\nconstruction date: {constructionDate}";
        }

        public void CompleteConstruction()
        {
            constructionDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public void ChangeFloorsNumber(long delta)
        {
            if (delta < 0 && Math.Abs(delta) > floorsNumber)
            {
                throw new ArgumentException("Trying to subtract too many floors");
            }

            floorsNumber = (uint)(floorsNumber + delta);
        }
    }
}
