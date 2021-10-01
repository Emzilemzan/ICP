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
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        public static readonly AddEmployeeViewModel Instance = new AddEmployeeViewModel();

        public AddEmployeeViewModel()
        {
        
        }

        private ICommand _addEmployeeBtn;
        public ICommand AddEmployeeBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { InsertEmployee(); }));
        }

        private void InsertEmployee()
        {
            Employee employee = new Employee()
            { 
                EmploymentNo = Instance.EmploymentNo, 
                Username = Instance.Username,
                Password = Instance.Password,
                Firstname = Instance.Firstname,
                Lastname = Instance.Lastname,
                StreetAddress = Instance.StreetAddress,
                City = Instance.City,
                Postalcode = Instance.Postalcode,
                FormOfEmployment = Instance.FormOfEmployment,
                TaxRate = Instance.TaxRate,

            };
            Context.EController.AddEmployee(employee);

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

        private int _postalCode;
        public int Postalcode
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

        private int _taxRate;
        public int TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                OnPropertyChanged("TaxRate");
            }
        }

        private int _foe;
        public int FormOfEmployment
        {
            get => _foe;
            set
            {
                _foe = value;
                OnPropertyChanged("FormOfEmployment");
            }
        }


        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();
       

        public ObservableCollection<Access> Accesses { get; set; } = new ObservableCollection<Access>();



    }

}
