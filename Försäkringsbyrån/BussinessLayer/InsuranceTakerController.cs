using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Insurancetakers. . 
    /// </summary>
    public class InsuranceTakerController
    {

        public IEnumerable<InsuranceTaker> GetAllInsuranceTakers() => BusinessController.Instance.Context.InsuranceTakers.GetAll();
        

        public void AddInsuranceTaker(InsuranceTaker insuranceTaker)
        {
            BusinessController.Instance.Context.InsuranceTakers.Add(insuranceTaker);
            BusinessController.Instance.Save();
        }

        public void RemoveInsuranceTaker(InsuranceTaker insuranceTaker)
        {
            BusinessController.Instance.Context.InsuranceTakers.Remove(insuranceTaker);
            BusinessController.Instance.Save();
        }
    }
}
