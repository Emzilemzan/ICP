using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Models.Models;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchEmployeeViewModel : BaseViewModel
    {
        public static readonly SearchEmployeeViewModel Instance = new SearchEmployeeViewModel();

        public SearchEmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>()
            {
                new Employee() { EmploymentNo = 1, Firstname = "Bill", Lastname = "Andersson", StreetAddress = "Vägen 12", Postalcode = 52335, City = "Borås", FormOfEmployment = 100, Role = RoleType.VD, TaxRate = 25, Username = "BA", Password = "x" },
                new Employee() { EmploymentNo = 2, Firstname = "Bertil", Lastname = "Andersson", StreetAddress = "XVägen 5", Postalcode = 52435, City = "Borås", FormOfEmployment = 70, Role = RoleType.Innesäljare, TaxRate = 25, Username = "BX", Password = "x" },
            };
            EmployeeGrid = CollectionViewSource.GetDefaultView(Employees);
            EmployeeGrid.Filter = new Predicate<object>(o => Filter(o as Employee));
        }


        #region Specific Porperties and methods for search in collection

        private ICollectionView _employeeCollection;
        public ICollectionView EmployeeGrid
        {
            get { return _employeeCollection; }
            set { _employeeCollection = value; OnPropertyChanged("employeeGrid"); }
        }
        private bool Filter(Employee employee)
        {
            return SearchInput == null
                || employee.EmploymentNo.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Firstname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Lastname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.StreetAddress.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.City.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Postalcode.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.FormOfEmployment.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.AgentNo.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.TaxRate.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Role.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;

        public string SearchInput
        {
            get =>_searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                EmployeeGrid.Refresh();   
            }
        }
        #endregion

        #region Properties 
        public ObservableCollection<Employee> Employees { get; set; }
       
        
        #endregion
    }
}
