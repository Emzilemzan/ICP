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

        private ICommand _addEmployeeBtn;
        public ICommand AddEmployeeBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { Employee(); }));
        }

        private static void Employee()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.DisplayHomeView();
        }
        private EmployeeManagementViewModel()
        {
              
        }
    }
}
