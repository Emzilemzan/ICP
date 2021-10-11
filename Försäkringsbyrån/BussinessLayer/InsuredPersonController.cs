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
        public List<InsuredPerson> GetInsuranceTakerIP(Person insuranceTaker) => insuranceTaker.InsuredPersons?.OrderByDescending(i => i.InsuredID).ToList();
        public List<InsuredPerson> GetInsuranceTakerIPS(Person insuranceTaker)
        {
            List<InsuredPerson> insuredPeople = new List<InsuredPerson>();
            GetInsuranceTakerIP(insuranceTaker)?.ForEach(p => insuredPeople.Add(p));
            return insuredPeople.OrderByDescending(i => i.InsuredID).ToList();
        }
        public void AddInsuredPerson(InsuredPerson insuredPerson)
        {
            insuredPerson.PersonTaker.InsuredPersons.Add(insuredPerson);
            BusinessController.Instance.Save();
        }
        public void RemoveInsuredPerson(InsuredPerson insuredPerson)
        {
            insuredPerson.PersonTaker.InsuredPersons.Remove(insuredPerson);
            BusinessController.Instance.Save();
        }


        #endregion

    }
}
