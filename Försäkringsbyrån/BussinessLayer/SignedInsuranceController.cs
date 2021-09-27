using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class SignedInsuranceController
    {
        public void AddSignedInsurance(SignedInsurance signedInsurance)
        {
            signedInsurance.Taker.SignedInsurances.Add(signedInsurance);
            BusinessController.Instance.Save();
        }
    }
}
