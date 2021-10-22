using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
   public class OPIPApplicationOverviewViewModel : BaseViewModel
    {
        public static readonly OPIPApplicationOverviewViewModel Instance = new OPIPApplicationOverviewViewModel();

        public OPIPApplicationOverviewViewModel()
        {
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            Insurancess = UpdateInsurance();
            OPInsuranceTypes = UpdateOPI();
            InsuredPersons = UpdateInsuredPerson();
        }

        #region Commands 
        // Update, Export and Remove buttons 
        public bool CanCreate() => true;

        private ICommand _updateBtn;
        public ICommand UpdateBtn
        {
            get => _updateBtn ?? (_updateBtn = new RelayCommand(x => { UpdateApplication(); CanCreate(); }));
        }

        private ICommand _exportBtn;
        public ICommand ExportBtn
        {
            get => _exportBtn ?? (_exportBtn = new RelayCommand(x => { ExportApplication(); CanCreate(); }));
        }


        private ICommand _removeBtn;
        public ICommand RemoveBtn
        {
            get => _removeBtn ?? (_removeBtn = new RelayCommand(x => { RemoveApplication(); CanCreate(); }));
        }


        #endregion

        #region Methods

      

        public void UpdateApplication()
        {
            if (SelectedInsurance != null && SelectedInsurance.Table != null && SelectedInsurance.Premie != 0 && SelectedInsurance.DeliveryDate != null &&
             SelectedInsurance.PaymentForm != null )
            {
                SelectedInsurance.Table = Instance.Tabell;
                SelectedInsurance.Premie = Instance._premie;
                SelectedInsurance.AgentNo = Instance.AgentNo;
                SelectedInsurance.PaymentForm = Instance.PaymentForm;
                SelectedInsurance.SerialNumber = Instance.SerialNumber;


                MessageBox.Show($"Uppdateringen lyckades av: {SelectedInsurance.SerialNumber}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                //Companies.Clear();
                foreach (var c in Context.IController.GetAllInsurances())
                {
                    Insurancess?.Add(c);
                }
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExportApplication()
        {

        }

        public void RemoveApplication()
        {

        }
        #endregion

        #region Methods for updating
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
            ObservableCollection<OtherPersonInsurance> x = new ObservableCollection<OtherPersonInsurance>
            {
                new OtherPersonInsurance() { OPIId = 0, OPIName = "Inget" }
            };
            foreach (var e in Context.IController.GetAllOPI())
            {
                x?.Add(e);
            }
            OPInsuranceTypes = x;
            return OPInsuranceTypes;
        }

        public ObservableCollection<Insurance> UpdateInsurance()
        {
            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();

            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.OPI != null && e.PersonTaker !=null)
                    x?.Add(e);
            }

            Insurancess = x;
            return Insurancess;
        }

        public ObservableCollection<InsuredPerson> UpdateInsuredPerson()
        {
            ObservableCollection<InsuredPerson> x = new ObservableCollection<InsuredPerson>();
            foreach (var e in Context.IPController.GetAllInsuredPersons())
            {
                x?.Add(e);
            }
            InsuredPersons = x;
            return InsuredPersons;
        }

        #endregion

        #region Lists
        public ObservableCollection<OtherPersonInsurance> OPInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; }
        public ObservableCollection<Insurance> Insurancess { get; set; }

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

        #region GridViews, filter and search

   

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                
            }
        }
        #endregion

        #region Properties Person
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
                if (int.TryParse(value, out _pC) && PostalCode.Length == 5)
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

        #region Properties Insurance
        private SalesMen _an;
        public SalesMen AgentNo
        {
            get => SelectedInsurance.AgentNo;
            set
            {
                SelectedInsurance.AgentNo = value;
                OnPropertyChanged("AgentNo");
            }
        }


      
        private string _serialNumber;
        public string SerialNumber
        {
            get => SelectedInsurance.SerialNumber;
            set
            {
                SelectedInsurance.SerialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        private InsuredPerson _iP;
        public InsuredPerson InsuredPerson
        {
            get => SelectedInsurance.InsuredID;
            set
            {
                SelectedInsurance.InsuredID = value;
                OnPropertyChanged("InsuredPerson");
            }
        }
        private Person _person;
        public Person Personen
        {
            get => SelectedInsurance.PersonTaker;
            set
            {
                SelectedInsurance.PersonTaker = value;
                OnPropertyChanged("Personen");
            }
        }

        private Status _status;
        public Status InsuranceStatus
        {
            get => SelectedInsurance.InsuranceStatus;
            set
            {
                SelectedInsurance.InsuranceStatus = value;
                OnPropertyChanged("InsuranceStatus");
            }
        }

        private string _payMent;
        public string PaymentForm
        {
            get => SelectedInsurance.PaymentForm;
            set
            {
                SelectedInsurance.PaymentForm = value;
                OnPropertyChanged("PaymentForm");
            }
        }

        private int _premie;
        public string Premie
        {
            get => SelectedInsurance.Premie > 0 ? SelectedInsurance.Premie.ToString() : "";
            set
            {
                SelectedInsurance.Premie = 0;
                if (int.TryParse(value, out _premie) && SelectedInsurance.Premie.ToString().Length == 5)
                {

                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara ett tal");
                }
                OnPropertyChanged("Premie");
            }
        }

        private string _tabel;
        public string Tabell
        {
            get => SelectedInsurance.Table;
            set
            {
                SelectedInsurance.Table = value;
                OnPropertyChanged("Tabell");
            }
        }

        public DateTime DeliveryDate
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    return SelectedInsurance.DeliveryDate;
                }
                else
                {
                    return DeliveryDate = DateTime.Now;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.DeliveryDate = value;
                }
                OnPropertyChanged("DeliveryDate");
            }
        }
        public string InsuranceNumber
        {
            get => SelectedInsurance.InsuranceNumber;
            set
            {
                SelectedInsurance.InsuranceNumber = value;
                OnPropertyChanged("InsuranceNumber");
            }
        }
        public int? PayMonth
        {
            get => SelectedInsurance.PayMonth;
            set
            {
                SelectedInsurance.PayMonth = value;
                OnPropertyChanged("PayMonth");
            }
        }
        public int? PayYear
        {
            get => SelectedInsurance.PayYear;
            set
            {
                SelectedInsurance.PayYear = value;
                OnPropertyChanged("PayYear");
            }
        }

        public int? PossibleComisson
        {
            get => SelectedInsurance.PossibleComisson;
            set
            {
                SelectedInsurance.PossibleComisson = value;
                OnPropertyChanged("PossibleComisson");
            }
        }
        public List<int> Years { get; set; }
        public List<int> Months { get; set; }
        //För att visa årtal i combobox. 
        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }

        private Insurance _si;
        public Insurance SelectedInsurance
        {
            get => _si;
            set
            {
                _si = value;
                OnPropertyChanged("SelectedInsurance");
            }
        }

        #endregion

        #region Properties InsuredPerson
        //private int _insuredId;
        //public int InsuredID
        //{
        //    get => _insuredId;
        //    set
        //    {
        //        _insuredId = value;
        //        OnPropertyChanged("InsuredID");
        //    }
        //}
        //private string _sSNIP;
        //public string SocialSecurityNumberIP
        //{
        //    get => _sSNIP;
        //    set
        //    {
        //        _sSNIP = value;
        //        OnPropertyChanged("SocialSecurityNumberIP");
        //    }
        //}
        //private string _lName;
        //public string LastNameIP
        //{
        //    get => _lName;
        //    set
        //    {
        //        _lName = value;
        //        OnPropertyChanged("LastNameIP");
        //    }
        //}
        //private string _fName;
        //public string FirstNameIP
        //{
        //    get => _fName;
        //    set
        //    {
        //        _fName = value;
        //        OnPropertyChanged("FirstNameIP");
        //    }
        //}
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

    }
}
