using GUILayer.Commands.InsuranceCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// viewmodel for insuranceview. 
    /// </summary>
    public class InsuranceViewModel : BaseViewModel
    {
        public static readonly InsuranceViewModel Instance = new InsuranceViewModel();

        public RegisterApplicationBtn RegisterApplication_Btn {get;}
        public RegisterSignedInsuranceBtn RegisterSignedInsurance_Btn { get; }

        private InsuranceViewModel()
        {
            RegisterSignedInsurance_Btn = new RegisterSignedInsuranceBtn();
            RegisterApplication_Btn = new RegisterApplicationBtn();
        }
    }
}
