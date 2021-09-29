using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands.InsuranceCommands
{
    
        /// <summary>
        /// command for opiperson btn, OPICompany = OtherPersonInsurance-Person
        /// </summary>
        public class OPIPersonBtn : BaseCommand
        {
            public override bool CanExecute(object parameter = null) => true;

            public override void Execute(object parameter)
            {

            }
        }
}
