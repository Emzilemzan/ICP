using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class RegisterPersonApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterPersonApplicationViewModel Instance = new RegisterPersonApplicationViewModel();

        public RegisterPersonApplicationViewModel()
        {
            SalesMens = UpdateSM();
            PersonTypes = new List<string>() {"Vuxen","Barn"};
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad"};
            OptionalTypes = UpdateS();
            OptionalTypes1 = UpdateOptionalType();
            OptionalType = OptionalTypes[0];
        }

        #region commands

        private void RegisterApplication()
        {
            //add insuredperson
            //add insurancetaker if not exits
            //add application. 
        }

        private bool CanCreate() => true;

        private ICommand _addInsuranceBtn;
        public ICommand AddInsuranceBtn
        {
            get => _addInsuranceBtn ?? (_addInsuranceBtn = new RelayCommand(x => { RegisterApplication(); CanCreate(); }));
        }

         /// <summary>
         /// Alternativ lösning. Lägga in kontroller i ADDmetoden senare att det inte går att lägga till en optionaltype som redan är vald i ett tidigare skede. 
         /// Inte heller gå att lägga till typen Inget. 
         /// </summary>
         /// <returns></returns>
        private ObservableCollection<OptionalType> UpdateOptionalType()
        {

            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();

            x.Add(new OptionalType() { OptionalTypeId = 0, OptionalName = "inget" });
            foreach (var e in Context.IController.GetAllOPT())
            {
                x?.Add(e);
            }
            if(x.Contains(OptionalType))
            {
                x.Remove(OptionalType);
            }

            OptionalTypes1 = x;
            
            return OptionalTypes1;
        }

        #endregion
        #region update of collections and lists. 

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

        public ObservableCollection<OptionalType> UpdateS()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            x.Add(new OptionalType() { OptionalTypeId = 0, OptionalName = "inget" });
            foreach (var e in Context.IController.GetAllOPT())
            {
                x?.Add(e);
            }
 
            OptionalTypes = x;
            OnPropertyChanged("OptionalTypes1");
            return OptionalTypes;

        }

        // Second list
        public ObservableCollection<OptionalType> OptionalTypes1 { get; set; }

        // Third list
        public ObservableCollection<OptionalType> OptionalTypes2 { get; set; }

        public List<string> PersonTypes { get; set; }
        public List<SAInsurance> SAInsuranceTypes { get; set; }

        public ObservableCollection<OptionalType> OptionalTypes { get; set; } 
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
        public int PostalCode
        {
            get => _pC;
            set
            {
                _pC = value;
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

        private InsuranceType _type;
        public InsuranceType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("Type");
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
        private InsuranceType _iTI;
        public InsuranceType InsuranceTypeId
        {
            get => _iTI;
            set
            {
                _iTI = value;
                OnPropertyChanged("InsuranceTypeId");
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

        private DateTime _dd;
        public DateTime DeliveryDate
        {
            get => _dd;
            set
            {
                _dd = value;
                OnPropertyChanged("DeliveryDate");
            }
        }
        #endregion

    }
}
