using GUILayer.ViewModels;
using GUILayer.ViewModels.EmployeeManagementViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.EmployeeManagementCommands
{
    /// <summary>
    /// One click and employeemanagement menuchoices shows, click again and weolcome page shows. 
    /// </summary>
    public class EmployeeManagementBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.CurrentTool != "EmployeeManagement")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = EmployeeManagementViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "EmployeeManagement";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }

        }

    }
    
}
