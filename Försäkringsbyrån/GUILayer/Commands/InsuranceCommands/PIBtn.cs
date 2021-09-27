using GUILayer.ViewModels;
using GUILayer.ViewModels.InsuranceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.InsuranceCommands
{
    /// <summary>
    /// command for PI btn = personinsurance
    /// </summary>
    public class PIBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
           
        }
    }
}
