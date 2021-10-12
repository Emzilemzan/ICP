using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Insurancetakers, Companies and Persons. 
    /// </summary>
    public class InsuranceTakerController
    {

     

        public IEnumerable<Person> GetAllPersons() => BusinessController.Instance.Context.Persons.GetAll();

        public IEnumerable<Company> GetAllCompanies() => BusinessController.Instance.Context.Companies.GetAll();

        public Company GetCompany(int id) => BusinessController.Instance.Context.Companies.GetById(id);


      

        public void AddCompanyInsuranceTaker(Company insuranceTaker)
        {
            BusinessController.Instance.Context.Companies.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }
        public void AddPersonInsuranceTaker(Person insuranceTaker)
        {
            BusinessController.Instance.Context.Persons.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }
      
        public void RemoveCompanyInsuranceTaker(Company insuranceTaker)
        {
            BusinessController.Instance.Context.Companies.Remove(insuranceTaker);
            BusinessController.Instance.Save();
        }
        public void RemovePersonInsuranceTaker(Person insuranceTaker)
        {
            BusinessController.Instance.Context.Persons.Remove(insuranceTaker);
            BusinessController.Instance.Save();
        }
       
    }
}
