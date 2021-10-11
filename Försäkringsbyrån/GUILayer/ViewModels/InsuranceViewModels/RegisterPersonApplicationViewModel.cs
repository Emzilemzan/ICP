using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class RegisterPersonApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterPersonApplicationViewModel Instance = new RegisterPersonApplicationViewModel();

        public RegisterPersonApplicationViewModel()
        {
            //Types = UpdateInsuranceTypes();
            SalesMens = UpdateSM();
            PersonTypes = new List<string>() {"Vuxen","Barn"};

        }

        #region commands

        private void RegisterApplication()
        {
            //add insuredperson
            //add insurancetaker if not exits
            //add application. 
        }

        #endregion 


        #region update of collections and lists. 
        private ObservableCollection<InsuranceType> UpdateInsuranceTypes()
        {
            ObservableCollection<InsuranceType> x = new ObservableCollection<InsuranceType>();
            foreach (var t in Context.IController.GetAllIT())
            {
                x?.Add(t);
            }
            Types = x;
            return Types;
        }

        private InsuranceType _insuranceType;
        public InsuranceType InsuranceType
        {
            get => _insuranceType;
            set
            {
                _insuranceType = value;
                OnPropertyChanged("InsuranceType");
            }
        }

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

        public List<string> PersonTypes { get; set; }
        public ObservableCollection<InsuranceType> Types { get; set; }
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
        private InsuranceTaker _taker;
        public InsuranceTaker Taker
        {
            get => _taker;
            set
            {
                _taker = value;
                OnPropertyChanged("Taker");
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
        #endregion

    }
}
