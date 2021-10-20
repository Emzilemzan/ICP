using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// ViewModel for Life Application adding.
    /// </summary>
    public class RegisterLifeApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterLifeApplicationViewModel Instance = new RegisterLifeApplicationViewModel();
        public RegisterLifeApplicationViewModel()
        {
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            BaseAmounts = UpdateBaseAmount();
            DeliveryDate = DateTime.Today;
            LifeInsuranceTypes = UpdateLife();
        }
        #region methods

        public void EmptyAllChoices()
        {
            Check = true;
            Instance.BAmount = null;
            Instance.AgentNo = null;
            Instance.City = string.Empty;
            Instance.StreetAddress = string.Empty;
            Instance.TelephoneNbrWork = string.Empty;
            Instance.TelephoneNbrHome = string.Empty;
            Instance.DiallingCodeHome = string.Empty;
            Instance.DiallingCodeWork = string.Empty;
            Instance.EmailOne = string.Empty;
            Instance.EmailTwo = string.Empty;
            Instance.SocialSecurityNumber = string.Empty;
            Instance.SocialSecurityNumberIP = string.Empty;
            Instance.LType = null;
            Instance.DeliveryDate = Today;
            Instance.LastName = string.Empty;
            Instance.Lastname = string.Empty;
            Instance.FirstName = string.Empty;
            Instance.Firstname = string.Empty;
            Instance.PaymentForm = null;
            Instance.PostalCode = string.Empty;
        }

        private void AddInsurance()
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

            Insurance i = new Insurance()
            {
                SerialNumber = Instance.SerialNumber = GenerateIdFormation(),
                PersonTaker = x,
                TakerNbr = x.SocialSecurityNumber,
                TypeName = Instance.LType.LifeName,
                PaymentForm = Instance.PaymentForm,
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = Instance.DeliveryDate,
                AgentNo = Instance.AgentNo,
                InsuredID = insured,
                LIFE = Instance.LType,
                BaseAmountValue = Instance.BAmount.Baseamount,
                AckValue4 = Instance.AckValue4 = Context.BDController.CountAckvalueLife(Instance.DeliveryDate, Instance.LType, Instance.BAmount.Baseamount),
            };

            Context.IController.AddInsuranceApplication(i);
            MessageBox.Show("Ansökan har lagts till");
            SignedInsuranceViewModel.Instance.UpdateAC();
            EmptyAllChoices();
            Context.Save();
            SignedInsuranceViewModel.Instance.UpdateAC();
        }

        private InsuredPerson AddInsuredIT(Person p)
        {
            InsuredPerson newInp = new InsuredPerson()
            {
                FirstName = Instance.FirstName = p.Firstname,
                LastName = Instance.LastName = p.Lastname,
                SocialSecurityNumberIP = Instance.SocialSecurityNumberIP = p.SocialSecurityNumber,
                PersonType = PersonTypes[0],
                PersonTaker = p,
            };

            Context.IPController.AddInsuredPerson(newInp);
            InsuredPerson = newInp;
            return InsuredPerson;
        }

        readonly List<string> PersonTypes = new List<string>() { "Vuxen" };

        private void BoxesCheckInsurance()
        {
            if (Instance.SocialSecurityNumber != null && Instance.City != null && Instance.Firstname != null && Instance.Lastname != null && Instance.PostalCode != null && Instance.EmailOne != null && Instance.StreetAddress != null
              && Instance.DiallingCodeHome != null && Instance.TelephoneNbrHome != null
              && Instance.PaymentForm != null && Instance.DeliveryDate != null && Instance.DeliveryDate != null && Instance.LType != null
              && Instance.AgentNo != null)
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

        private InsuredPerson AddInsured(Person p)
        {
            InsuredPerson newInp = new InsuredPerson()
            {
                FirstName = Instance.FirstName,
                LastName = Instance.LastName,
                SocialSecurityNumberIP = Instance.SocialSecurityNumberIP= Instance.SocialSecurityNumberIP,
                PersonType = "Vuxen",
                PersonTaker = p,
            };
            Context.IPController.AddInsuredPerson(newInp);
            InsuredPerson = newInp;
            return InsuredPerson;
        }
        private bool CanCreate() => true;

        private ICommand _addInsuranceBtn;
        public ICommand AddInsuranceBtn
        {
            get => _addInsuranceBtn ?? (_addInsuranceBtn = new RelayCommand(x => { RegisterApplication(); CanCreate(); }));
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
                if (i.LIFE != null)
                {
                    if (LType.LifeName.Equals(i.LIFE.LifeName))
                    {
                        insurances?.Add(i);
                    }
                }
            }
            if (insurances.Count < 1)
            {
                string str = "LIV";
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


        #region updating of lists

        //Update all baseamounts for a specific optionalinsurances and for the delivery year.  
        public ICollection<BaseAmount> UpdateBaseAmount()
        {
            List<BaseAmount> x = new List<BaseAmount>();

            foreach (var e in Context.BDController.GetAllBaseAmount())
            {
                if (Today.Year.Equals(e.Date.Year))
                {
                    x?.Add(e);
                }
            }
            BaseAmounts = x;
            return BaseAmounts;
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
        //Get alla SAInsurances. 
        public ObservableCollection<LifeInsurance> UpdateLife()
        {
            ObservableCollection<LifeInsurance> x = new ObservableCollection<LifeInsurance>();

            foreach (var e in Context.IController.GetAllLIFE())
            {
                x?.Add(e);
            }

            LifeInsuranceTypes = x;

            return LifeInsuranceTypes;
        }
        #endregion
        #region lists
        public ICollection<BaseAmount> BaseAmounts { get; set; }
        public ObservableCollection<LifeInsurance> LifeInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        #endregion

        #region properties for person

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
                _pC = 0;
                if (int.TryParse(value, out _pC) && PostalCode.Length > 0 && PostalCode.Length < 6)
                {
                    
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara fem siffror");
                }
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
        #region properties for insured person
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
        #region properties for insurance

        private double _av4;
        public double AckValue4
        {
            get => _av4;
            set
            {
                _av4 = value;
                OnPropertyChanged("AckValue4");
            }
        }

        private LifeInsurance _Ltype;
        //The selection of _Ltype is responsible for populating the BaseAmounts collection based on their date.
        public LifeInsurance LType
        {
            get => _Ltype;
            set
            {
                _Ltype = value;
                OnPropertyChanged("LType");
                if (Check == false)
                {
                    List<BaseAmount> Bases = new List<BaseAmount>();
                    foreach (var e in this.BaseAmounts = _Ltype.Amounts)
                    {
                        if (!Today.Year.Equals(e.Date.Year))
                            Bases.Add(e);
                    }
                    foreach (var f in Bases)
                    {
                        BaseAmounts.Remove(f);
                    }
                    OnPropertyChanged("BaseAmounts");
                }
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
        private BaseAmount _bAmount;
        public BaseAmount BAmount
        {
            get => _bAmount;
            set
            {
                _bAmount = value;
                OnPropertyChanged("BAmount");
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
        public DateTime Today => DateTime.Today.Date;
        private bool _check;
        public bool Check
        {
            get => _check;
            private set
            {
                _check = value;
            }
        }

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
