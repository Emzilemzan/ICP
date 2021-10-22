using GUILayer.Commands;
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
    public class SAOverviewViewModel : BaseViewModel
    {
        public static readonly SAOverviewViewModel Instance = new SAOverviewViewModel();

        private SAOverviewViewModel()
        {
            BaseAmountsOP1 = new List<int>() { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000 };
            BaseAmountsOP2 = new List<int>() { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            SAInsuranceTypes = UpdateSA();
            BaseAmountTabell = UpdateBaseTable();
            BaseAmounts1 = UpdateBaseAmount();
            Insurances = UpdateInsurance();
            SelectedInsurance = Insurances[0];
        }

        #region command 
        private ICommand _updateBtn;

        public ICommand UpdateBtn
        {
            get => _updateBtn ?? (_updateBtn = new RelayCommand(x => { Update(); }));
        }

        public List<OptionalType> UpdateOT()
        {
            OptionalType a = new OptionalType();
            OptionalType b = new OptionalType();
            OptionalType c = new OptionalType();
            a = Context.IController.GetOPT(1);
            b = Context.IController.GetOPT(2);
            c = Context.IController.GetOPT(3);
            List<OptionalType> x = new List<OptionalType>();
            if (BARLL != 0)
            {
                x.Add(b);
            }
            if (BAmount != 0)
            {
                x.Add(c);
            }
            if (BAmount1 != 0)
            {
                x.Add(a);
            }
            return x;
        }

        public void Update()
        {
            if (SelectedInsurance != null && PaymentForm != null && AgentNo != null && SerialNumber != null
               /* && BaseTabel != null*/)
            {
                SelectedInsurance.PaymentForm = PaymentForm;
                SelectedInsurance.OptionalTypes = UpdateOT(); 
                SelectedInsurance.AgentNo = AgentNo;
                SelectedInsurance.SerialNumber = SerialNumber;
                SelectedInsurance.BaseAmountValue = BARLL;
                SelectedInsurance.BaseAmountValue2 = BAmount;
                SelectedInsurance.BaseAmountValue3 = BAmount1;
                //SelectedInsurance.BaseAmountValue4 = BaseAmount = Instance.BaseTabel.BaseAmount;
                Context.IController.Edit(SelectedInsurance);
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkring i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _exportBtn;

        public ICommand ExportBtn
        {
            get => _exportBtn ?? (_exportBtn = new RelayCommand(x => { Export(); }));
        }

        public void Export()
        {

        }

        private ICommand _removeBtn;

        public ICommand RemoveBtn
        {
            get => _removeBtn ?? (_removeBtn = new RelayCommand(x => { Remove(); }));
        }

        public void Remove()
        {

        }
        #endregion
        private ICommand _valusBtn;
        public ICommand WhatValuesOP1Btn => _valusBtn ?? (_valusBtn = new RelayCommand(x => { WhatValuesOP1(); }));
        private void WhatValuesOP1()
        {
            MessageBox.Show("De grundbelopp du kan skriva in är: 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000", "Grundbelopp för tillval", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private ICommand _valusOP2Btn;
        public ICommand WhatValuesOP2Btn => _valusOP2Btn ?? (_valusOP2Btn = new RelayCommand(x => { WhatValuesOP2(); }));
        private void WhatValuesOP2()
        {
            MessageBox.Show("De grundbelopp du kan skriva in är: 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000", "Grundbelopp för tillval", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ICommand _valusBase;
        public ICommand WhatValuesSABtn => _valusBase ?? (_valusBase = new RelayCommand(x => { WhatValuesSA(); }));
        private void WhatValuesSA()
        {
            //kod för att hitta grundbeloppen i baseamounttable som går att välja mellan för den SAI
        }

        //Update all baseamounts for a specific sainsurances and for the delivery year.  
        public ICollection<BaseAmountTabel> UpdateBaseTable()
        {
            List<BaseAmountTabel> x = new List<BaseAmountTabel>();
            if (SelectedInsurance != null)
            {
                foreach (var e in Context.BDController.GetAllTables())
                {
                    if (DateTime.Today.Year.Equals(e.Date.Year))
                    {
                        x?.Add(e);
                    }
                }
            }
            BaseAmountTabell = x;
            return BaseAmountTabell;
        }


        #region updatelists
        //Update all baseamounts for a specific sainsurance and for the delivery year.  
        public ICollection<BaseAmount> UpdateBaseAmount()
        {
            List<BaseAmount> x = new List<BaseAmount>();
            if (SelectedInsurance != null)
            {
                foreach (var e in Context.BDController.GetAllBaseAmount())
                {
                    if (SelectedInsurance.DeliveryDate.Year.Equals(e.Date.Year))
                    {
                        x?.Add(e);
                    }
                }
            }

            BaseAmounts1 = x;
            return BaseAmounts1;
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

        //Get all Insurances
        public ObservableCollection<Insurance> UpdateInsurance()
        {
            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.SAI != null)
                    x?.Add(e);
            }

            Insurances = x;

            return Insurances;
        }

        public ObservableCollection<InsuredPerson> UpdateInsuredPersons()
        {

            ObservableCollection<InsuredPerson> x = new ObservableCollection<InsuredPerson>();
            if (SelectedInsurance != null)
            {
                foreach (var p in Context.IPController.GetAllInsuredPersons())
                {
                    x?.Add(p);
                }
            }
            InsuredPersons = x;
            return InsuredPersons;
        }

        public ObservableCollection<Person> UpdatePersons()
        {
            ObservableCollection<Person> x = new ObservableCollection<Person>();
            foreach (var p in Context.ITController.GetAllPersons())
            {
                x?.Add(p);
            }
            Persons = x;
            return Persons;
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

        public void UpdateAC(string filter = "")
        {
            Insurances = new ObservableCollection<Insurance>();
            List<Insurance> x = new List<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.SAI != null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                    if (i.SerialNumber.Contains(filter) || i.PersonTaker.SocialSecurityNumber.Contains(filter) || i.TypeName.Contains(filter)
                        || i.InsuredID.SocialSecurityNumberIP.Contains(filter))
                        x.Add(i);
            }
            x?.ForEach(i => Insurances.Add(i));
        }
        #endregion

        #region list
        public List<int> BaseAmountsOP1 { get; set; }
        public List<int> BaseAmountsOP2 { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();

        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();
        
        private ObservableCollection<Insurance> _insurances;
        public ObservableCollection<Insurance> Insurances
        {
            get => _insurances;
            set
            {
                _insurances = value;
                OnPropertyChanged("Applications");
            }
        }
        public List<string> PersonTypes { get; set; }
        public ICollection<BaseAmount> BaseAmounts1 { get; set; }
        public ObservableCollection<SAInsurance> SAInsuranceTypes { get; set; }
        public List<string> PayMentForms { get; set; }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public ICollection<BaseAmountTabel> BaseAmountTabell { get; set; }
        #endregion

        #region properties

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


        //The selection of SAItype is responsible for populating the BaseAmountTabell collection based on their date.
        public SAInsurance SAIType
        {
            get => SelectedInsurance.SAI;

            set
            {
                SelectedInsurance.SAI = value;
                OnPropertyChanged("SAIType");
                if (SelectedInsurance.SAI != null)
                {
                    List<BaseAmountTabel> Bases = new List<BaseAmountTabel>();
                    foreach (var e in this.BaseAmountTabell = SelectedInsurance.SAI.Tabels)
                    {
                        if (!SelectedInsurance.DeliveryDate.Year.Equals(e.Date.Year))
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
        //Int for BaseAmountValue for Optionaltype: "Höjning av livförsäkring"
        public int BARLL
        {
            get => SelectedInsurance.BaseAmountValue;
            set
            {
                SelectedInsurance.BaseAmountValue = value;
                OnPropertyChanged("BARLL");
            }
        }
        //Int for BaseAmountValue for Optionaltype: "Månadsersättning vid långvarig sjukskrivning"
        public int BAmount
        {
            get => SelectedInsurance.BaseAmountValue2;
            set
            {
                SelectedInsurance.BaseAmountValue2 = value;
                OnPropertyChanged("BAmount");
            }
        }
        //Int for BaseAmountValue for Optionaltype: "Invaliditet vid olycksfall"
        public int BAmount1
        {
            get => SelectedInsurance.BaseAmountValue3;
            set
            {
                SelectedInsurance.BaseAmountValue3 = value;
                OnPropertyChanged("BAmount1");
            }
        }
        public int BaseAmount
        {
            get => SelectedInsurance.BaseAmountValue4;
            set
            {
                SelectedInsurance.BaseAmountValue4 = value;
                OnPropertyChanged("BaseAmount");
            }
        }
        public double AckValue
        {
            get => SelectedInsurance.AckValue;
            set
            {
                SelectedInsurance.AckValue = value;
                OnPropertyChanged("AckValue");
            }
        }
        public double AckValue2
        {
            get => SelectedInsurance.AckValue2;
            set
            {
                SelectedInsurance.AckValue2 = value;
                OnPropertyChanged("AckValue2");
            }
        }
        public double AckValue3
        {
            get => SelectedInsurance.AckValue3;
            set
            {
                SelectedInsurance.AckValue3 = value;
                OnPropertyChanged("AckValue3");
            }
        }
        public double AckValue4
        {
            get => SelectedInsurance.AckValue4;
            set
            {
                SelectedInsurance.AckValue4 = value;
                OnPropertyChanged("AckValue4");
            }
        }

        public ICollection<OptionalType> OptionalTypes 
        {
            get => SelectedInsurance.OptionalTypes;

            set
            {
                SelectedInsurance.OptionalTypes = UpdateOT();
                OnPropertyChanged("OptionalTypes");
            }
        
        }



        //public DateTime Date
        //{
        //    get => SelectedInsurance.DeliveryDate;  //kraschar bara här? varför? 
        //    set
        //    {
        //        SelectedInsurance.DeliveryDate = value;
        //        OnPropertyChanged("DeliveryDate");
        //    }
        //}

        private Insurance _selectedInsurance;
        public Insurance SelectedInsurance
        {
            get => _selectedInsurance;
            set
            {
                _selectedInsurance = value;
                OnPropertyChanged("SelectedInsurance");
            }
        }


        #endregion

    }
}
