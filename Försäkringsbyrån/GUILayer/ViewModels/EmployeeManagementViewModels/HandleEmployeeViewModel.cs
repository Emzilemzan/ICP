using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    /// <summary>
    /// handle salesmen. 
    /// </summary>
    public class HandleEmployeeViewModel : BaseViewModel
    {
        public static readonly HandleEmployeeViewModel Instance = new HandleEmployeeViewModel();

        public HandleEmployeeViewModel()
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

        private ICommand _updateEmployeeBtn;
        public ICommand UpdateEmployeeBtn
        {
            get => _updateEmployeeBtn ?? (_updateEmployeeBtn = new RelayCommand(x => { UpdateSalesMen(); CanCommand(); }));
        }

        private void UpdateSalesMen()
        {
          
        }


       

        public bool CanCommand() => true;

        #region search
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
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                EmployeeGrid.Refresh();
            }
        }
        #endregion




        #region properties
        private SalesMen _selectedPerson;

        public SalesMen SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public ObservableCollection<SalesMen> SalesMens { get; set; }


        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        private string _streetAddress;
        public string StreetAddress
        {
            get => _streetAddress;
            set
            {
                _streetAddress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        private int _postalCode;
        public string Postalcode
        {
            get => _postalCode > 0 ? _postalCode.ToString() : "";
            set
            {
                if (int.TryParse(value, out _postalCode) && _postalCode.ToString().Length < 6 && _postalCode != 0)
                {
                    OnPropertyChanged("PostalCode");
                }
                else
                {
                    MessageBox.Show("Postnummer måste bestå av 5 siffror och kan inte vara en text");
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        private int _agentNumber;
        public string AgentNumber
        {
            get => _agentNumber > 0 ? _agentNumber.ToString() : "";
            set
            {
                if (int.TryParse(value, out _agentNumber) && _agentNumber > 0 && _agentNumber != 0)
                {
                    OnPropertyChanged("AgentNumber");
                }
                else
                {
                    MessageBox.Show("Anställningsnummer måste vara ett nummer och får inte heller sättas till 0");
                }
            }
        }

        private double _taxRate;
        public string TaxRate
        {
            get => _taxRate > 0 ? _taxRate.ToString() : "";
            set
            {
                if (double.TryParse(value, out _taxRate) && _taxRate > 0 && _taxRate <= 100)
                {
                    OnPropertyChanged("TaxRate");
                }
                else
                {
                    MessageBox.Show("Skattesatsen måste vara ett nummer mellan 0 & 100");
                }
            }
        }


        #endregion
    }
}
