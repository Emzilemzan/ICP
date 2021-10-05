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
    public class HandleEmployeeViewModel : BaseViewModel
    {
        public static readonly HandleEmployeeViewModel Instance = new HandleEmployeeViewModel();

        public HandleEmployeeViewModel()
        {
            Employees = UpdateEmployees();
            EmployeeGrid = CollectionViewSource.GetDefaultView(Employees);
            EmployeeGrid.Filter = new Predicate<object>(o => Filter(o as Employee));
            
        }

        #region methods
        public ObservableCollection<Employee> UpdateEmployees()
        {
            ObservableCollection<Employee> x = new ObservableCollection<Employee>();
            foreach (var e in Context.EController.GetAllEmployees())
            {
                x?.Add(e);
            }
            Employees = x;
            return Employees;
        }
              
        #endregion

        private ICommand _updateEmployeeBtn;
        public ICommand UpdateEmployeeBtn
        {
            get => _updateEmployeeBtn ?? (_updateEmployeeBtn = new RelayCommand(x => { UpdateEmployee(); CanCommand(); }));
        }

        private void UpdateEmployee()
        {
          
                Employee employee = new Employee()
                {
                    EmploymentNo = Instance._employmentNo,
                    Username = Instance._username,
                    Password = Instance._password,
                    Firstname = Instance._firstname,
                    Lastname = Instance._lastname,
                    StreetAddress = Instance._streetAddress,
                    City = Instance._city,
                    Postalcode = Instance._postalCode,
                    FormOfEmployment = TryParseFoe(Instance._foe),
                    TaxRate = TryParseTR(Instance._taxRate),
                    BasicData = Instance.BasicData,
                    Commission = Instance.Commission,
                    Insurances = Instance.Insurances,
                    EmployeeManagement = Instance.EmployeeManagement,
                    Search = Instance.Search,
                    StatisticsAndProspects = Instance.StatisticsAndProspects,
                    CEO = Instance.Ceo,
                    EconomyAssistent = Instance.Economyassistent,
                    FieldSalesMen = Instance.FieldsalesMen,
                    OfficeSalesMen = Instance.OfficesalesMen,
                    SalesAssistent = Instance.Salesassistent,
                    SalesManager = Instance.Salesmanager,
                    AgentNumber = Instance.AgentNumber,
                    //Accesses = Createaccess(),
                    //Roles = Createrole(),
                    //SalesMen = InsertSalesMen(),
                };
                Context.EController.Edit(employee);
                MessageBox.Show("En ny anställd har lagts till");
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                HandleEmployeeViewModel.Instance.UpdateEmployees();
                MainViewModel.Instance.SelectedViewModel = HandleEmployeeViewModel.Instance;
            
        }


        /// <summary>
        /// if the user write in a text or doesn't fill in a number the formofemployment its automaticly 100. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int TryParseFoe(string value)
        {
            int nNumber = int.TryParse(value, out nNumber) ? nNumber : 100;
            return nNumber;
        }
        /// <summary>
        /// if the user write in a text or doesn't fill in a number the taxrate its automaticly 29. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int TryParseTR(string value)
        {
            int nNumber = int.TryParse(value, out nNumber) ? nNumber : 29;
            return nNumber;
        }


        public bool CanCommand() => true;

        #region search
        private ICollectionView _employeeCollection;
        public ICollectionView EmployeeGrid
        {
            get { return _employeeCollection; }
            set { _employeeCollection = value; OnPropertyChanged("EmployeeGrid"); }
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

        private Employee _selectedPerson;

        public Employee SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }

        private string _agentNumber;
        public string AgentNumber
        {
            get => _agentNumber;
            set
            {
                _agentNumber = value;
                OnPropertyChanged("AgentNumber");
            }

        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

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

        private string _postalCode;
        public string Postalcode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged("PostalCode");
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

        private int _employmentNo;
        public int EmploymentNo
        {
            get => _employmentNo;
            set
            {
                _employmentNo = value;
                OnPropertyChanged("EmploymentNo");
            }
        }

        private string _taxRate;
        public string TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                OnPropertyChanged("TaxRate");
            }
        }

        private string _foe;
        public string FormOfEmployment
        {
            get => _foe;
            set
            {
                _foe = value;
                OnPropertyChanged("FormOfEmployment");
            }
        }

        private bool _ceo;

        public bool Ceo
        {
            get => _ceo;
            set
            {
                _ceo = value;
                OnPropertyChanged("Ceo");
            }
        }
        private bool _salesAssistent;

        public bool Salesassistent
        {
            get => _salesAssistent;
            set
            {
                _salesAssistent = value;
                OnPropertyChanged("Salesassistent");
            }
        }
        private bool _salesM;
        public bool Salesmanager
        {
            get => _salesM;
            set
            {
                _salesM = value;
                OnPropertyChanged("Salesmanager");
            }
        }
        private bool _fieldSalesMen;
        public bool FieldsalesMen
        {
            get => _fieldSalesMen;
            set
            {
                _fieldSalesMen = value;
                OnPropertyChanged("FieldsalesMen");
            }
        }
        private bool _officeSalesMen;
        public bool OfficesalesMen
        {
            get => _officeSalesMen;
            set
            {
                _officeSalesMen = value;
                OnPropertyChanged("OfficesalesMen");
            }
        }
        private bool _economyA;
        public bool Economyassistent
        {
            get => _economyA;
            set
            {
                _economyA = value;
                OnPropertyChanged("Economyassistent");
            }
        }

        private bool _search;
        public bool Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged("Search");
            }
        }

        private bool _sap;
        public bool StatisticsAndProspects
        {
            get => _sap;
            set
            {
                _sap = value;
                OnPropertyChanged("StatisticsAndProspects");
            }
        }

        private bool _employeeManagement;
        public bool EmployeeManagement
        {
            get => _employeeManagement;
            set
            {
                _employeeManagement = value;
                OnPropertyChanged("EmployeeManagement");
            }
        }

        private bool _insurances;
        public bool Insurances
        {
            get => _insurances;
            set
            {
                _insurances = value;
                OnPropertyChanged("Insurances");
            }
        }

        private bool _basicData;
        public bool BasicData
        {
            get => _basicData;
            set
            {
                _basicData = value;
                OnPropertyChanged("BasicData");
            }
        }

        private bool _commission;
        public bool Commission
        {
            get => _commission;
            set
            {
                _commission = value;
                OnPropertyChanged("BasicData");
            }
        }
        #endregion
    }
}
