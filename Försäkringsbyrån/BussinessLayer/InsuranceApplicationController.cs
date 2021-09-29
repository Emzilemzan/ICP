using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Insurance applications. 
    /// </summary>
    public class InsuranceApplicationController
    {
        public List<InsuranceApplication> GetInsuranceTakerIA(InsuranceTaker insuranceTaker) => insuranceTaker.InsuranceApplications?.OrderByDescending(i => i.InsuranceNumber).ToList();
        public List<InsuranceApplication> GetInsuranceTakerIAS(InsuranceTaker insuranceTaker)
        {
            List<InsuranceApplication> applications = new List<InsuranceApplication>();
            GetInsuranceTakerIA(insuranceTaker)?.ForEach(p => applications.Add(p));
            return applications.OrderByDescending(i => i.InsuranceNumber).ToList();
        }
        public void AddInsuranceApplication(InsuranceApplication insuranceApplication)
        {
            insuranceApplication.Taker.InsuranceApplications.Add(insuranceApplication);
            BusinessController.Instance.Save();
        }
        public void RemoveInsuranceApplication(InsuranceApplication insuranceApplication)
        {
            insuranceApplication.Taker.InsuranceApplications.Remove(insuranceApplication);
            BusinessController.Instance.Save();
        }

    }
}
