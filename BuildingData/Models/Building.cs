using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class WrongStartConstructionDateValue : ArgumentException
    {
        public WrongStartConstructionDateValue() : base("Start construction date must be less then construction date")
        {

        }
    }

    public abstract class BuildingAbstract : RepoItem
    {
        public string Address { get; }
        public float Square { get; set; }
        public uint FloorsNumber { get; private set; }
        public DateOnly? ConstructionDate { get; private set; } = null;
        public DateOnly StartConstructionDate
        {
            get => StartConstructionDate; set
            {
                if (ConstructionDate != null)
                {
                    DateOnly newValue = value;
                    if (newValue.CompareTo(ConstructionDate) < 0)
                    {
                        StartConstructionDate = value;
                    } else
                    {
                        throw new 
                    }
                }
                else StartConstructionDate = value;

            }
        }

        public void CompleteConstruction()
        {
            ConstructionDate = DateOnly.FromDateTime(DateTime.Now);
        }

        protected void DefaultChangeFloorsNumber(int delta)
        {
            if (delta < 0 && Math.Abs(delta) > FloorsNumber)
            {
                throw new ArgumentException("Trying to subtract too many floors");
            }

            FloorsNumber = (uint)(FloorsNumber + delta);
        }

        public abstract void ChangeFloorsNumber(int delta);


    }
}
