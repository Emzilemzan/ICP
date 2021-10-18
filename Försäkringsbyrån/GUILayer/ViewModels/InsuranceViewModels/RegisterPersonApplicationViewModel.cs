using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
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
            PersonType = PersonTypes[0];
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OptionalTypes = UpdateOptionalType();
            OptionalTypes1 = UpdateOptionalType1();
            OptionalTypes2 = UpdateOptionalType2();
            SAInsuranceTypes = UpdateSA();
            BaseAmountTabell = UpdateBaseTable();
            BaseAmounts1 = UpdateBaseAmount();
            DeliveryDate = DateTime.Today;
            BaseAmountsOP1 = new List<int>() { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000 };
            BaseAmountsOP2 = new List<int>() { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
            OptionalType = OptionalTypes[0];
            OptionalType1 = OptionalTypes1[0];
            OptionalType2 = OptionalTypes2[0];
        }
        #region commands and methods for it. 

        private void AddInsurance()
        {
            string y;
            Person x = Instance.Personen = AddInsuranceTaker();
            InsuredPerson insured = Instance.InsuredPerson = AddInsured(x);
            if(Instance.SAIType.SAID == 1)
            {
                 y = Instance.SerialNumber = GenerateIdFormationSOB();
            }
            else
            {
                 y = Instance.SerialNumber = GenerateIdFormationSOV();
            }
            Insurance i = new Insurance()
            {
                SerialNumber = y,
                PersonTaker = x,
                TakerNbr = x.SocialSecurityNumber,
                TypeName = Instance.SAIType.SAInsuranceType,
                PaymentForm = Instance.PaymentForm,
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = Instance.DeliveryDate,
                AgentNo = Instance.AgentNo,
                InsuredID = insured,
                OptionalTypes = OTypes(),
                BaseAmountValue = Instance._barll,
                AckValue = Instance.AckValue = Context.BDController.CountAckvalueOt(Instance.DeliveryDate, Instance.OptionalType2, Instance._barll),
                BaseAmountValue2 = Instance.BAmount,
                AckValue2 = Instance.AckValue2 = Context.BDController.CountAckvalueOt(Instance.DeliveryDate, Instance.OptionalType, Instance.BAmount),
                BaseAmountValue3 = Instance.BAmount1,
                AckValue3 = Instance.AckValue3 = Context.BDController.CountAckvalueOt(Instance.DeliveryDate, Instance.OptionalType1, Instance.BAmount1),
                BaseAmountValue4 = Instance.BaseTabel.BaseAmount,
                AckValue4 = Instance.BaseTabel.AckValue,
                SAI = Instance.SAIType,
            };
            Context.IController.AddInsuranceApplication(i);
            MessageBox.Show("Ansökan har lagts till");
            new SignedInsuranceViewModel();
            Check = true;
            Instance.ACheck = false;
            Instance.BCheck = false;
            Instance.CCheck = false;
            Instance.BAmount = 0;
            Instance.BAmount1 = 0;
            Instance.AgentNo = null;
            Instance.BARLL = string.Empty;
            Instance.BaseTabel = null;
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
            Instance.SAIType = null;
            Instance.DeliveryDate = Today;
            Instance.LastName = string.Empty;
            Instance.Lastname = string.Empty;
            Instance.FirstName = string.Empty;
            Instance.Firstname = string.Empty;
            Instance.PaymentForm = null;
            Instance.PersonType = null;
            Instance.PostalCode = string.Empty;
        }

        private void BoxesCheckInsurance()
        {
            if (Instance.SocialSecurityNumber != null && Instance.City != null && Instance.Firstname != null && Instance.Lastname != null && Instance.PostalCode != null && Instance.EmailOne != null && Instance.StreetAddress != null
                          && Instance.DiallingCodeHome != null && Instance.TelephoneNbrHome != null && Instance.PaymentForm != null && Instance.DeliveryDate != null && Instance.DeliveryDate != null && Instance.SAIType != null
              && Instance.AgentNo != null)
            {
                if (IPISPerson == false)
                {
                    if (SAIType.SAID != 1)
                    {
                        AddInsurance();
                    }
                    else
                    {
                        MessageBox.Show("Om försäkringstagaren är samma som försäkrad måste SO vara för vuxen. ");
                    }
                }
                else
                {
                    if (Instance.LastName != null && Instance.FirstName != null && Instance.SocialSecurityNumberIP != null && Instance.PersonType != null)
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

            if (ACheck == true)
            {
                if (OptionalType != null && BAmount != 0)
                    a = OptionalType;
                else
                {
                    MessageBox.Show("Om någon checkbox för tillval är i klickad måste du också ha fyllt i tillhörande uppgifter");
                }
            }
            if (BCheck == true)
            {
                if (OptionalType1 != null && BARLL != null)
                    b = OptionalType1;
                else
                {
                    MessageBox.Show("Om någon checkbox för tillval är i klickad måste du också ha fyllt i tillhörande uppgifter");
                }
            }
            if (CCheck == true)
            {
                if (OptionalType2 != null && BAmount1 != 0)
                    c = OptionalType2;
                else
                {
                    MessageBox.Show("Om någon checkbox för tillval är i klickad måste du också ha fyllt i tillhörande uppgifter");
                }
            }
            List<OptionalType> y = new List<OptionalType>() { a, b, c };
            return y;
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
                SocialSecurityNumber = Instance.SocialSecurityNumberIP,
                PersonType = Instance.PersonType,
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
        private string GenerateIdFormationSOB()
        {
            string y;
            List<Insurance> insurances = new List<Insurance>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.SAI != null)
                {
                    if (SAIType.SAID == 1)
                        insurances?.Add(i);
                }
            }
            if (insurances.Count < 1)
            {
                string str = "SOB";
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

        private string GenerateIdFormationSOV()
        {
            string y;
            List<Insurance> insurances = new List<Insurance>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.SAI != null)
                {
                    if (SAIType.SAID == 2)
                        insurances?.Add(i);
                }
            }

            if (insurances.Count < 1)
            {
                string str = "SOV";
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
        //Get alla SAInsurances. 
        public ObservableCollection<SAInsurance> UpdateSA()
        {
            ObservableCollection<SAInsurance> x = new ObservableCollection<SAInsurance>();

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
                if (e.OptionalName == "Månadsersättning vid långvarig sjukskrivning")
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
                if (Today.Year.Equals(e.Date.Year))
                {
                    x?.Add(e);
                }
            }
            BaseAmountTabell = x;
            return BaseAmountTabell;
        }
        //Update all baseamounts for a specific lifeinsurance and for the delivery year.  
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
            BaseAmounts1 = x;
            return BaseAmounts1;
        }
        #endregion

        #region lists

        public List<int> BaseAmountsOP1 { get; set; }
        public List<int> BaseAmountsOP2 { get; set; }
        //First list
        public ObservableCollection<OptionalType> OptionalTypes { get; set; }
        // Second list
        public ObservableCollection<OptionalType> OptionalTypes1 { get; set; }
        // Third list
        public ObservableCollection<OptionalType> OptionalTypes2 { get; set; }
        public List<string> PersonTypes { get; set; }
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
                if (IPISPerson == false)
                {
                    _sSNIP = SocialSecurityNumber;
                }
                else
                {
                    _sSNIP = value;
                }
                OnPropertyChanged("SocialSecurityNumberIP");
            }
        }
        private string _lName;
        public string LastName
        {
            get => _lName;
            set
            {
                if (IPISPerson == false)
                {
                    _lName = Lastname;
                }
                else
                {
                    _lName = value;
                }
                OnPropertyChanged("LastName");
            }
        }

        private string _fName;
        public string FirstName
        {
            get => _fName;
            set
            {
                if (IPISPerson == false)
                {
                    _fName = Firstname;
                }
                else
                {
                    _fName = value;
                }
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
                if (Check == false)
                {
                    List<BaseAmountTabel> Bases = new List<BaseAmountTabel>();
                    foreach (var e in this.BaseAmountTabell = _Stype.Tabels)
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
        }

        private OptionalType _opType;
        public OptionalType OptionalType
        {
            get => _opType;
            set
            {
                _opType = value;
                OnPropertyChanged("OptionalType");
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
        private int _bAmount;
        public int BAmount
        {
            get => _bAmount;
            set
            {
                _bAmount = value;
                OnPropertyChanged("BAmount");
            }
        }
        private int _bAmount1;
        public int BAmount1
        {
            get => _bAmount1;
            set
            {
                _bAmount1 = value;
                OnPropertyChanged("BAmount1");
            }
        }
        private int _bAV4;
        public int BaseAmountValue4
        {
            get => _bAV4;
            set
            {
                _bAV4 = value;
                OnPropertyChanged("BaseAmountValue4");
            }
        }
        private double _av;
        public double AckValue
        {
            get => _av;
            set
            {
                _av = value;
                OnPropertyChanged("AckValue");
            }
        }
        private double _av2;
        public double AckValue2
        {
            get => _av2;
            set
            {
                _av2 = value;
                OnPropertyChanged("AckValue2");
            }
        }

        private double _av3;
        public double AckValue3
        {
            get => _av3;
            set
            {
                _av3 = value;
                OnPropertyChanged("AckValue3");
            }
        }
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
