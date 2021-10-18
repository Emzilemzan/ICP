using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GUILayer.Commands;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    public class AddUserAccessViewModel : BaseViewModel
    {
        public static readonly AddUserAccessViewModel Instance = new AddUserAccessViewModel();
        public AddUserAccessViewModel()
        {

        }
        #region properties
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
        #region command
        private ICommand _addUserBtn;
        public ICommand AddUserBtn
        {
            get => _addUserBtn ?? (_addUserBtn = new RelayCommand(x => { InsertUser(); }));
        }

        public void EmptyAllChoices()
        {
            Instance.Username = string.Empty;
            Instance.Password = string.Empty;
            Instance.Lastname = string.Empty;
            Instance.Firstname = string.Empty;
            Instance.Search = false;
            Instance.StatisticsAndProspects = false;
            Instance.BasicData = false;
            Instance.Commission = false;
            Instance.EmployeeManagement = false;
            Instance.Insurances = false;
        }

        private void InsertUser()
        {
            if (Instance._username != null && Instance.Password != null && Instance.Firstname != null && Instance.Lastname != null &&
                (Instance.StatisticsAndProspects != false || Instance.Commission != false || Instance.Insurances != false
                || Instance.EmployeeManagement != false || Instance.BasicData != false || Instance.Search != false))
            {
                UserAccess a = new UserAccess()
                {
                    Username = Instance.Username,
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
                Context.UAController.CheckExistingUser(Instance._username, a);
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                EmptyAllChoices();
                ManageUserAccessViewModel.Instance.UpdateUA();
                MainViewModel.Instance.SelectedViewModel = ManageUserAccessViewModel.Instance;
            }
            else
            {
                MessageBox.Show("Inget fält får lämnas tomt och minst en behörighet måste väljas", "Felinmatning", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
        #endregion
    }
}
