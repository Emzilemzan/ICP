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

/// <summary>
/// Register application for the other person insurances. 
/// </summary>

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class RegisterOPIPApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterOPIPApplicationViewModel Instance = new RegisterOPIPApplicationViewModel();
        public RegisterOPIPApplicationViewModel()
        {
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            DeliveryDate = DateTime.Today;
            OPInsuranceTypes = UpdateOPI();
        }

        #region Commands and methods
        private bool CanCreate() => true;

        private ICommand _addInsuranceBtn;
        public ICommand AddInsuranceBtn
        {
            get => _addInsuranceBtn ?? (_addInsuranceBtn = new RelayCommand(x => { RegisterApplication(); _ = CanCreate(); }));
        }

        public void EmptyAllChoices()
        {
            Check = true;
            Instance.AgentNo = null;
            Instance.City = string.Empty;
            Instance.StreetAddress = string.Empty;
            Instance.TelephoneNbrHome = string.Empty;
            Instance.TelephoneNbrWork = string.Empty;
            Instance.DiallingCodeHome = string.Empty;
            Instance.DiallingCodeWork = string.Empty;
            Instance.EmailOne = string.Empty;
            Instance.EmailTwo = string.Empty;
            Instance.LastName = string.Empty;
            Instance.Lastname = string.Empty;
            Instance.FirstName = string.Empty;
            Instance.Firstname = string.Empty;
            Instance.PaymentForm = string.Empty;
            Instance.OPIType = null;
            Instance.SocialSecurityNumber = string.Empty;
            Instance.SocialSecurityNumberIP = string.Empty;
            Instance.DeliveryDate = Today;
            Instance.PostalCode = string.Empty;
            Instance.Premie = string.Empty;
            Instance.Tabell = string.Empty;
        }

        private void BoxesCheckInsurance()
        {
            if (Instance.SocialSecurityNumber != null && Instance.City != null && Instance.Firstname != null && Instance.Lastname != null && Instance.PostalCode != null && Instance.EmailOne != null && Instance.StreetAddress != null
          && Instance.DiallingCodeHome != null && Instance.TelephoneNbrHome != null && Instance.LastName != null && Instance.FirstName != null && Instance.SocialSecurityNumberIP != null && Instance.PaymentForm != null && Instance.DeliveryDate != null && Instance._premie != 0 && Instance.Tabell != null)
            {
                if (IPISPerson == false)
                {
                    AddInsurance();
                }
                else
                {
                    if (Instance.LastName != null && Instance.FirstName != null && Instance.SocialSecurityNumberIP != null)
                        AddInsurance();
                }
            }
            else
            {
                MessageBox.Show("Alla fält med en stjärna är obligatoriska!");
            }
        }

        public void AddInsurance()
        {
            Person x = Instance.Personen = AddInsuranceTaker();
            InsuredPerson insured;
            if (IPISPerson == false)
            {
                insured = Instance.InsuredPerson = AddInsuredIT(x);
            }
            else
            {
                insured = Instance.InsuredPerson = AddInsured(x);
            }
            Insurance op = new Insurance()
            {
                SerialNumber = Instance.SerialNumber = GenerateIdFormation(),
                PersonTaker = x,
                TakerNbr = x.SocialSecurityNumber,
                TypeName = Instance.OPIType.OPIName,
                PaymentForm = Instance.PaymentForm,
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = Instance.DeliveryDate,
                AgentNo = Instance.AgentNo,
                InsuredID = insured,
                Table = Instance.Tabell,
                Premie = Instance._premie,
                OPI = Instance.OPIType,
            };

            Context.IController.AddInsuranceApplication(op);
            MessageBox.Show("Ansökan tillagd!");
            EmptyAllChoices();
            Context.Save();
            SignedInsuranceViewModel.Instance.UpdateAC();
        }
        private Person AddInsuranceTaker()
        {
            Person newP = new Person()
            {
                SocialSecurityNumber = Instance.SocialSecurityNumber,
                City = Instance.City,
                Firstname = Instance.Firstname,
                Lastname = Instance.Lastname,
                PostalCode = Instance._pC,
                EmailOne = Instance.EmailOne,
                EmailTwo = Instance.EmailTwo,
                StreetAddress = Instance.StreetAddress,
                DiallingCodeHome = Instance.DiallingCodeHome,
                TelephoneNbrHome = Instance.TelephoneNbrHome,
                DiallingCodeWork = Instance.DiallingCodeWork,
                TelephoneNbrWork = Instance.TelephoneNbrWork,
            };
            Context.ITController.CheckExistingPerson(Instance._sSN, newP, Instance.Firstname, Instance.Lastname, Instance.City, Instance._pC, Instance.StreetAddress, Instance.TelephoneNbrHome, Instance.TelephoneNbrWork, Instance.DiallingCodeHome, Instance.DiallingCodeWork, Instance.EmailOne, Instance.EmailTwo);
            Person x = Context.ITController.GetPerson(Instance._sSN);
            Personen = x;
            return Personen;
        }
        private InsuredPerson AddInsuredIT(Person p)
        {
            InsuredPerson newInp = new InsuredPerson()
            {
                FirstName = Instance.FirstName = p.Firstname,
                LastName = Instance.LastName = p.Lastname,
                SocialSecurityNumber = p.SocialSecurityNumber,
                PersonType = PersonTypes[0],
                PersonTaker = p,
            };

            Context.IPController.AddInsuredPerson(newInp);
            InsuredPerson = newInp;
            return InsuredPerson;
        }
        readonly List<string> PersonTypes = new List<string>() { "Vuxen" };

        private InsuredPerson AddInsured(Person p)
        {
            List<InsuredPerson> tempList = new List<InsuredPerson>();
            InsuredPerson newInp = new InsuredPerson()
            {
                InsuredId = Instance.InsuredID,
                FirstName = Instance.FirstName,
                LastName = Instance.LastName,
                SocialSecurityNumber = Instance.SocialSecurityNumberIP,
                PersonType = "Vuxen",
                PersonTaker = p,
            };
            tempList.Add(newInp);
            Context.IPController.AddInsuredPerson(newInp);

            return InsuredPerson;
        }



        private void RegisterApplication()
        {
            Person y = Context.ITController.GetPerson(Instance.SocialSecurityNumber);
            if (y != null)
            {
                MessageBoxResult result = MessageBox.Show($"Det finns redan en försäkringstagare med det inskrivna personnumret vid namn: {y.Firstname} {y.Lastname} vill du uppdatera dessa uppgifter?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    BoxesCheckInsurance();
                }
            }
            else
            {
                BoxesCheckInsurance();
            }
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
            if (insurances.Count < 1)
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

        #region update of collections and lists. 
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

        // Get all OtherPerions insurances
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

        #endregion

        #region lists
        public ObservableCollection<OtherPersonInsurance> OPInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }

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
        #region Properites for person 
        private string _sSN;
        public string SocialSecurityNumber
        {
            get => _sSN;
            set
            {
                _sSN = value;
                OnPropertyChanged("SocialSecurityNumber");
            }
        }
        private string _lname;
        public string Lastname
        {
            get => _lname;
            set
            {
                _lname = value;
                OnPropertyChanged("Lastname");
            }
        }

        private string _fname;
        public string Firstname
        {
            get => _fname;
            set
            {
                _fname = value;
                OnPropertyChanged("Firstname");
            }
        }
        private string _streetA;
        public string StreetAddress
        {
            get => _streetA;
            set
            {
                _streetA = value;
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
        private string _dCH;
        public string DiallingCodeHome
        {
            get => _dCH;
            set
            {
                _dCH = value;
                OnPropertyChanged("DiallingCodeHome");
            }
        }
        private string _dCW;
        public string DiallingCodeWork
        {
            get => _dCW;
            set
            {
                _dCW = value;
                OnPropertyChanged("DiallingCodeWork");
            }
        }
        private string _tNH;
        public string TelephoneNbrHome
        {
            get => _tNH;
            set
            {
                _tNH = value;
                OnPropertyChanged("TelephoneNbrHome");
            }
        }
        private string _tNW;
        public string TelephoneNbrWork
        {
            get => _tNW;
            set
            {
                _tNW = value;
                OnPropertyChanged("TelephoneNbrWork");
            }
        }
        private string _emailOne;
        public string EmailOne
        {
            get => _emailOne;
            set
            {
                _emailOne = value;
                OnPropertyChanged("EmailOne");
            }
        }
        private string _emailTwo;
        public string EmailTwo
        {
            get => _emailTwo;
            set
            {
                _emailTwo = value;
                OnPropertyChanged("EmailTwo");
            }
        }

        #endregion
        #region Properties for Insured Person
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
        #region Properties for Insurance

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
        private Person _person;
        public Person Personen
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged("Personen");
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
                if (int.TryParse(value, out _premie) && _premie.ToString().Length == 5)
                {
                    OnPropertyChanged("Premie");
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara ett tal");
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

        #endregion

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

        private bool _IPISPerson;
        public bool IPISPerson
        {
            get => _IPISPerson;
            set
            {
                _IPISPerson = value;
                OnPropertyChanged("IPISPerson");
            }
        }
    }
}

