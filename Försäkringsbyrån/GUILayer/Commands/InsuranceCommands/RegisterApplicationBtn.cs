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
            if (MainViewModel.Instance.CurrentTool != "InsuranceChoices")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = InsuranceViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "InsuranceChoices";
                MainViewModel.Instance.SelectedViewModel = InsuranceApplicationChoiceViewModel.Instance;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = InsuranceViewModel.Instance;
            }
        }
    }
}
