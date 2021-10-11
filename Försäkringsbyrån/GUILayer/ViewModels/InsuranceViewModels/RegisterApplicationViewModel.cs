using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUILayer.Commands;
using Models.Models;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// FÖRETAGSFÖRSÄKRING!!!! REGISTRERA FÖRETAGSFÖRSÄKRING!!!!!!!
    /// </summary>
    public class RegisterApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterApplicationViewModel Instance = new RegisterApplicationViewModel();

        public RegisterApplicationViewModel()
        {
            AddCompanyApplication = new RelayCommand(RegisterCompanyApplication, CanAddCompanyApplication);
        }

        public ObservableCollection<Company> Companies { get; set; }

        private string _searchFrase;
        public string SearchFrase
        {
            get => _searchFrase;
            set
            {
                _searchFrase = value;
                OnPropertyChanged("SearchFrase");
            }
        }
        public ObservableCollection<Company> UpdateCompanies()
        {
            ObservableCollection<Company> x = new ObservableCollection<Company>();
            foreach (var c in Context.ITController.GetAllCompanies())
            {
                x?.Add(c);
            }
            Companies = x;
            return Companies;
        }

        public RelayCommand AddCompanyApplication { get; set; }

        public bool CanAddCompanyApplication(object parameter)
        {
            return true;
        }
        public void RegisterCompanyApplication(object parameter)
        {
            Insurance i = new Insurance();

            if (Instance._orgNbr == null)
            {
                Company _tk = new Company();
                {
                    OrganizationNumber = Instance.OrganizationNumber;
                PostalCode = Instance.PostalCode;
                StreetAddress = Instance.StreetAddress;
                City = Instance.City;
                CompanyName = Instance.CompanyName;
                ContactPerson = Instance.ContactPerson;
                FaxNumber = Instance.FaxNumber;
                TelephoneNbr = Instance.TelephoneNbr;

                    // add insurancetaker (instance?) gör något som bara skirver ut rå datan i excel och sen laborera i excel och för över det till koden. 
            };     
            }
        } 

        //  if (Instance._username != null)
        //    {
        //        UserAccess a = new UserAccess()
        //        {
        //            Username = Instance.Username,
        //            Password = Instance.Password,
        //            Search = Instance.Search,
        //            StatisticsAndProspects = Instance.StatisticsAndProspects,
        //            Insurances = Instance.Insurances,
        //            EmployeeManagement = Instance.EmployeeManagement,
        //            Commission = Instance.Commission,
        //            BasicData = Instance.BasicData
        //        };
        //Context.UAController.CheckExistingUser(Instance._username, a);
        //        MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
        //        MainViewModel.Instance.CurrentTool = "";

        #region properties
        private int _serialNumber;
        public int SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        private string _payDate;
        public string PayDate
        {
            get => _payDate;
            set
            {
                _payDate = value;
                OnPropertyChanged("PayDate");
            }
        }
        private SalesMen _agentNo;
        public SalesMen AgentNo
        {
            get => _agentNo;
            set
            {
                _agentNo = value;
                OnPropertyChanged("AgentNo");
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged("CompanyName");
            }
        }

        private string _orgNbr;
        public string OrganizationNumber
        {
            get => _orgNbr;
            set
            {
                _orgNbr = value;
                OnPropertyChanged("OrganizationNumber");
            }
        }

        private string _streetAdress;
        public string StreetAddress
        {
          get =>  _streetAdress;
            set
            {
                _streetAdress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        private int _postalCode;
        public int PostalCode
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

        private string _tel;
        public string TelephoneNbr
        {
            get => _tel;
            set
            {
                _tel = value;
                OnPropertyChanged("TelephoneNbr");
            }
        }
        private string _fax;
        public string FaxNumber
        {
            get => _fax;
            set
            {
                _fax = value;
                OnPropertyChanged("FaxNumber");
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get => _contactPerson;
            set
            {
                _contactPerson = value;
                OnPropertyChanged("ContactPerson");
            }
        }

        private string _insuredName;
        public string InsuredName
        {
            get => _insuredName;
            set
            {
                _insuredName = value;
                OnPropertyChanged("InsuredName");
            }
        }

        private string _insuredLastName;
        public string InsuredLastName
        {
            get => _insuredLastName;
            set
            {
                _insuredLastName = value;
                OnPropertyChanged("InsuredLastName");
            }
        }

        private string _insuredssNbr;
        public string InsuredSocialSecurityNbr
        {
            get => _insuredssNbr;
            set
            {
                _insuredssNbr = value;
                OnPropertyChanged("InsuredSocialSecurityNbr");
            }
        }

        private int _premie;
        public int Premie
        {
            get => _premie;
            set
            {
                _premie = value;
                OnPropertyChanged("Premie");
            }
        }

        private string _note;
        public string Notes
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged("Notes");
            }
        }
    }


    #endregion
}






