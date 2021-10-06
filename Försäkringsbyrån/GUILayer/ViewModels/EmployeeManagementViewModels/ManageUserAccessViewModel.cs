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
    public class ManageUserAccessViewModel: BaseViewModel
    {
        public static readonly ManageUserAccessViewModel Instance = new ManageUserAccessViewModel();

        public ManageUserAccessViewModel()
        {
            Users = UpdateUA();
        }

        #region properties
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
                    OnPropertyChanged("Lasttname");
                
                  
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
        private object _selectedPerson;
        public object SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public ObservableCollection<UserAccess> Users { get; set; }
       
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
        #region methods and commands
        private ICommand _uUserBtn;
        public ICommand UpdateUserBtn
        {
            get => _uUserBtn ?? (_uUserBtn = new RelayCommand(x => { UpdateUser(); CanCommand(); }));
        }

        public bool CanCommand()
        {
            return !string.IsNullOrWhiteSpace(Instance.Username) && !string.IsNullOrWhiteSpace(Instance.Password) && !string.IsNullOrWhiteSpace(Instance.Firstname) && !string.IsNullOrWhiteSpace(Instance.Lastname);
        }

        private void UpdateUser()
        {
            if (Instance._username != null)
            {
                UserAccess a = new UserAccess()
                {
                   Username = Username,
                    Password = Instance.Password,
                    Firstname = Instance.Firstname,
                    Lastname = Instance.Lastname,
                    Search = Instance.Search,
                    StatisticsAndProspects = Instance.StatisticsAndProspects,
                    Insurances = Instance.Insurances,
                    EmployeeManagement = Instance.EmployeeManagement,
                    Commission = Instance.Commission,
                    BasicData = Instance.BasicData
                };

                Context.UAController.EditUser(a);
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                Instance.UpdateUA();
            }
            else
            {
                MessageBox.Show("Användarnamn får inte lämnas tomt");
            }
        }
        
        public ObservableCollection<UserAccess> UpdateUA()
        {
            ObservableCollection<UserAccess> x = new ObservableCollection<UserAccess>();
            foreach (var u in Context.UAController.GetAllUsers())
            {
                x?.Add(u);
            }
            Users = x;
            return Users;
        }

        #endregion

    }

}
