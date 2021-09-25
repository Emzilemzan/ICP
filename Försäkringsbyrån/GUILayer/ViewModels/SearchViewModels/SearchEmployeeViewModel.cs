using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchEmployeeViewModel : BaseViewModel
    {
        public static readonly SearchEmployeeViewModel Instance = new SearchEmployeeViewModel();

        public SearchEmployeeViewModel()
        {
            
        }
        #region Properties
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        readonly int _employmentNo;
        public int EmploymentMo
        {
            get => _employmentNo;
        }
        
        readonly string _firstName;
        public string FirstName
        {
            get => _firstName;
        }

        readonly string _lastName;
        public string LastName
        {
            get => _lastName;
        }

        readonly  RoleType _role;
        public RoleType Role
        {
            get => _role;
        }

        readonly string _streetadress;
        public string StreetAdress
        {
            get => _streetadress;

        }

        readonly string _city;
        public string City
        {
            get => _city;

        }

        readonly int _postalcode;
        public int PostalCode
        {
            get => _postalcode;

        }

        readonly string _taxRate;
        public string TaxRate
        {
            get => _taxRate;

        }

        readonly string _formOfEmployment;
        public string FormOfEmployment
        {
            get => _formOfEmployment;

        }

        readonly SalesMen _salesMen;
        public SalesMen SaleMen
        {
            get => _salesMen;

        }
        readonly string _username;
        public string Username
        {
            get => _username;
        }

        readonly string _password;
        public string Password
        {
            get => _password;
        }

        private object _selectedDataGridRow;
        public object SelectedDataGridRow
        {
            get => _selectedDataGridRow;
            set
            {
                if (_selectedDataGridRow != value)
                {
                    _selectedDataGridRow = value;
                    OnPropertyChanged("SelectedDataGridCell");
                }
            }
        }

        private string _textBoxContent;
        public string TextBoxContent
        {
            get => _textBoxContent;
            set
            {
                if (_textBoxContent != value)
                {
                    _textBoxContent = value;
                    OnPropertyChanged("TextBoxContent");
                }
            }
        }
        #endregion
    }
}
