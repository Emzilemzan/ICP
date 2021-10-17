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
        public IEnumerable<Insurance> GetAllInsurances() => BusinessController.Instance.Context.Insurances.GetAll();
        #region Insurance for InsuranceTaker. 
        public List<Insurance> GetInsuranceTakerIA(Person insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerIAS(Person insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerIA(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }

        public List<Insurance> GetInsuranceTakerIAC(Company insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerIASC(Company insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerIAC(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }

        public void AddInsuranceApplication(Insurance insuranceApplication)
        {
            BusinessController.Instance.Context.Insurances.Add(insuranceApplication);
            BusinessController.Instance.Save();
        }
        public void RemoveInsurance(Insurance insuranceApplication)
        {
            BusinessController.Instance.Context.Insurances.Remove(insuranceApplication);
            BusinessController.Instance.Save();
        }

        public List<Insurance> GetInsuranceTakerCI(Company insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerCias(Company insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerCI(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }

        #endregion

        #region insurancetype
        public SAInsurance GetSA(int id) => BusinessController.Instance.Context.SAInsurances.Find(x => x.SAID == id).FirstOrDefault();
        public LifeInsurance GetLIFE(int id) => BusinessController.Instance.Context.LifeInsurances.Find(x => x.LifeID == id).FirstOrDefault();
        public OptionalType GetOPT(int id) => BusinessController.Instance.Context.OptionalTypes.Find(x => x.OptionalTypeId == id).FirstOrDefault();

        public IEnumerable<OptionalType> GetAllOPT() => BusinessController.Instance.Context.OptionalTypes.GetAll();

        public IEnumerable<SAInsurance> GetAllSAI() => BusinessController.Instance.Context.SAInsurances.GetAll();
        public IEnumerable<CompanyInsurance> GetAllCAI() => BusinessController.Instance.Context.CIInsurances.GetAll();
        public IEnumerable<OtherPersonInsurance> GetAllOPI() => BusinessController.Instance.Context.OPInsurances.GetAll();

        public IEnumerable<LifeInsurance> GetAllLIFE() => BusinessController.Instance.Context.LifeInsurances.GetAll();

        #endregion

        public void AddOptionalTypes(OptionalType i)
        {
            BusinessController.Instance.Context.OptionalTypes.Add(i);
            BusinessController.Instance.Save();
        }

        public void AddSaInsurances(SAInsurance i)
        {
            BusinessController.Instance.Context.SAInsurances.Add(i);
            BusinessController.Instance.Save();
        }

        public void AddCompanyInsurance(CompanyInsurance ci)
        {
            BusinessController.Instance.Context.CIInsurances.Add(ci);
            BusinessController.Instance.Save();
        }

        public void AddLifeInsurance(LifeInsurance li)
        {
            BusinessController.Instance.Context.LifeInsurances.Add(li);
            BusinessController.Instance.Save();
        }

        public void AddOtherPersonInsurance(OtherPersonInsurance oi)
        {
            BusinessController.Instance.Context.OPInsurances.Add(oi);
            BusinessController.Instance.Save();
        }
        public void Edit(Insurance i)
        {
            BusinessController.Instance.Context.Insurances.Update(i);
            BusinessController.Instance.Save();
        }
    }
}



