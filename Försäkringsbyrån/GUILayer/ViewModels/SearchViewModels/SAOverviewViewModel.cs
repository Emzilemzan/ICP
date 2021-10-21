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
            OptionalTypes = UpdateOptionalType();
            OptionalTypes1 = UpdateOptionalType1();
            OptionalTypes2 = UpdateOptionalType2();
            SAInsuranceTypes = UpdateSA();
            BaseAmountTabell = UpdateBaseTable();
            BaseAmounts1 = UpdateBaseAmount();
            Insurances = UpdateInsurance();
            //OptionalType = OptionalTypes[0];
            //OptionalType1 = OptionalTypes1[0];
            //OptionalType2 = OptionalTypes2[0];
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

        #region method
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
                if (OptionalType1 != null && BAmount != 0)
                    b = OptionalType1;
                else
                {
                    MessageBox.Show("Om någon checkbox för tillval är i klickad måste du också ha fyllt i tillhörande uppgifter");
                }
            }
            if (CCheck == true)
            {
                if (OptionalType2 != null && BARLL != null)
                    c = OptionalType2;
                else
                {
                    MessageBox.Show("Om någon checkbox för tillval är i klickad måste du också ha fyllt i tillhörande uppgifter");
                }
            }
            List<OptionalType> y = new List<OptionalType>() { a, b, c };
            return y;
        }
        #endregion

        #region updatelists
        //Update all baseamounts for a specific sainsurance and for the delivery year.  
        public ICollection<BaseAmount> UpdateBaseAmount()
        {
            List<BaseAmount> x = new List<BaseAmount>();

            foreach (var e in Context.BDController.GetAllBaseAmount())
            {
                if (Date.Year.Equals(e.Date.Year))
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

        //Update all baseamounts for a specific sainsurances and for the delivery year.  
        public ICollection<BaseAmountTabel> UpdateBaseTable()
        {
            List<BaseAmountTabel> x = new List<BaseAmountTabel>();

            foreach (var e in Context.BDController.GetAllTables())
            {
                if (Date.Year.Equals(e.Date.Year))
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
        public ObservableCollection<Insurance> Insurances { get; set; }
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
                        if (!Date.Year.Equals(e.Date.Year))
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
        #region Optionaltype 
        private OptionalType _opType;
        public OptionalType OptionalType
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    if (SelectedInsurance.OptionalTypes.Contains(Op()))
                    {
                        return _opType;
                    }
                    else
                    {
                        return _opType = null;
                    }
                }
                else
                {
                    return _opType = null;
                }
            }
            set
            {
                _opType = value;
                OnPropertyChanged("OptionalType");
            }
        }

        private OptionalType _opType1;
        public OptionalType OptionalType1
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    if (SelectedInsurance.OptionalTypes.Contains(Op1()))
                    {
                        return _opType1;
                    }
                    else
                    {
                        return _opType1 = null;
                    }
                }
                else
                {
                    return _opType1 = null;
                }
            }
            set
            {
                _opType1 = value;
                OnPropertyChanged("OptionalType1");
            }

        }

        private OptionalType _opType2;
        public OptionalType OptionalType2
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    if (SelectedInsurance.OptionalTypes.Contains(Op2()))
                    {
                        return _opType2;
                    }
                    else
                    {
                        return _opType2 = null;
                    }
                }
                else
                {
                    return _opType2 = null;
                }
            }
            set
            {
                _opType2 = value;
                OnPropertyChanged("OptionalType2");
            }
        }
        public OptionalType Op()
        {
            return Context.IController.GetOPT(3);
        }
        public OptionalType Op1()
        {
            return Context.IController.GetOPT(1);
        }
        public OptionalType Op2()
        {
            return Context.IController.GetOPT(2);
        }
        #endregion
        
        public SalesMen AgentNo
        {
            get => SelectedInsurance.AgentNo;
            set
            {
                SelectedInsurance.AgentNo = value;
                OnPropertyChanged("AgentNo");
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
        public int BARLL
        {
            get => SelectedInsurance.BaseAmountValue;
            set
            {
                SelectedInsurance.BaseAmountValue = value;
                OnPropertyChanged("BARLL");
            }
        }
        public int BAmount
        {
            get => SelectedInsurance.BaseAmountValue2;
            set
            {
                SelectedInsurance.BaseAmountValue2 = value;
                OnPropertyChanged("BAmount");
            }
        }
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

        public DateTime Date
        {
            get
            {
                if (SelectedInsurance != null)
                {
                    return SelectedInsurance.DeliveryDate;
                }
                else
                {
                    return Date = DateTime.Now;
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
            get
            {
                if (OptionalType != null)
                    return _aCheck = true;
                else
                {
                    return _aCheck = false;
                }
            }
            set
            {
                _aCheck = value;
                OnPropertyChanged("ACheck");
            }
        }
        private bool _bCheck;
        public bool BCheck
        {
            get
            {
                if (OptionalType1 != null)
                    return _bCheck = true;
                else
                {
                    return _bCheck = false;
                }
            }
            set
            {
                _bCheck = value;
                OnPropertyChanged("BCheck");
            }
        }
        private bool _cCheck;
        public bool CCheck
        {
            get
            {
                if (OptionalType2 != null)
                    return _cCheck = true;
                else
                {
                    return _cCheck = false;
                }
            }
            set
            {
                _cCheck = value;
                OnPropertyChanged("CCheck");
            }
        }
        #endregion 
    }
}
