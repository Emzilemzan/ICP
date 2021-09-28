using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands.InsuranceCommands
{
    /// <summary>
    /// command for opicompany btn, OPICompany = OtherPersonInsurance-Company
    /// </summary>
    public class OPICompanyBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {

        }
    }
}
