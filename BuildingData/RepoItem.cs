using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData
{
    public abstract class RepoItem
    {
        public Guid Id { get; set; }

        public abstract string Info { get; }

    }
}
