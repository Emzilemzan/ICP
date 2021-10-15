using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        }

        #region
        private ICommand _registerApplication;
        public ICommand AddCompanyApplication
        {
            get => _registerApplication ?? (_registerApplication = new RelayCommand(x => { RegisterCompanyApplication(); CanAddCompanyApplication(); }));
        }

        #endregion


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

        private bool CanAddCompanyApplication() => true;
        private void RegisterCompanyApplication()
        {
            Insurance i = new Insurance();

            if (Instance._orgNbr != null && Instance.ContactPerson != null && Instance.CompanyName != null && Instance.City != null && Instance.AgentNo != null & Instance.DiallingCode != null
                && Instance.Email != null && Instance.FaxNumber != null)
            {

                // add insurancetaker (instance?) gör något som bara skirver ut rå datan i excel och sen laborera i excel och för över det till koden.   
            }
            else
            {
                MessageBox.Show("Alla fält med en * är obligatoriska att fylla i", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private Company AddCompany()
        {
            Company _tk = new Company()
            {
                OrganizationNumber = Instance.OrganizationNumber,
                PostalCode = Instance._pC,
                StreetAddress = Instance.StreetAddress,
                City = Instance.City,
                CompanyName = Instance.CompanyName,
                DiallingCode = Instance.DiallingCode,
                Email = Instance.Email,
                ContactPerson = Instance.ContactPerson,
                FaxNumber = Instance.FaxNumber,
                TelephoneNbr = Instance.TelephoneNbr,
            };
            Context.ITController.CheckExistingCompany(Instance._orgNbr, _tk, Instance.CompanyName, Instance.City, Instance._pC, Instance.StreetAddress, Instance.TelephoneNbr, Instance.DiallingCode, Instance.Email, Instance.ContactPerson, Instance.FaxNumber);
            Company x = Context.ITController.GetCompany(Instance._orgNbr);
            Company = x;
            return Company;
        }

        #region properties

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
            get => _streetAdress;
            set
            {
                _streetAdress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        private int _pC;
        public string PostalCode
        {
            get => _pC > 0 ? _pC.ToString() : "";
            set
            {
                if (int.TryParse(value, out _pC) && PostalCode.Length == 5)
                {
                    OnPropertyChanged("PostalCode");
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara fem siffror");
                }
            }
        }

        private bool _check;
        public bool Check
        {
            get => _check;
            set
            {
                _check = value;
                OnPropertyChanged("Check");
            }
        }


        private string _dc;
        public string DiallingCode
        {
            get => _dc;
            set
            {
                _dc = value;
                OnPropertyChanged("DiallingCode");
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


        private Company _company;
        public Company Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }
        #endregion

        #region Insurance properties

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
        public string CompanyInsuranceType { get; set; }
        public string PaymentForm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string InsuranceCompany { get; set; }

        #endregion

    }
    
}






