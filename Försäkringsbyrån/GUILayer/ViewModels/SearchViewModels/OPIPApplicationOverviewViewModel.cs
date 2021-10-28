using GUILayer.Commands;
using GUILayer.ViewModels.InsuranceViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    /// <summary>
    /// Overview for Otherperson Insurances taken by Persons.
    /// </summary>
    public class OPIPApplicationOverviewViewModel : BaseViewModel
    {
        public static readonly OPIPApplicationOverviewViewModel Instance = new OPIPApplicationOverviewViewModel();

        public OPIPApplicationOverviewViewModel()
        {
        }

        #region Commands 
        private ICommand registerApplication_Btn;
        public ICommand GoBack => registerApplication_Btn ?? (registerApplication_Btn = new RelayCommand(x => { Back(); }));

        
        private void Back()
        {
            if (MainViewModel.Instance.CurrentTool != "Search")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = SearchValueViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "Search";
                MainViewModel.Instance.SelectedViewModel = SearchApplicationChoiceViewModel.Instance;
            }
        }
        public void UpdateComboBoxes()
        {
            SalesMens = UpdateSM();
            OnPropertyChanged("SalesMens");
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OnPropertyChanged("PayMentForms");
            OPInsuranceTypes = UpdateOPI();
            OnPropertyChanged("OPInsuranceTypes");
        }
        
        public void UpdateGridToDb()
        {
            UpdateAC();
            if (Insurancess != null)
            {
                foreach (Insurance i in Insurancess)
                {
                    Context.IController.Edit(i);
                }
            }
        }
       
        private ICommand _exportBtn;
        public ICommand ExportBtn => _exportBtn ?? (_exportBtn = new RelayCommand(x => { ExportApplication();}));


        private ICommand _removeBtn;
        public ICommand RemoveBtn => _removeBtn ?? (_removeBtn = new RelayCommand(x => { RemoveApplication();}));


        public void ExportApplication()
        {
            if (SelectedInsurance != null)
            {
                string date = DateTime.Today.ToString("MM-dd-yyyy");
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream("FörsäkringÖFP.pdf", FileMode.Create));
                BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                Font times = new Font(basefont, 18);
                document.Open();
                document.Add(new Paragraph("Försäkring", times));
                document.Add(new Paragraph("Dagens datum: \t" + date));
                document.Add(new Paragraph("Försäkringstyp: \t" + SelectedInsurance.OPI.OPIName));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Försäkringstagaren ", times));
                document.Add(new Paragraph("Personnummer: \t" + SelectedInsurance.PersonTaker.SocialSecurityNumber));
                document.Add(new Paragraph("Namn: \t" + SelectedInsurance.PersonTaker.Firstname + " " + SelectedInsurance.PersonTaker.Lastname));
                document.Add(new Paragraph("Gatuadress: \t" + SelectedInsurance.PersonTaker.StreetAddress));
                document.Add(new Paragraph("Postnummer: \t" + SelectedInsurance.PersonTaker.PostalCode));
                document.Add(new Paragraph("Postort: \t" + SelectedInsurance.PersonTaker.City));
                document.Add(new Paragraph("Rikt- & telefonnummer bostad: \t" + SelectedInsurance.PersonTaker.DiallingCodeHome + "-" + SelectedInsurance.PersonTaker.TelephoneNbrHome));
                document.Add(new Paragraph("Rikt- & telefonnummer bostad: \t" + SelectedInsurance.PersonTaker.DiallingCodeWork + "-" + SelectedInsurance.PersonTaker.TelephoneNbrWork));
                document.Add(new Paragraph("Email: \t" + SelectedInsurance.PersonTaker.EmailOne));
                document.Add(new Paragraph("Email: \t" + SelectedInsurance.PersonTaker.EmailTwo));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Försäkrad ", times));
                document.Add(new Paragraph("Personnummer: \t" + SelectedInsurance.InsuredID.SocialSecurityNumberIP));
                document.Add(new Paragraph("Namn: \t" + SelectedInsurance.InsuredID.FirstName + " " + SelectedInsurance.InsuredID.LastName));
                document.Add(new Paragraph("Persontyp: \t" + SelectedInsurance.InsuredID.PersonType));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Övriga försäkringsuppgifter ", times));
                document.Add(new Paragraph("Löpnummer: \t" + SelectedInsurance.SerialNumber));
                document.Add(new Paragraph("Tabell: \t" + SelectedInsurance.Table));
                document.Add(new Paragraph("Premie: \t" + SelectedInsurance.Premie));
                document.Add(new Paragraph("Betalform: \t" + SelectedInsurance.PaymentForm));
                document.Add(new Paragraph("Ankomstdatum: \t" + SelectedInsurance.DeliveryDate));
                document.Add(new Paragraph("Agenturnummer: \t" + SelectedInsurance.AgentNo.AgentNumber));

                document.Close();
                Process.Start("FörsäkringÖFP.pdf");
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring att exportera. ");
            }
        }

        public void RemoveApplication()
        {
            if (SelectedInsurance != null)
            {
                if (SelectedInsurance.InsuranceStatus != Status.Tecknad)
                {
                    MessageBoxResult result2 = MessageBox.Show("Vill du verkligen ta bort ansökan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        Context.IController.RemoveInsurance(SelectedInsurance);

                        MessageBox.Show("Ansökan togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        Insurancess.Clear();
                        foreach (var i in Context.IController.GetAllInsurances())
                        {
                            if (i.OPI != null && i.PersonTaker !=null)
                            {
                                Insurancess?.Add(i);
                            }
                        }
                        SignedInsuranceViewModel.Instance.UpdateAC();
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedInsurance.SerialNumber} är inte borttaget");
                    }

                }
                else
                {
                    MessageBox.Show("Tecknad försäkring kan inte tas bort!", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Antingen har ingen person markerats i registret eller så har du lämnat något fält tomt! ", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        
        public void UpdateAC(string filter = "")
        {
            Insurancess = new ObservableCollection<Insurance>();
            List<Insurance> x = new List<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.OPI != null && e.PersonTaker != null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                    if (i.SerialNumber.Contains(filter) || i.PersonTaker.SocialSecurityNumber.Contains(filter) || i.PersonTaker.Firstname.Contains(filter) || i.PersonTaker.Lastname.Contains(filter) || i.TypeName.Contains(filter)
                        || i.InsuredID.SocialSecurityNumberIP.Contains(filter) || i.InsuredID.FirstName.Contains(filter) || i.InsuredID.LastName.Contains(filter))
                        x.Add(i);
            }
            x?.ForEach(i => Insurancess.Add(i));
        }

        #endregion

        #region Lists
        public ObservableCollection<OtherPersonInsurance> OPInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; }

        private ObservableCollection<Insurance> _insurances;
        public ObservableCollection<Insurance> Insurancess
        {
            get => _insurances;
            set
            {
                _insurances = value;
                OnPropertyChanged("Insurancess");
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

        public SalesMen AgentNo
        {
            get => SelectedInsurance.AgentNo;
            set
            {
                SelectedInsurance.AgentNo = value;
                OnPropertyChanged("AgentNo");
                if (AgentNo != null)
                {
                    AgentNo1 = SelectedInsurance.AgentNo;
                    OnPropertyChanged("AgentNo1");
                }
            }
        }

        private SalesMen _an;
        public SalesMen AgentNo1
        {
            get => _an;
            set
            {
                _an = value;
                OnPropertyChanged("AgentNo1");
            }
        }
        public string SerialNumber
        {
            get => SelectedInsurance.SerialNumber;
            set
            {
                SelectedInsurance.SerialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        public InsuredPerson InsuredPerson
        {
            get => SelectedInsurance.InsuredID;
            set
            {
                SelectedInsurance.InsuredID = value;
                OnPropertyChanged("InsuredPerson");
            }
        }
        public Person Personen
        {
            get => SelectedInsurance.PersonTaker;
            set
            {
                SelectedInsurance.PersonTaker = value;
                OnPropertyChanged("Personen");
            }
        }

        public Status InsuranceStatus
        {
            get => SelectedInsurance.InsuranceStatus;
            set
            {
                SelectedInsurance.InsuranceStatus = value;
                OnPropertyChanged("InsuranceStatus");
            }
        }

        public string PaymentForm
        {
            get => SelectedInsurance.PaymentForm;
            set
            {
                SelectedInsurance.PaymentForm = value;
                OnPropertyChanged("PaymentForm");
            }
        }

        public int Premie
        {
            get => SelectedInsurance.Premie;
            set
            {
                SelectedInsurance.Premie = value;
                OnPropertyChanged("Premie");
            }
        }

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
                if (SelectedInsurance != null)
                {
                    UpdateComboBoxes();
                }
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

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                UpdateAC(SearchInput);
                OnPropertyChanged("SearchInput");

            }
        }
    }
}
