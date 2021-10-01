using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    /// <summary>
    /// viewmodel for employee management view
    /// </summary>
    public class EmployeeManagementViewModel : BaseViewModel
    {
        public static readonly EmployeeManagementViewModel Instance = new EmployeeManagementViewModel();

        private EmployeeManagementViewModel()
        {
              
        }


        private ICommand _addEmployeeBtn;
        public ICommand AEVBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { AddEmployeeView(); }));
        }

        public void AddEmployeeView()
        {
            if (MainViewModel.Instance.CurrentTool != "AddEmployee")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = AddEmployeeViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "AddEmployee";
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
