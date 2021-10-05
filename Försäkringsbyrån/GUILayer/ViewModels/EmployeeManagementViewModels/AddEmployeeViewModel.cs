using GUILayer.Commands;
using GUILayer.ViewModels.SearchViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        public static readonly AddEmployeeViewModel Instance = new AddEmployeeViewModel();

        public AddEmployeeViewModel()
        {
            
        }

        #region commands

        
        private SalesMen InsertSalesMen()
        {
            SalesMen salesMen = new SalesMen()
            {
                AgentNumber = Instance.AgentNumber,
            };
            return salesMen;
        }


        #region properties

        private string _agentNumber;
        public string AgentNumber
        { get => _agentNumber;
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
        public string EmploymentNo
        {
            get => _employmentNo > 0 ? _employmentNo.ToString() : "";
            set
            {
                if (int.TryParse(value, out _employmentNo) && _employmentNo > 0 && _employmentNo != 0)
                {
                    OnPropertyChanged("EmploymentNo");
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
                if (double.TryParse(value, out _taxRate) && _taxRate > 0 && _taxRate < 101)
                {
                    OnPropertyChanged("TaxRate");
                }
                else
                {
                    MessageBox.Show("Skattesatsen måste vara ett nummer mellan 0 & 100");
                }
            }
        }

        private double _foe;
        public string FormOfEmployment
        {
            get => _foe > 0 ? _foe.ToString() : "";
            set
            {
                if (double.TryParse(value, out _foe) && _foe > 0 && _foe < 101)
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
        #region roles bools properties
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
        #endregion
        #region bools for access
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



        #region Manages GUI happenings. 
        
        private RelayCommand _addEmployeeBtn;
        public RelayCommand AddEmployeeBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { InsertEmployee(); CanCommand(); }));
        }

        public bool CanCommand()
        {
            return !string.IsNullOrWhiteSpace(Instance.EmploymentNo);

        }



        
        private void InsertEmployee()
        {
            if (Instance._employmentNo != 0)
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
                    FormOfEmployment = Instance._foe,
                    TaxRate = Instance._taxRate,
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
                };
                Context.EController.CheckExistingEmployee(Instance._employmentNo, employee);
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                HandleEmployeeViewModel.Instance.UpdateEmployees();
                MainViewModel.Instance.SelectedViewModel = HandleEmployeeViewModel.Instance;
            }
            else
            {
                MessageBox.Show("Anställningsnummer får inte lämnas tomt");
            }
        }

        #endregion 
    }

}
