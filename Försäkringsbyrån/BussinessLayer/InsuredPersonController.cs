using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Insured persons. 
    /// </summary>
    public class InsuredPersonController
    {
        #region InsurancePerson that belongs to a InsuranceTaker. 
        public List<InsuredPerson> GetInsuranceTakerIP(Person insuranceTaker) => insuranceTaker.InsuredPersons?.OrderByDescending(i => i.InsuredId).ToList();

        public InsuredPerson GetPerson(string id) => BusinessController.Instance.Context.InsuredPersons.GetById(id);

        public List<InsuredPerson> GetInsuranceTakerIPS(Person insuranceTaker)
        {
            List<InsuredPerson> insuredPeople = new List<InsuredPerson>();
            GetInsuranceTakerIP(insuranceTaker)?.ForEach(p => insuredPeople.Add(p));
            return insuredPeople.OrderByDescending(i => i.InsuredId).ToList();
        }
        public void AddInsuredPersonOnPersontaker(InsuredPerson insuredPerson, Person p)
        {
            p.InsuredPersons.Add(insuredPerson);
            BusinessController.Instance.Save();
        }

        public void AddInsuredPerson(InsuredPerson insuredPerson)
        {
            BusinessController.Instance.Context.InsuredPersons.Add(insuredPerson);
            BusinessController.Instance.Save();
        }

        public void RemoveInsuredPerson(InsuredPerson insuredPerson, Person p)
        {
            p.InsuredPersons.Remove(insuredPerson);
            BusinessController.Instance.Save();
        }

         public void CheckExistingIP(string id, InsuredPerson p)
        {
            InsuredPerson x = BusinessController.Instance.Context.InsuredPersons.GetById(id);
            if (x == null)
            {
                AddInsuredPerson(p);
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
