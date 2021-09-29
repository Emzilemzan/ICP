using GUILayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.InsuranceCommands
{
    /// <summary>
    /// command for CIBtn, CI = companyinsurance 
    /// </summary>
    public class CIBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            
        }
    }
}
