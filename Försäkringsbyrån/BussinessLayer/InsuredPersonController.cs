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

        public List<InsuredPerson> GetInsuranceTakerIPS(Person insuranceTaker)
        {
            List<InsuredPerson> insuredPeople = new List<InsuredPerson>();
            GetInsuranceTakerIP(insuranceTaker)?.ForEach(p => insuredPeople.Add(p));
            return insuredPeople.OrderByDescending(i => i.InsuredId).ToList();
        }

        public IEnumerable<InsuredPerson> GetAllInsuredPersons() => BusinessController.Instance.Context.InsuredPersons.GetAll();

        public InsuredPerson GetIPerson(int id) => BusinessController.Instance.Context.InsuredPersons.GetById(id);
        public List<InsuredPerson> GetInsuranceTakerIPC(Company insuranceTaker) => insuranceTaker.InsuredPersons?.OrderByDescending(i => i.InsuredId).ToList();

        public List<InsuredPerson> GetInsuranceTakerIPSC(Company insuranceTaker)
        {
            List<InsuredPerson> insuredPeople = new List<InsuredPerson>();
            GetInsuranceTakerIPC(insuranceTaker)?.ForEach(p => insuredPeople.Add(p));
            return insuredPeople.OrderByDescending(i => i.InsuredId).ToList();
        }

        public void AddInsuredPerson(InsuredPerson insuredPerson)
        {
            BusinessController.Instance.Context.InsuredPersons.Add(insuredPerson);
            BusinessController.Instance.Save();
        }

        public void RemoveInsuredPerson(InsuredPerson insuredPerson)
        {
            BusinessController.Instance.Context.InsuredPersons.Remove(insuredPerson);
            BusinessController.Instance.Save();
        }
        public void Edit(InsuredPerson sm)
        {
            BusinessController.Instance.Context.InsuredPersons.Update(sm);
            BusinessController.Instance.Save();
        }

        #endregion
    }
}
