using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData.Models
{
    public class MaximumNumberOfCompanies : Exception { }
    public class CompanyNameRepeat : Exception
    {
        public CompanyNameRepeat() : base("Cant add two offices of one company to one office") { }
    }

    public class OfficeCompanies : Repo<OfficeCompany> { }

    public class Office : Building
    {

        private OfficeCompanies _companies = new();

        public uint MaxCompaniesNumber
        {
            get => MaxCompaniesNumber;
            set
            {
                if (value < CompaniesCount)
                {
                    throw new ArgumentException("The maximum number of companies cannot be less than the existing one");
                }
                MaxCompaniesNumber = value;
            }
        }
        public int CompaniesCount { get => _companies.Count; }

        public override string Info => throw new NotImplementedException();

        public override void ChangeFloorsNumber(int delta)
        {
            DefaultChangeFloorsNumber(delta);
        }

        public void AddCompany(string companyName)
        {
            if (CompaniesCount == MaxCompaniesNumber)
            {
                throw new MaximumNumberOfCompanies();
            }
            List<OfficeCompany> officeCompanies = (List<OfficeCompany>)_companies.Find((company) => company.Name == companyName);
            if (officeCompanies.Count != 0) throw new CompanyNameRepeat();
            _companies.Add(new OfficeCompany() { Name = companyName });
        }

        public bool DeleteCompany(string companyName)
        {
            List<OfficeCompany> officeCompanies = (List<OfficeCompany>)_companies.Find((company) => company.Name == companyName);
            if (officeCompanies.Count == 0)
            {
                return false;
            }
            return _companies.Delete(officeCompanies[0]);
        }

    }
}
