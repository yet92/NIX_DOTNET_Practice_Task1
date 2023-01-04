using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class WireHouseObject : RepoItem
    {
        public string Name { get; set; }
        public float Volume { get; }

        public override string Info => $"WireHouseObject: {Name} with volume: {Volume}";
    }
}
