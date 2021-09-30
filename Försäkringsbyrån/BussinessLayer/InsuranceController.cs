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
        public List<Insurance> GetInsuranceTakerIA(InsuranceTaker insuranceTaker) => insuranceTaker.Insurances?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<Insurance> GetInsuranceTakerIAS(InsuranceTaker insuranceTaker)
        {
            List<Insurance> applications = new List<Insurance>();
            GetInsuranceTakerIA(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }
        public void AddInsuranceApplication(Insurance insuranceApplication)
        {
            insuranceApplication.Taker.Insurances.Add(insuranceApplication);
            BusinessController.Instance.Save();
        }
        public void RemoveInsuranceApplication(Insurance insuranceApplication)
        {
            insuranceApplication.Taker.Insurances.Remove(insuranceApplication);
            BusinessController.Instance.Save();
        }
        //public void AddSignedInsurance(SignedInsurance signedInsurance)
        //{
        //    signedInsurance.Taker.SignedInsurances.Add(signedInsurance);
        //    BusinessController.Instance.Save();
        //}

    }
}



  