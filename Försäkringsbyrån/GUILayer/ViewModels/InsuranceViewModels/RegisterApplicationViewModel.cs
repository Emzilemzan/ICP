using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            SalesMens = UpdateSM();
            CInsuranceTypes = new List<string>() { "Kombinerad företagsförsäkring", "Fastighet" };
            CompanyInsurances = UpdateCompanyInsurances();
            StartDate = DateTime.Today;
        }

        #region commands and methods
        private ICommand _registerApplication;
        public ICommand AddCompanyApplication
        {
            get => _registerApplication ?? (_registerApplication = new RelayCommand(x => { RegisterCompanyApplication(); CanAddCompanyApplication(); }));
        }

        //Get all salesmen. 
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

        public ObservableCollection<CompanyInsurance> CompanyInsurances { get; set; }
        public ObservableCollection<CompanyInsurance> UpdateCompanyInsurances()
        {
            ObservableCollection<CompanyInsurance> x = new ObservableCollection<CompanyInsurance>();
            foreach (var c in Context.IController.GetAllCAI())
            {
                x?.Add(c);
            }
            CompanyInsurances = x;
            return CompanyInsurances;
        }

        private void AddInsurance()
        {
            if (Instance._orgNbr != null && Instance.ContactPerson != null && Instance.CompanyName != null && Instance.City != null && Instance.AgentNo != null & Instance.DiallingCode != null
                 && Instance.CompanyInsuranceType != null && Instance.EndDate != null && Instance.StartDate != null && Instance.PaymentForm != null && Instance.StreetAddress != null &&
                 Instance.PostalCode != null && Instance.Premie != null)
            {
                Insurance i = new Insurance()
                {
                    SerialNumber = Instance.SerialNumber = GenerateIdFormation(),
                    AgentNo = Instance.AgentNo,
                    COI = Instance.CompanyI,
                    PaymentForm = Instance.PaymentForm,
                    InsuranceCompany = Instance.InsuranceCompany,
                    CompanyTaker = Instance.Company = AddCompany(),
                    EndDate = Instance.EndDate,
                    StartDate = Instance.StartDate,
                    Notes = Instance.Notes,
                    Premie = Instance._premie,
                    InsuranceStatus = Status.Otecknad,
                    CompanyInsuranceType = Instance.CompanyInsuranceType,
                };
                Context.IController.AddInsuranceApplication(i);
                MessageBox.Show("Ansökan har lagts till");
                Check = true;
                Instance.OrganizationNumber = string.Empty;
                Instance.AgentNo = null;
                Instance.City = string.Empty;
                Instance.StreetAddress = string.Empty;
                Instance.TelephoneNbr = string.Empty;
                Instance.DiallingCode = string.Empty;
                Instance.Email = string.Empty;
                Instance.EndDate = Today;
                Instance.StartDate = Today;
                Instance.CompanyName = string.Empty;
                Instance.Premie = string.Empty;
                Instance.PaymentForm = null;
                Instance.PostalCode = string.Empty;
                Instance.Notes = string.Empty;
                Instance.FaxNumber = string.Empty;
                Instance.InsuranceCompany = string.Empty;
                Instance.CompanyI = null;
                Instance.Company = null;
                Instance.CompanyInsuranceType = null;
            }
            else
            {
                MessageBox.Show("Alla fält med en * är obligatoriska att fylla i", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanAddCompanyApplication() => true;
        private void RegisterCompanyApplication()
        {
            Company y = Context.ITController.GetCompany(Instance.OrganizationNumber);
            if (y != null)
            {
                MessageBoxResult result = MessageBox.Show($"Det finns redan en försäkringstagare med det inskrivna organisationsnumret vid namn: {y.CompanyName} vill du uppdatera dessa uppgifter?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    AddInsurance();                
                }
                else
                {
                    MessageBox.Show("Ändra organisationsnummer.");
                }
            }
            else
            {
                AddInsurance();
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
        /// <summary>
        /// method for autogenerate alphanumeric serialnumber
        /// </summary>
        /// <returns></returns>
        private string GenerateIdFormation()
        {
            string y;
            List<Insurance> insurances = new List<Insurance>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.COI == Instance.CompanyI)
                {
                    insurances?.Add(i);
                }
            }
            if (insurances.Count < 1)
            {
                string str = "FF";
                string num = "1";

                y = str + num;
            }
            else
            {
                string x = insurances.Last().SerialNumber;

                string str = Regex.Replace(x, @"\d", "");
                string num = Regex.Replace(x, @"\D", "");

                int num1 = int.Parse(num);
                int num2 = num1++;
                string newNum = num2.ToString();

                y = str + newNum;
            }
            return y;
        }

        #endregion
        #region properties

        public List<string> PayMentForms { get; set; }
        public List<string> CInsuranceTypes { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }

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
                if(Check == false)
                {
                    if (int.TryParse(value, out _pC) && PostalCode.Length == 5)
                    {
                        OnPropertyChanged("PostalCode");
                    }
                    else
                    {
                        MessageBox.Show("Måste vara fem siffror");
                    }
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

        private CompanyInsurance _companyi;
        public CompanyInsurance CompanyI
        {
            get => _companyi;
            set
            {
                _companyi = value;
                OnPropertyChanged("CompanyI");
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

        private int _premie;
        public string Premie
        {
            get => _premie > 0 ? _premie.ToString() : "";
            set
            {
                if(Check == false)
                {
                    if (int.TryParse(value, out _premie) && _premie.ToString().Length == 5)
                    {
                        OnPropertyChanged("Premie");
                    }
                    else 
                    {
                        MessageBox.Show("Måste vara ett tal");
                    }
                }
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

        private string _serialNumber;
        public string SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        private string _CIT;
        public string CompanyInsuranceType
        {
            get => _CIT;
            set
            {
                _CIT = value;
                OnPropertyChanged("CompanyInsuranceType");
            }
        }
        private string _pF;
        public string PaymentForm 
        { 
            get => _pF; 
            set
            {
                _pF = value;
                OnPropertyChanged("PaymentForm");
            }
        }
        private DateTime _st;
        public DateTime StartDate
        {
            get => _st;
            set
            {
                _st = value;
                OnPropertyChanged("StartDate");
            }
        }
        private DateTime _et;
        public DateTime EndDate
        {
            get => _et;
            set
            {
                _et = value;
                OnPropertyChanged("EndDate");
            }
        }

        private string _IC;
        public string InsuranceCompany 
        {
            get => _IC; 
            set
            {
                _IC = value;
                OnPropertyChanged("InsuranceCompany");
            }
        }

        public DateTime Today => DateTime.Today.Date;

        #endregion

    }
    
}






