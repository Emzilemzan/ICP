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

        #region commands
        private ICommand _addEmployeeBtn;
        public ICommand AEVBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { AddEmployeeView(); }));
        }

        private ICommand _handleEmployeeBtn;
        public ICommand HEVBtn
        {
            get => _handleEmployeeBtn ?? (_handleEmployeeBtn = new RelayCommand(x => { HandleEmployeeView(); }));
        }

        private void HandleEmployeeView()
        {
            if (MainViewModel.Instance.CurrentTool != "HandleEmployee")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = HandleEmployeeViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "HandleEmployee";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }
        private ICommand _addUserBtn;
        public ICommand AUABtn
        {
            get => _addUserBtn ?? (_addUserBtn = new RelayCommand(x => { AddUserAccessView(); }));
        }

        private void AddUserAccessView()
        {
            if (MainViewModel.Instance.CurrentTool != "AddUserAccess")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = AddUserAccessViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "AddUserAccess";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }
        private ICommand _manageUserBtn;
        public ICommand HBBtn
        {
            get => _manageUserBtn ?? (_manageUserBtn = new RelayCommand(x => { ManageUserView(); }));
        }

        private void ManageUserView()
        {
            if (MainViewModel.Instance.CurrentTool != "ManageUser")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = ManageUserAccessViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "ManageUser";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }
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
        #endregion
    }
}
