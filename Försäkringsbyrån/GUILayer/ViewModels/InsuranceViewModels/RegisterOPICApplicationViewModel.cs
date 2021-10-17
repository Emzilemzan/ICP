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
    /// Register Application for OtherPerson Insurance Company edition. 
    /// </summary>
   public class RegisterOPICApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterOPICApplicationViewModel Instance = new RegisterOPICApplicationViewModel();

        public RegisterOPICApplicationViewModel()
        {
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            DeliveryDate = DateTime.Today;
            OPInsuranceTypes = UpdateOPI();
        }

        #region Commands and Methods
        private bool CanCreate() => true;

        private ICommand _addCIInsuranceBtn;
        public ICommand AddCIInsuranceBtn
        {
            get => _addCIInsuranceBtn ?? (_addCIInsuranceBtn = new RelayCommand(x => { RegisterApplication(); _ = CanCreate(); }));
        }

        public void AddInsurance()
        {
            if (Instance._orgNr != null && Instance.ContactPerson != null && Instance.CompanyName != null && Instance.City != null && Instance.AgentNo != null & Instance.DialingCode != null
                && Instance.PaymentForm != null && Instance.StreetAdress != null && Instance.DeliveryDate != null && Instance.PostalCode != null && Instance.Premie != null && Instance.Tabell !=null 
                && Instance.FaxNbr != null && Instance.TelephoneNbr !=null)
            {
                Insurance i = new Insurance()
                {
                    SerialNumber = Instance.SerialNbr = GenerateIdFormation(),
                    AgentNo = Instance.AgentNo,
                    TakerNbr = Instance.OrganizationNbr,
                    TypeName = Instance.OPIType.OPIName,
                    OPI = Instance.OPIType,
                    PaymentForm = Instance.PaymentForm,
                    CompanyTaker = Instance.Companyn = AddCompany(),
                    DeliveryDate = Instance.DeliveryDate,
                    Premie = Instance._premie,
                    Table = Instance.Tabell,
                    InsuranceStatus = Status.Otecknad,

                };
                Context.IController.AddInsuranceApplication(i);
                MessageBox.Show("Ansökan har lagts till");
                Check = true;
                Instance.OrganizationNbr = string.Empty;
                Instance.AgentNo = null;
                Instance.City = string.Empty;
                Instance.StreetAdress = string.Empty;
                Instance.TelephoneNbr = string.Empty;
                Instance.DialingCode = string.Empty;
                Instance.Email = string.Empty;
                Instance.CompanyName = string.Empty;
                Instance.Premie = string.Empty;
                Instance.PaymentForm = null;
                Instance.PostalCode = string.Empty;
                Instance.FaxNbr = string.Empty;
                Instance.Companyn = null;
                Instance.CompanyInsuranceType = null;
            }
            else
            {
                MessageBox.Show("Alla fält med en * är obligatoriska att fylla i", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void RegisterApplication()
        {
            Company y = Context.ITController.GetCompany(Instance.OrganizationNbr);
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
                OrganizationNumber = Instance.OrganizationNbr,
                PostalCode = Instance._pC,
                StreetAddress = Instance.StreetAdress,
                City = Instance.City,
                CompanyName = Instance.CompanyName,
                DiallingCode = Instance.DialingCode,
                Email = Instance.Email,
                ContactPerson = Instance.ContactPerson,
                FaxNumber = Instance.FaxNbr,
                TelephoneNbr = Instance.TelephoneNbr,
            };
            Context.ITController.CheckExistingCompany(Instance._orgNr, _tk, Instance.CompanyName, Instance.City, Instance._pC, Instance.StreetAdress, Instance.TelephoneNbr, Instance.DialingCode, Instance.Email, Instance.ContactPerson, Instance.FaxNbr);
            Company x = Context.ITController.GetCompany(Instance._orgNr);
            Companyn = x;
            return Companyn;
        }
        #endregion

        #region Updates 
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
        public ObservableCollection<OtherPersonInsurance> UpdateOPI()
        {
            ObservableCollection<OtherPersonInsurance> x = new ObservableCollection<OtherPersonInsurance>();

            x.Add(new OtherPersonInsurance() { OPIId = 0, OPIName = "Inget" });
            foreach (var e in Context.IController.GetAllOPI())
            {
                x?.Add(e);
            }

            OPInsuranceTypes = x;

            return OPInsuranceTypes;
        }



        private string GenerateIdFormation()
        {
            string y;
            List<Insurance> insurances = new List<Insurance>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.OPI != null)
                {
                    if (OPIType.OPIName.Equals(i.OPI.OPIName))
                    {
                        insurances?.Add(i);
                    }
                }
            }
            if (insurances == null)
            {
                string str = "ÖPFV";
                string num = "1";

                y = str + num;
            }
            else
            {
                string x = insurances.Last().SerialNumber;

                string str = Regex.Replace(x, @"\d", "");
                string num = Regex.Replace(x, @"\D", "");

                int num1 = int.Parse(num);
                int num2 = num1 + 1;
                string newNum = num2.ToString();

                y = str + newNum;
            }
            return y;
        }

        #endregion

        #region Lists
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ObservableCollection<OtherPersonInsurance> OPInsuranceTypes { get; set; }

        #endregion

        #region Properties for Companies
        private string _orgNr;
        public string OrganizationNbr
        {
            get => _orgNr;
            set
            {
                _orgNr = value;
                OnPropertyChanged("OrganizationNbr");
            }
        }
        private string _CompName;
        public string CompanyName
        {
            get => _CompName;
            set
            {
                _CompName = value;
                OnPropertyChanged("CompanyName");
            }
        }

        private string _adress;
        public string StreetAdress
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged("StreetAdress");
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
        private string _emial;
        public string Email
        {
            get => _emial;
            set
            {
                _emial = value;
                OnPropertyChanged("Email");
            }
        }

        private string _faxNbr;
        public string FaxNbr
        {
            get => _faxNbr;
            set
            {
                _faxNbr = value;
                OnPropertyChanged("FaxNbr");
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

        private string _telNbr;
        public string TelephoneNbr
        {
            get => _telNbr;
            set
            {
                _telNbr = value;
                OnPropertyChanged("TelephoneNbr");
            }
        }

        private string _dialingCode;
        public string DialingCode
        {
            get => _dialingCode;
            set
            {
                _dialingCode = value;
                OnPropertyChanged("DialingCode");
            }
        }

        private OtherPersonInsurance _Opip;
        public OtherPersonInsurance OPIType
        {
            get => _Opip;
            set
            {
                _Opip = value;
                OnPropertyChanged("OPIType");
            }
        }

        #endregion

        #region Properties for Insurance
        private string _serialNbr; 
        public string SerialNbr
        {
            get => _serialNbr;
            set
            {
                _serialNbr = value;
                OnPropertyChanged("SerialNbr");
            }
        }

        private SalesMen _an;
        public SalesMen AgentNo
        {
            get => _an;
            set
            {
                _an = value;
                OnPropertyChanged("AgentNo");
            }
        }

        private InsuredPerson _iP;
        public InsuredPerson InsuredPerson
        {
            get => _iP;
            set
            {
                _iP = value;
                OnPropertyChanged("InsuredPerson");
            }
        }

        private Company _company;
        public Company Companyn
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged("Companyn");
            }
        }

        private Status _status;
        public Status InsuranceStatus
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("InsuranceStatus");
            }
        }

        private string _payMent;
        public string PaymentForm
        {
            get => _payMent;
            set
            {
                _payMent = value;
                OnPropertyChanged("PaymentForm");
            }
        }

        private int _premie;
        public string Premie
        {
            get => _premie > 0 ? _premie.ToString() : "";
            set
            {
                if (Check == false)
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

        private string _tabel;
        public string Tabell
        {
            get => _tabel;
            set
            {
                _tabel = value;
                OnPropertyChanged("Tabell");
            }
        }

        private DateTime _dd;
        public DateTime DeliveryDate
        {
            get => _dd != null ? _dd : DateTime.Now;
            set
            {
                _dd = value;
                OnPropertyChanged("DeliveryDate");
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

        #endregion

        #region Insured P 
        private int _insuredId;
        public int InsuredID
        {
            get => _insuredId;
            set
            {
                _insuredId = value;
                OnPropertyChanged("InsuredID");
            }
        }

        private string _personType;
        public string PersonType
        {
            get => _personType;
            set
            {
                _personType = value;
                OnPropertyChanged("PersonType");
            }
        }

        private string _sSNIP;
        public string SocialSecurityNumberIP
        {
            get => _sSNIP;
            set
            {
                _sSNIP = value;
                OnPropertyChanged("SocialSecurityNumberIP");
            }
        }
        private string _lName;
        public string LastName
        {
            get => _lName;
            set
            {
                _lName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string _fName;
        public string FirstName
        {
            get => _fName;
            set
            {
                _fName = value;
                OnPropertyChanged("FirstName");
            }
        }
        #endregion

        #region Bools 
        private bool _check;
        public bool Check
        {
            get => _check;
            private set
            {
                _check = value;
            }
        }

        public DateTime Today => DateTime.Today.Date;
        #endregion

    }
}
