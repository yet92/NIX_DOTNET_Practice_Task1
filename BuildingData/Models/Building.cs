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

        public BuildingType type = BuildingType.House;
        public string? address = "Street";
        public uint floorsNumber = 1;
        public float square = 1.0f;
        public DateOnly constructionDate = DateOnly.FromDateTime(DateTime.Now);

        public override string ToString()
        {
            return $"id: {Id.ToString("N")}\ntype: {type}\naddress: {address}\nfloors number: {floorsNumber}\nsquare: {square}\nconstruction date: {constructionDate}";
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

            floorsNumber = (uint)(floorsNumber - delta);
        }
    }
}
