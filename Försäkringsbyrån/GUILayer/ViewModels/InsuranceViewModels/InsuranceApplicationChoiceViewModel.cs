using GUILayer.Commands.InsuranceCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class InsuranceApplicationChoiceViewModel: BaseViewModel
    {
        public static readonly InsuranceApplicationChoiceViewModel Instance = new InsuranceApplicationChoiceViewModel();

        public PIBtn PI_Btn { get; }
        public OPICompanyBtn OPIC_Btn { get; }
        public OPIPersonBtn OPIP_Btn { get; }
        public CIBtn CI_Btn { get; }

        private InsuranceApplicationChoiceViewModel()
        {
            PI_Btn = new PIBtn();
            CI_Btn = new CIBtn();
            OPIC_Btn = new OPICompanyBtn();
            OPIP_Btn = new OPIPersonBtn();
        }
    }
}
