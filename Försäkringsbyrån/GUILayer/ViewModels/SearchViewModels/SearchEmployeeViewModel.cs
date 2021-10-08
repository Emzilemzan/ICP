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
            SalesMens = UpdateSM();
            EmployeeGrid = CollectionViewSource.GetDefaultView(SalesMens);
            EmployeeGrid.Filter = new Predicate<object>(o => Filter(o as SalesMen));
        }
        #region methods
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach (var e in Context.SMController.GetAllSalesMen())
            {
                x?.Add(e);
            }
            SalesMens = x;
            return SalesMens;
        }
        #endregion

        #region Specific Porperties and methods for search in collection

        private ICollectionView _employeeCollection;
        public ICollectionView EmployeeGrid
        {
            get { return _employeeCollection; }
            set { _employeeCollection = value; OnPropertyChanged("EmployeeGrid"); }
        }
        private bool Filter(SalesMen employee)
        {
            return SearchInput == null
                || employee.AgentNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Firstname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Lastname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.StreetAddress.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.City.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.Postalcode.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || employee.TaxRate.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;

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
        public ObservableCollection<SalesMen> SalesMens { get; set; }

        private SalesMen _salesMen;
        public SalesMen SelectedPerson
        {
            get => _salesMen;
            set
            {
                _salesMen = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        
        #endregion
    }
}
