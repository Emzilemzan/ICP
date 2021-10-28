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
    /// Overview for our Lifeinsurances.
    /// </summary>
    public class LifeOverviewViewModel : BaseViewModel
    {
        public static readonly LifeOverviewViewModel Instance = new LifeOverviewViewModel();

        private LifeOverviewViewModel()
        {
        }

        #region command 
        private ICommand registerApplication_Btn;
        public ICommand GoBack => registerApplication_Btn ?? (registerApplication_Btn = new RelayCommand(x => { Back(); }));

        // Takes user back to previous view.
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
        private ICommand _updateBtn;

        public ICommand UpdateBtn => _updateBtn ?? (_updateBtn = new RelayCommand(x => { Update(); }));

        // Method that updates the datagrid and the database when changes are made in the view.
        public void UpdateGridToDb()
        {
            UpdateAC();
            if (Insurances != null)
            {
                foreach (Insurance i in Insurances)
                {
                    Context.IController.Edit(i);
                }
            }
        }

        public void Update()
        {
            if (SelectedInsurance != null && PaymentForm != null && AgentNo != null && SerialNumber != null
               && BAmount != null)
            {
                SelectedInsurance.PaymentForm = PaymentForm;
                SelectedInsurance.AgentNo = AgentNo;
                SelectedInsurance.SerialNumber = SerialNumber;
                SelectedInsurance.BaseAmountValue = Instance.BAmount.Baseamount;
                SelectedInsurance.AckValue4 = Context.BDController.CountAckvalueLife(SelectedInsurance.DeliveryDate, SelectedInsurance.LIFE, Instance.BAmount.Baseamount);
                Context.IController.Edit(SelectedInsurance);
                MessageBox.Show("Försäkring är uppdaterad!", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                Insurances.Clear();
                foreach (var i in Context.IController.GetAllInsurances())
                {
                    if (i.LIFE != null)
                    {
                        Insurances?.Add(i);
                    }
                }
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _exportBtn;

        public ICommand ExportBtn => _exportBtn ?? (_exportBtn = new RelayCommand(x => { Export(); }));

        public void Export()
        {
            if (SelectedInsurance != null)
            {
                string date = DateTime.Today.ToString("MM-dd-yyyy");
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream("FörsäkringLiv.pdf", FileMode.Create));
                BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                Font times = new Font(basefont, 18);
                document.Open();
                document.Add(new Paragraph("Försäkring", times));
                document.Add(new Paragraph("Dagens datum: \t" + date));
                document.Add(new Paragraph("Försäkringstyp: \t" + SelectedInsurance.LIFE.LifeName));
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
                document.Add(new Paragraph("Ankomstdatum: \t" + SelectedInsurance.DeliveryDate));
                document.Add(new Paragraph("Betalform: \t" + SelectedInsurance.PaymentForm));
                document.Add(new Paragraph("Agenturnummer: \t" + SelectedInsurance.AgentNo.AgentNumber));
                document.Add(new Paragraph("Grundbelopp för Liv: \t" + SelectedInsurance.BaseAmountValue + " kr"));
               
                document.Close();
                Process.Start("FörsäkringLiv.pdf");
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
                        Insurances.Clear();
                        foreach (var i in Context.IController.GetAllInsurances())
                        {
                            if (i.LIFE != null)
                            {
                                Insurances?.Add(i);
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

        #region updatelists
        
        //Update all baseamounts for a specific lifeinsurances and for the delivery year.  
        public ICollection<BaseAmount> UpdateBaseAmount()
        {
            List<BaseAmount> x = new List<BaseAmount>();
            if (SelectedInsurance != null)
            {
                foreach (var e in Context.BDController.GetAllBaseAmount())
                {
                    if (DateTime.Today.Year.Equals(e.Date.Year) && SelectedInsurance.LIFE.Amounts.Contains(e))
                    {
                        x?.Add(e);
                    }
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

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                UpdateAC(SearchInput);
            }
        }
        public void UpdateAC(string filter = "")
        {
            Insurances = new ObservableCollection<Insurance>();
            List<Insurance> x = new List<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.LIFE != null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                {
                    if (i.SerialNumber.Contains(filter) || i.PersonTaker.SocialSecurityNumber.Contains(filter) || i.TypeName.Contains(filter) || i.PersonTaker.Firstname.Contains(filter)
                        || i.InsuredID.SocialSecurityNumberIP.Contains(filter) || i.InsuredID.LastName.Contains(filter) || i.PersonTaker.Lastname.Contains(filter)
                        || i.LIFE.LifeName.Contains(filter) || i.BaseAmountValue.ToString().Contains(filter) || i.AgentNo.AgentNumber.ToString().Contains(filter))
                    {
                        x.Add(i);
                    }
                }
            }
            x?.ForEach(i => Insurances.Add(i));
        }

        public void UpdateComboBoxes()
        {
            SalesMens = UpdateSM();
            OnPropertyChanged("SalesMens");
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OnPropertyChanged("PayMentForms");
            LifeInsuranceTypes = UpdateLife();
            OnPropertyChanged("LifeInsuranceTypes");
        }
        #endregion

        #region list
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();

        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();

        private ObservableCollection<Insurance> _insurances;
        public ObservableCollection<Insurance> Insurances
        {
            get => _insurances;
            set
            {
                _insurances = value;
                OnPropertyChanged("Insurances");
            }
        }
        public List<string> PersonTypes { get; set; }
        public ObservableCollection<LifeInsurance> LifeInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ICollection<BaseAmount> BaseAmounts { get; set; }
        #endregion

        #region properties

        //The selection of LItype is responsible for populating the BaseAmounts collection based on their date.
        public LifeInsurance LType
        {
            get => SelectedInsurance.LIFE;

            set
            {
                SelectedInsurance.LIFE = value;
                OnPropertyChanged("LType");
            }
        }

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
                    List<BaseAmount> Bases = new List<BaseAmount>();
                    foreach (var e in this.BaseAmounts = SelectedInsurance.LIFE.Amounts)
                    {
                        if (!SelectedInsurance.DeliveryDate.Year.Equals(e.Date.Year))
                            Bases.Add(e);
                    }
                    foreach (var f in Bases)
                    {
                        BaseAmounts.Remove(f);
                    }
                    OnPropertyChanged("BaseAmounts");
                    BaseAmount c = new BaseAmount();
                    foreach (var b in SelectedInsurance.LIFE.Amounts)
                    {
                        if (b.Baseamount.Equals(SelectedInsurance.BaseAmountValue))
                        {
                            c = b;
                        }
                    }
                    BAmount = c;
                    OnPropertyChanged("BAmount");
                }

            }


        }

        #endregion

    }
}
