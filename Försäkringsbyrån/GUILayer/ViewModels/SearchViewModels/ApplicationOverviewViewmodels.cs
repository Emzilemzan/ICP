using GUILayer.Commands;
using GUILayer.ViewModels.InsuranceViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Diagnostics;

namespace GUILayer.ViewModels.SearchViewModels
{
    /// <summary>
    /// ViewModel for overview of Company applications. 
    /// </summary>
    public class ApplicationOverviewViewModels : BaseViewModel
    {
        public static readonly ApplicationOverviewViewModels Instance = new ApplicationOverviewViewModels();

        private ApplicationOverviewViewModels()
        {
        }
        public void UpdateComboBoxes()
        {
            CompInsurances = UpdateSA();
            OnPropertyChanged("CompInsurances");
            SalesMens = UpdateSM();
            OnPropertyChanged("SalesMens");
            CInsuranceTypes = new List<string>() { "Kombinerad företagsförsäkring", "Fastighet" };
            OnPropertyChanged("CInsuranceTypes");
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OnPropertyChanged("PayMentForms");
            InsuredPersons = UpdateInsuredPerson();
            OnPropertyChanged("InsuredPersons");
            PayYears = GetYears();
            OnPropertyChanged("PayYears");
            PayMonths = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            OnPropertyChanged("PayMonths");
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
        #region command 
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

        private ICommand _exportBtn;

        public ICommand ExportBtn
        {
            get => _exportBtn ?? (_exportBtn = new RelayCommand(x => { Export(); }));
        }

        public void Export()
        {
            if (SelectedInsurance != null)
            {
                string date = DateTime.Today.ToString("MM-dd-yyyy");
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream("FörsäkringFF.pdf", FileMode.Create));
                BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                Font times = new Font(basefont, 18);
                document.Open();
                document.Add(new Paragraph("Försäkring", times));
                document.Add(new Paragraph("Dagens datum: \t" + date));
                document.Add(new Paragraph("Försäkringstyp: \t" + SelectedInsurance.COI.COIName));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Försäkringstagaren ", times));
                document.Add(new Paragraph("Organisationsnummer: \t" + SelectedInsurance.CompanyTaker.OrganizationNumber));
                document.Add(new Paragraph("Företagsnamn: \t" + SelectedInsurance.CompanyTaker.CompanyName));
                document.Add(new Paragraph("Gatuadress: \t" + SelectedInsurance.CompanyTaker.StreetAddress));
                document.Add(new Paragraph("Postnummer: \t" + SelectedInsurance.CompanyTaker.PostalCode));
                document.Add(new Paragraph("Postort: \t" + SelectedInsurance.CompanyTaker.City));
                document.Add(new Paragraph("Rikt- & telefonnummer bostad: \t" + SelectedInsurance.CompanyTaker.DiallingCode + "-" + SelectedInsurance.CompanyTaker.TelephoneNbr));
                document.Add(new Paragraph("Email: \t" + SelectedInsurance.CompanyTaker.Email));
                document.Add(new Paragraph("Faxnummer: \t" + SelectedInsurance.CompanyTaker.FaxNumber));
                document.Add(new Paragraph("Kontaktperson: \t" + SelectedInsurance.CompanyTaker.ContactPerson));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Övriga försäkringsuppgifter ", times));
                document.Add(new Paragraph("Löpnummer: \t" + SelectedInsurance.SerialNumber));
                document.Add(new Paragraph("Försäkringsbolag: \t" + SelectedInsurance.InsuranceCompany));
                document.Add(new Paragraph("Typ av försäkring: \t" + SelectedInsurance.CompanyInsuranceType));
                document.Add(new Paragraph("Premie: \t" + SelectedInsurance.Premie));
                document.Add(new Paragraph("Betalform: \t" + SelectedInsurance.PaymentForm));
                document.Add(new Paragraph("Begynnelsedatum: \t" + SelectedInsurance.StartDate));
                document.Add(new Paragraph("Förfallodatum: \t" + SelectedInsurance.EndDate));
                document.Add(new Paragraph("Agenturnummer: \t" + SelectedInsurance.AgentNo.AgentNumber));
                document.Add(new Paragraph("Anteckningar: \t" + SelectedInsurance.Notes));
                document.Close();
                Process.Start("FörsäkringFF.pdf");
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring att exportera. ");
            }
        }

        private ICommand _removeBtn;
        public ICommand RemoveBtn => _removeBtn ?? (_removeBtn = new RelayCommand(x => { Remove(); }));

        public void Remove()
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
                            if (i.COI != null)
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

        #region Updating methods
        public List<int> PayYears { get; set; }
        public List<int?> PayMonths { get; set; }
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
        public ObservableCollection<CompanyInsurance> UpdateSA()
        {
            ObservableCollection<CompanyInsurance> x = new ObservableCollection<CompanyInsurance>();
            foreach (var e in Context.IController.GetAllCAI())
            {
                x?.Add(e);
            }

            CompInsurances = x;

            return CompInsurances;
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
                if (e.COI !=null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                    if (i.SerialNumber.Contains(filter) || i.CompanyTaker.OrganizationNumber.Contains(filter) || i.TypeName.Contains(filter)
                       || i.CompanyTaker.CompanyName.Contains(filter))
                        x.Add(i);
            }
            x?.ForEach(i => Insurancess.Add(i));
        }

        #endregion

        #region Lists


        public List<string> PayMentForms { get; set; }
        public List<string> CInsuranceTypes { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; }
        public ObservableCollection<CompanyInsurance> CompInsurances { get; set; }

        private ObservableCollection<Insurance> _insurance;
        public ObservableCollection<Insurance> Insurancess
        {
            get => _insurance;
            set
            {
                _insurance = value;
                OnPropertyChanged("Insurancess");
            }
        }
        #endregion

        #region Properties
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
        public Company Companyn
        {
            get => SelectedInsurance.CompanyTaker;
            set
            {
                SelectedInsurance.CompanyTaker = value;
                OnPropertyChanged("CompanyTaker");
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

        public DateTime StartDate
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    return SelectedInsurance.StartDate;
                }
                else
                {
                    return StartDate = DateTime.Now;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.StartDate = value;
                }
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    return SelectedInsurance.EndDate;
                }
                else
                {
                    return EndDate = DateTime.Now;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.EndDate = value;
                }
                OnPropertyChanged("EndDate");
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

        public string Notes
        {
            get => SelectedInsurance.Notes;
            set
            {
                SelectedInsurance.Notes = value;
                OnPropertyChanged("Notes");
            }
        }
        public string InsuranceCompany
        {
            get => SelectedInsurance.InsuranceCompany;
            set
            {
                SelectedInsurance.InsuranceCompany = value;
                OnPropertyChanged("InsuranceCompany");
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

        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }

        private Insurance _selectedInsurance;
        public Insurance SelectedInsurance
        {
            get => _selectedInsurance;
            set
            {

                _selectedInsurance = value;
                PayMentForms = null;
                SalesMens = null;

                OnPropertyChanged("SelectedInsurance");
                if (SelectedInsurance != null)
                {
                    UpdateComboBoxes();
                    EndDate = SelectedInsurance.EndDate;
                    StartDate = SelectedInsurance.StartDate;
                    OnPropertyChanged("EndDate");
                    OnPropertyChanged("StartDate");

                }
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
          #endregion

    }
}