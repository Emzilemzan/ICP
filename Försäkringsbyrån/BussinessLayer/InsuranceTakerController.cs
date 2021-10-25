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
        public IEnumerable<CustomerProspect> GetProspects() => BusinessController.Instance.Context.Prospects.GetAll();
        public IEnumerable<Person> GetAllPersons() => BusinessController.Instance.Context.Persons.GetAll();

        public IEnumerable<Company> GetAllCompanies() => BusinessController.Instance.Context.Companies.GetAll();

        public Company GetCompany(string id) => BusinessController.Instance.Context.Companies.GetById(id);

        public Person GetPerson(string id) => BusinessController.Instance.Context.Persons.GetById(id);

        public void AddProspect(CustomerProspect cp)
        {
            BusinessController.Instance.Context.Prospects.Add(cp);
            BusinessController.Instance.Save();
        }

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


        public void CheckExistingCompany(string id, Company a, string name, string city, int postalCode, string streetadress, string number1, string nbr2,
           string email1, string contact, string num)
        {
            Company x = BusinessController.Instance.Context.Companies.GetById(id);
            if (x == null)
            {
                AddCompanyInsuranceTaker(a);
            }
            else
            {
                x.OrganizationNumber = id;
                x.CompanyName= name;
                x.City = city;
                x.PostalCode = postalCode;
                x.StreetAddress = streetadress;
                x.TelephoneNbr = number1;
                x.DiallingCode = nbr2;
                x.Email = email1;
                x.ContactPerson = contact;
                x.FaxNumber = num;
                Edit(x);
            }
        }
        public void Edit(Company sm)
        {
            BusinessController.Instance.Context.Companies.Update(sm);
            BusinessController.Instance.Save();
        }

        public bool CheckCompanyInInsurance(Company a)
        {
            bool result = false;
            foreach (var i in BusinessController.Instance.IController.GetAllInsurances())
            {
                if (i.CompanyTaker != null)
                {
                    if (i.CompanyTaker.Equals(a))
                    {
                        result = true;
                    }
                }
            }
            return result;
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

        public bool CheckPersonInInsurance(Person a)
        {
            bool result = false;
            foreach (var i in BusinessController.Instance.IController.GetAllInsurances())
            {
                if(i.PersonTaker != null)
                {
                    if (i.PersonTaker.Equals(a))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        

        #endregion
    }
}
