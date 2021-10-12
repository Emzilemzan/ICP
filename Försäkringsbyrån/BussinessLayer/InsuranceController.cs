using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class InsuranceController
    {
        #region Insurance for InsuranceTaker. 
        public List<Insurance> GetInsuranceTakerIA(Person insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerIAS(Person insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerIA(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }
        public void AddInsuranceApplication(Insurance insuranceApplication)
        {
            insuranceApplication.PersonTaker.Insurances.Add(insuranceApplication);
            BusinessController.Instance.Save();
        }
        public void RemoveInsuranceApplication(Insurance insuranceApplication)
        {
            insuranceApplication.PersonTaker.Insurances.Remove(insuranceApplication);
            BusinessController.Instance.Save();
        }

        public List<Insurance> GetInsuranceTakerCI(Company insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerCias(Company insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerCI(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }
        public void AddInsuranceApplicationCI(Insurance insuranceApplication)
        {
            insuranceApplication.CompanyTaker.Insurances.Add(insuranceApplication);
            BusinessController.Instance.Save();
        }
        public void RemoveInsuranceApplicationCIA(Insurance insuranceApplication)
        {
            insuranceApplication.CompanyTaker.Insurances.Remove(insuranceApplication);
            BusinessController.Instance.Save();
        }

        #endregion

        #region insurancetype
        public InsuranceType GetInsuranceType(int id) => BusinessController.Instance.Context.InsuranceTypes.Find(x => x.InsuranceTypeId == id).FirstOrDefault();
        public IEnumerable<InsuranceType> GetAllIT() => BusinessController.Instance.Context.InsuranceTypes.GetAll();

        public OptionalType GetOPT(int id) => BusinessController.Instance.Context.OptionalTypes.Find(x => x.OptionalTypeId == id).FirstOrDefault();

        public IEnumerable<OptionalType> GetAllOPT() => BusinessController.Instance.Context.OptionalTypes.GetAll();
        #endregion

        public void AddOptionalTypes(OptionalType insuranceTaker)
        {
            BusinessController.Instance.Context.OptionalTypes.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }
     

       
    }
}



  