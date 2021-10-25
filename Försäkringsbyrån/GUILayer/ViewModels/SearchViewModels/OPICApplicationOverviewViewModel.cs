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

namespace GUILayer.ViewModels.SearchViewModels
{
    public class OPICApplicationOverviewViewModel : BaseViewModel
    {
        public static readonly OPICApplicationOverviewViewModel Instance = new OPICApplicationOverviewViewModel();

        public OPICApplicationOverviewViewModel()
        {
            Insurancess = UpdateInsurance();
            SalesMens = UpdateSM();
            UpdateAC();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
              OPInsuranceTypes = UpdateOPI();
            InsuredPersons = UpdateInsuredPerson();
        }

        #region Commands
        private ICommand _updateBtn;
        public ICommand UppdateBtn
        {
            get => _updateBtn ?? (_updateBtn = new RelayCommand(x => { UpdateApplication(); CanCreate(); }));
        }

        private bool CanCreate() => true;

        public void UpdateApplication()
        {
            if (SelectedInsurance != null && SelectedInsurance.Table != null && SelectedInsurance.Premie != 0 &&
            SelectedInsurance.PaymentForm != null && SelectedInsurance.AgentNo != null)
            {
                SelectedInsurance.Table = Tabell;
                SelectedInsurance.Premie = Premie;
                SelectedInsurance.AgentNo = AgentNo;
                SelectedInsurance.PaymentForm = PaymentForm;
                SelectedInsurance.SerialNumber = SerialNumber;
                Context.IController.Edit(SelectedInsurance);

                MessageBox.Show($"Uppdateringen lyckades av: {SelectedInsurance.SerialNumber}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
       
    }

    private ICommand _exportBtn;
        public ICommand ExportBtn
        {
            get => _exportBtn ?? (_exportBtn = new RelayCommand(x => { ExportApplication(); CanCreate(); }));
        }

        public void ExportApplication()
        {

        }

        private ICommand _removeBtn;
        public ICommand RemoveBtn
        {
            get => _removeBtn ?? (_removeBtn = new RelayCommand(x => { RemoveApplication(); CanCreate(); }));
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
                            if (i.OPI != null && i.CompanyTaker !=null)
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

        #region Updates

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
                if (e.OPI != null && e.CompanyTaker != null)
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
        public void UpdateAC(string filter = "")
        {
            Insurancess = new ObservableCollection<Insurance>();
            List<Insurance> x = new List<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.OPI != null && e.CompanyTaker !=null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                    if (i.SerialNumber.Contains(filter) || i.CompanyTaker.OrganizationNumber.Contains(filter) || i.TypeName.Contains(filter)
                        || i.InsuredID.SocialSecurityNumberIP.Contains(filter) || i.CompanyTaker.ContactPerson.Contains(filter) || i.CompanyTaker.CompanyName.Contains(filter))
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
