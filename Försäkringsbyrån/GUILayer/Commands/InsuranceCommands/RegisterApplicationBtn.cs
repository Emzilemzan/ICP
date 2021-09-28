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
 /// command for register application btn. 
 /// </summary>
    public class RegisterApplicationBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {

            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterApplicationViewModel.Instance; 
        }
     
    }
}
