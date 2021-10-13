using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class RegisterPersonApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterPersonApplicationViewModel Instance = new RegisterPersonApplicationViewModel();

        public RegisterPersonApplicationViewModel()
        {
            SalesMens = UpdateSM();
            PersonTypes = new List<string>() { "Vuxen", "Barn" };
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OptionalTypes = UpdateOptionalType();
            OptionalTypes1 = UpdateOptionalType1();
            OptionalTypes2 = UpdateOptionalType2();
            SAInsuranceTypes = UpdateSA();
            BaseAmountTabell = UpdateBaseTable();
            BaseAmounts1 = UpdateBaseAmount();
            BaseAmounts = UpdateBaseAmount();
            DeliveryDate = DateTime.Today;
        }
        #region commands and methods for it. 
        private void RegisterApplication()
        {
            if (Instance.SocialSecurityNumber != null && Instance.City != null && Instance.Firstname != null && Instance.Lastname != null && Instance.PostalCode != null && Instance.EmailOne != null && Instance.StreetAddress != null
               && Instance.DiallingCodeHome != null && Instance.TelephoneNbrHome != null && Instance.LastName != null && Instance.FirstName != null && Instance.SocialSecurityNumberIP != null && Instance.PersonType != null)
            {
                Insurance i = new Insurance()
                {
                    PersonTaker = Instance.Personen = AddInsuranceTaker(),
                    PaymentForm = Instance.PaymentForm,
                    InsuranceStatus = Status.Otecknad,
                    DeliveryDate = Instance.DeliveryDate,
                    AgentNo = Instance.AgentNo,
                    InsuredID = Instance.InsuredPerson = AddInsured(Instance.Personen),
                    OptionalTypes = OTypes(),
                    BaseAmountValue = Instance._barll,
                    SAI = Instance.SAIType,  //Får vi med grunddata eller ej??
                };
                Context.IController.AddInsuranceApplication(i, Instance.Personen);
            }
            else
            {
                MessageBox.Show("Alla fält med en stjärna är obligatoriska!");
            }
        }
        private bool _check;
        public bool Check
        {
            get => _check;
            private set
            {
                _check = value;
            }
        }
        private List<OptionalType> OTypes()
        {
            OptionalType a = new OptionalType();
            OptionalType b = new OptionalType();
            OptionalType c = new OptionalType();

            if (OptionalType != null && ACheck == true)
                a = OptionalType;
            if (OptionalType1 != null && BCheck == true)
                b = OptionalType1;
            if (OptionalType2 != null && CCheck == true)
                c = OptionalType2;
            List<OptionalType> y = new List<OptionalType>() {a, b, c};
            return y;
        }
        //method for autogenerate alphanumeric serialnumber. 
        //private string GenerateIdFormation()
        //{
            
        //}

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
            List<InsuredPerson> tempList = new List<InsuredPerson>();
            InsuredPerson newInp = new InsuredPerson()
            {
                FirstName = Instance.FirstName,
                LastName = Instance.LastName,
                SocialSecurityNumber = Instance.SocialSecurityNumberIP,
                PersonType = Instance.PersonType,
                PersonTaker = p,
            };
            tempList.Add(newInp);
            Context.IPController.AddInsuredPersonOnPersontaker(newInp, p);

            return InsuredPerson;
        }
        private bool CanCreate() => true;

        private ICommand _addInsuranceBtn;
        public ICommand AddInsuranceBtn
        {
            get => _addInsuranceBtn ?? (_addInsuranceBtn = new RelayCommand(x => { RegisterApplication(); CanCreate(); }));
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
        //Get alla SAInsurances. 
        public ObservableCollection<SAInsurance> UpdateSA()
        {
            ObservableCollection<SAInsurance> x = new ObservableCollection<SAInsurance>();

            x.Add(new SAInsurance() { SAID = 0, SAInsuranceType = "inget" });
            foreach (var e in Context.IController.GetAllSAI())
            {
                x?.Add(e);
            }

            SAInsuranceTypes = x;

            return SAInsuranceTypes;
        }
        //OptionalTypeList filled with only one choice. 
        public ObservableCollection<OptionalType> UpdateOptionalType()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            foreach (var e in Context.IController.GetAllOPT())
            {
                if(e.OptionalName == "Månadsersättning vid långvarig sjukskrivning")
                {
                    x?.Add(e);
                }
            }
            OptionalTypes = x;
            return OptionalTypes;
        }
        //OptionalTypeList filled with only one choice. 
        private ObservableCollection<OptionalType> UpdateOptionalType1()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            foreach (var e in Context.IController.GetAllOPT())
            {
                if (e.OptionalName == "Invaliditet vid olycksfall")
                {
                    x?.Add(e);
                }
            }
            OptionalTypes1 = x;
            return OptionalTypes1;
        }
        //OptionalTypeList filled with only one choice. 
        private ObservableCollection<OptionalType> UpdateOptionalType2()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            foreach (var e in Context.IController.GetAllOPT())
            {
                if (e.OptionalName == "Höjning av livförsäkring")
                {
                    x?.Add(e);
                }
            }
            OptionalTypes2 = x;
            return OptionalTypes2;
        }
        //Update all baseamounts for a specific sainsurances and for the delivery year.  
        public ICollection<BaseAmountTabel> UpdateBaseTable()
        {
            List<BaseAmountTabel> x = new List<BaseAmountTabel>();

            foreach (var e in Context.BDController.GetAllTables())
            {
                 if(Today.Year.Equals(e.Date.Year))
                 {
                    x?.Add(e);
                 }
            }
            BaseAmountTabell = x;
            return BaseAmountTabell;
        }
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
        #endregion

        #region lists
        //First list
        public ObservableCollection<OptionalType> OptionalTypes { get; set; }
        // Second list
        public ObservableCollection<OptionalType> OptionalTypes1 { get; set; }
        // Third list
        public ObservableCollection<OptionalType> OptionalTypes2 { get; set; }
        public List<string> PersonTypes { get; set; }
        public ICollection<BaseAmount> BaseAmounts { get; set; }
        public ICollection<BaseAmount> BaseAmounts1 { get; set; }
        public ObservableCollection<SAInsurance> SAInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ICollection<BaseAmountTabel> BaseAmountTabell { get; set; }
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

        #region properties for insured person
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

        #region properties for insurance
        private BaseAmountTabel _baseTbl;
        public BaseAmountTabel BaseTabel
        {
            get => _baseTbl;
            set
            {
                _baseTbl = value;
                OnPropertyChanged("BaseTabel");
            }
        }

        private SAInsurance _Stype;
        //The selection of _Stype is responsible for populating the BaseAmountTabell collection based on their date.
        public SAInsurance SAIType
        {
            get => _Stype;
            set
            {
                _Stype = value;
                OnPropertyChanged("SAIType");
                List<BaseAmountTabel> Bases = new List<BaseAmountTabel>();
                foreach (var e in this.BaseAmountTabell=_Stype.Tabels)
                {
                    if (!Today.Year.Equals(e.Date.Year))
                        Bases.Add(e);
                }
                foreach (var f in Bases)
                {
                    BaseAmountTabell.Remove(f);
                }
                OnPropertyChanged("BaseAmountTabell");
            }
        }
        
        private OptionalType _opType;
        public OptionalType OptionalType
        {
            get => _opType;
            set
            {
                _opType = value;
                OnPropertyChanged("OptionalType");
                List<BaseAmount> Bases = new List<BaseAmount>();
                foreach (var e in this.BaseAmounts = _opType.Amounts)
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

        private OptionalType _opType1;
        public OptionalType OptionalType1
        {
            get => _opType1;
            set
            {
                _opType1 = value;
                OnPropertyChanged("OptionalType1");
                List<BaseAmount> Bases = new List<BaseAmount>();
                foreach (var e in this.BaseAmounts1 = _opType1.Amounts)
                {
                    if (!Today.Year.Equals(e.Date.Year))
                        Bases.Add(e);
                }
                foreach (var f in Bases)
                {
                    BaseAmounts1.Remove(f);
                }
                OnPropertyChanged("BaseAmounts");
            }

        }

        private OptionalType _opType2;
        public OptionalType OptionalType2
        {
            get => _opType2;
            set
            {
                _opType2 = value;
                OnPropertyChanged("OptionalType2");
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
        private int _serialNumber;
        public int SerialNumber 
        { get => _serialNumber;
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
                _status= value;
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
        private int _barll;
        public string BARLL
        {
            get => _barll > 0 ? _barll.ToString() : "";
            set
            {
                if (int.TryParse(value, out _barll))
                {
                    OnPropertyChanged("BARLL");
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra ");
                }
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
        private BaseAmount _bAmount1;
        public BaseAmount BAmount1
        {
            get => _bAmount1;
            set
            {
                _bAmount1 = value;
                OnPropertyChanged("BAmount1");
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


        #region bools for three checkboxes
        private bool _aCheck;
        public bool ACheck
        {
            get => _aCheck;
            set
            {
                _aCheck = value;
                OnPropertyChanged("ACheck");
            }
        }
        private bool _bCheck;
        public bool BCheck
        {
            get => _bCheck;
            set
            {
                _bCheck = value;
                OnPropertyChanged("BCheck");
            }
        }
        private bool _cCheck;
        public bool CCheck
        {
            get => _cCheck;
            set
            {
                _cCheck = value;
                OnPropertyChanged("CCheck");
            }
        }
        #endregion 
    }
}
