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

        public IEnumerable<InsuranceTaker> GetAllInsuranceTakers() => BusinessController.Instance.Context.InsuranceTakers.GetAll();

        public IEnumerable<Person> GetAllPersons() => BusinessController.Instance.Context.Persons.GetAll();

        public IEnumerable<Company> GetAllCompanies() => BusinessController.Instance.Context.Companies.GetAll();


        public void AddInsuranceTaker(InsuranceTaker insuranceTaker)
        {
            BusinessController.Instance.Context.InsuranceTakers.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }

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
        public void RemoveInsuranceTaker(InsuranceTaker insuranceTaker)
        {
            BusinessController.Instance.Context.InsuranceTakers.Remove(insuranceTaker);
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
        public void Edit(Person insuranceTaker)
        {
            Person edit = BusinessController.Instance.Context.Persons.GetById(insuranceTaker.InsuranceTakerId);
            edit.Lastname = insuranceTaker.Lastname;
            edit.Firstname = insuranceTaker.Firstname;
            edit.SocialSecurityNumber = insuranceTaker.SocialSecurityNumber;
            edit.StreetAddress = insuranceTaker.StreetAddress;
            edit.City = insuranceTaker.City;
            edit.PostalCode = insuranceTaker.PostalCode;
            edit.TelephoneNbrHome = insuranceTaker.TelephoneNbrHome;
            edit.TelephoneNbrWork = insuranceTaker.TelephoneNbrWork;
            edit.DiallingCodeHome = insuranceTaker.DiallingCodeHome;
            edit.DiallingCodeWork = insuranceTaker.DiallingCodeWork;
            edit.EmailOne = insuranceTaker.EmailOne;
            edit.EmailTwo = insuranceTaker.EmailTwo;


            BusinessController.Instance.Save();
        }
    }
}
