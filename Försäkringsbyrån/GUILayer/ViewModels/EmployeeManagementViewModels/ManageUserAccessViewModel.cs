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
    public class ManageUserAccessViewModel : BaseViewModel
    {
        public static readonly ManageUserAccessViewModel Instance = new ManageUserAccessViewModel();

        public ManageUserAccessViewModel()
        {

        }

        #region properties
        public string Username
        {
            get => SelectedPerson.Username;
            set
            {
                SelectedPerson.Username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get => SelectedPerson.Password;
            set
            {
                SelectedPerson.Password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Lastname
        {
            get => SelectedPerson.Lastname;
            set
            {
                SelectedPerson.Lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public string Firstname
        {
            get => SelectedPerson.Firstname;
            set
            {
                SelectedPerson.Firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        //SelectedPerson is used to select rowdata in datagrid. 
        private UserAccess _selectedPerson;
        public UserAccess SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        private ObservableCollection<UserAccess> _users;
        public ObservableCollection<UserAccess> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        #endregion
        #region properties bools for access
        public bool Search
        {
            get => SelectedPerson.Search;
            set
            {
                SelectedPerson.Search = value;
                OnPropertyChanged("Search");
            }
        }

        public bool StatisticsAndProspects
        {
            get => SelectedPerson.StatisticsAndProspects;
            set
            {
                SelectedPerson.StatisticsAndProspects = value;
                OnPropertyChanged("StatisticsAndProspects");
            }
        }

        public bool EmployeeManagement
        {
            get => SelectedPerson.EmployeeManagement;
            set
            {
                SelectedPerson.EmployeeManagement = value;
                OnPropertyChanged("EmployeeManagement");
            }
        }

        public bool Insurances
        {
            get => SelectedPerson.Insurances;
            set
            {
                SelectedPerson.Insurances = value;
                OnPropertyChanged("Insurances");
            }
        }

        public bool BasicData
        {
            get => SelectedPerson.BasicData;
            set
            {
                SelectedPerson.BasicData = value;
                OnPropertyChanged("BasicData");
            }
        }
        public bool Commission
        {
            get => SelectedPerson.Commission;
            set
            {
                SelectedPerson.Commission = value;
                OnPropertyChanged("BasicData");
            }
        }
        #endregion
        #region methods and commands

        public void UpdateGridToDb()
        {
            Users = UpdateUA();
            foreach (UserAccess ua in Users)
            {
                Context.UAController.EditUser(ua);
            }
        }
        //method to get all useraccesses in db. 
        public ObservableCollection<UserAccess> UpdateUA()
        {
            ObservableCollection<UserAccess> x = new ObservableCollection<UserAccess>();
            foreach (var u in Context.UAController.GetAllUsers())
            {
                if (u.Username != "ADMIN")
                    x?.Add(u);
            }
            Users = x;
            return Users;
        }

        private ICommand _deleteUserBtn;
        public ICommand DeleteUserBtn => _deleteUserBtn ?? (_deleteUserBtn = new RelayCommand(x => { DeleteUser(); }));

        /// <summary>
        /// Method for deleting user
        /// </summary>
        private void DeleteUser()
        {
            if (SelectedPerson != null)
            {
                if (SelectedPerson != Context.CurrentUser)
                {
                    MessageBoxResult result = MessageBox.Show("Vill du ta bort behörig?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SelectedPerson.Username = Username;
                        SelectedPerson.Password = Password;
                        SelectedPerson.Firstname = Firstname;
                        SelectedPerson.Lastname = Lastname;
                        SelectedPerson.Search = Search;
                        SelectedPerson.StatisticsAndProspects = StatisticsAndProspects;
                        SelectedPerson.Insurances = Insurances;
                        SelectedPerson.EmployeeManagement = EmployeeManagement;
                        SelectedPerson.Commission = Commission;
                        SelectedPerson.BasicData = BasicData;

                        Context.UAController.RemoveUser(SelectedPerson);
                        Users.Clear();
                        foreach (var u in Context.UAController.GetAllUsers())
                        {
                            if (u.Username != "Admin")
                                Users?.Add(u);
                        }
                        MessageBox.Show("Borttagningen lyckades:", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedPerson.Username} är inte borttagen");
                    }
                }
                else
                {
                    MessageBox.Show("Den behörige du försöker ta bort är dig själv, det går därmed inte att ta bort. Ska du försvinna kontakta it service, eller någon annan med personhanterings behörighet");
                }
            }
            else
            {
                MessageBox.Show("Du måste markera en behörig i registret", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }

}
