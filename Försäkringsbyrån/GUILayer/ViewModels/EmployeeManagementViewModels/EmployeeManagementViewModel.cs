using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
