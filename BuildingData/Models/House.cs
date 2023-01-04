using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class HouseAlreadyBuildedException : Exception {}


    public class House : Building
    {
        public uint ApartmentsNumber { get; set; }

        public override string Info => $"House on the {Address} with {FloorsNumber} floors and {ApartmentsNumber} appartemnts";

        public override void ChangeFloorsNumber(int delta)
        {
            if (ConstructionDate != null)
            {
                throw new HouseAlreadyBuildedException();
            }

            DefaultChangeFloorsNumber(delta);
        }
    }
}
