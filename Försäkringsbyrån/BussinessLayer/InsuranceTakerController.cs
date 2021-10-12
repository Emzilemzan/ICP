using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Insurancetakers, Companies and Persons. 
    /// </summary>
    public class InsuranceTakerController
    {

     

        public IEnumerable<Person> GetAllPersons() => BusinessController.Instance.Context.Persons.GetAll();

        public IEnumerable<Company> GetAllCompanies() => BusinessController.Instance.Context.Companies.GetAll();

        public Company GetCompany(string id) => BusinessController.Instance.Context.Companies.GetById(id);

        public Person GetPerson(string id) => BusinessController.Instance.Context.Persons.GetById(id);

        #region Companies
        public void AddCompanyInsuranceTaker(Company insuranceTaker)
        {
            BusinessController.Instance.Context.Companies.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }

        public void RemoveCompanyInsuranceTaker(Company insuranceTaker)
        {
            BusinessController.Instance.Context.Companies.Remove(insuranceTaker);
            BusinessController.Instance.Save();
        }

        #endregion

        #region Persons
        public void AddPersonInsuranceTaker(Person insuranceTaker)
        {
            BusinessController.Instance.Context.Persons.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }
      
      
        public void RemovePersonInsuranceTaker(Person insuranceTaker)
        {
            BusinessController.Instance.Context.Persons.Remove(insuranceTaker);
            BusinessController.Instance.Save();
        }


        public void CheckExistingPerson(string id, Person a, string fname, string lname, string city, int postalCode, string streetadress, string number1, string nbr2, string dnbr1, string dnbr2, 
            string email1, string email2)
        {
            Person x = BusinessController.Instance.Context.Persons.GetById(id);
            if (x == null)
            {
                AddPersonInsuranceTaker(a);
            }
            else
            {
                x.SocialSecurityNumber = id;
                x.Firstname = fname;
                x.Lastname = lname;
                x.City = city;
                x.PostalCode = postalCode;
                x.StreetAddress = streetadress;
                x.TelephoneNbrHome = number1;
                x.TelephoneNbrWork = nbr2;
                x.DiallingCodeHome = dnbr1;
                x.DiallingCodeWork = dnbr2;
                x.EmailOne = email1;
                x.EmailTwo = email2;

                Edit(x);
            }
        }
        public void Edit(Person sm)
        {
            BusinessController.Instance.Context.Persons.Update(sm);
            BusinessController.Instance.Save();
        }


        #endregion
    }
}
