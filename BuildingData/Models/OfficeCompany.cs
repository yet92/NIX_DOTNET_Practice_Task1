using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class OfficeCompany : RepoItem
    {
        public string Name { get; set; }

        public override string Info => $"Office of {Name} company";
    }
}
