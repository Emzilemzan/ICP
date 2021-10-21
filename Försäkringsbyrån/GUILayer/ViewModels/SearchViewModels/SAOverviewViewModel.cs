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
    public class SAOverviewViewModel: BaseViewModel
    {
        public static readonly SAOverviewViewModel Instance = new SAOverviewViewModel();

        private SAOverviewViewModel()
        {
            BaseAmountsOP1 = new List<int>() { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000 };
            BaseAmountsOP2 = new List<int>() { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
            SalesMens = UpdateSM();
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OptionalTypes = UpdateOptionalType();
            OptionalTypes1 = UpdateOptionalType1();
            OptionalTypes2 = UpdateOptionalType2();
            SAInsuranceTypes = UpdateSA();
            BaseAmountTabell = UpdateBaseTable();
            BaseAmounts1 = UpdateBaseAmount();
            OptionalType = OptionalTypes[0];
            OptionalType1 = OptionalTypes1[0];
            OptionalType2 = OptionalTypes2[0];
        }
        #region command 
        private ICommand _updateBtn;

        public ICommand UpdateBtn
        {
            get => _updateBtn ?? (_updateBtn = new RelayCommand(x => { Update(); }));
        }

        public void Update()
        {

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

        #region updatelists
        //Update all baseamounts for a specific sainsurance and for the delivery year.  
        public ICollection<BaseAmount> UpdateBaseAmount()
        {
            List<BaseAmount> x = new List<BaseAmount>();

            foreach (var e in Context.BDController.GetAllBaseAmount())
            {
                if (DeliveryDate.Year.Equals(e.Date.Year))
                {
                    x?.Add(e);
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

        //public ObservableCollection<InsuredPerson> UpdateInsuredPersons()
        //{

        //    ObservableCollection<InsuredPerson> x = new ObservableCollection<InsuredPerson>();
        //    if (SelectedInsurance != null)
        //    {
        //        foreach (var ip in Context.IPController.GetInsuranceTakerIPS(SelectedInsurance))
        //        {
        //            x?.Add(ip);
        //        }
        //    }
        //    InsuredPersons = x;
        //    return InsuredPersons;
        //}

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

        //Update all baseamounts for a specific sainsurances and for the delivery year.  
        public ICollection<BaseAmountTabel> UpdateBaseTable()
        {
            List<BaseAmountTabel> x = new List<BaseAmountTabel>();

            foreach (var e in Context.BDController.GetAllTables())
            {
                if (DeliveryDate.Year.Equals(e.Date.Year))
                {
                    x?.Add(e);
                }
            }
            BaseAmountTabell = x;
            return BaseAmountTabell;
        }

        #endregion


        #region list
        public List<int> BaseAmountsOP1 { get; set; }
        public List<int> BaseAmountsOP2 { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();

        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();
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

        private SAInsurance _Stype;
        //The selection of _Stype is responsible for populating the BaseAmountTabell collection based on their date.
        public SAInsurance SAIType
        {
            get => _Stype;
            set
            {
                _Stype = value;
                OnPropertyChanged("SAIType");
                    if (_Stype != null)
                    {
                        List<BaseAmountTabel> Bases = new List<BaseAmountTabel>();
                        foreach (var e in this.BaseAmountTabell = _Stype.Tabels)
                        {
                            if (!DeliveryDate.Year.Equals(e.Date.Year))
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
                else
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
