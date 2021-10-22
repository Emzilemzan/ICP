﻿using GUILayer.Commands;
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
    public class SAOverviewViewModel : BaseViewModel
    {
        public static readonly SAOverviewViewModel Instance = new SAOverviewViewModel();

        private SAOverviewViewModel()
        {
            UpdateAC();
        }

        #region command 
        private ICommand _updateBtn;

        public ICommand UpdateBtn => _updateBtn ?? (_updateBtn = new RelayCommand(x => { Update(); }));
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
               && BaseTabel != null)
            {
                SelectedInsurance.PaymentForm = PaymentForm;
                SelectedInsurance.OptionalTypes = UpdateOT();
                SelectedInsurance.AgentNo = AgentNo;
                SelectedInsurance.SerialNumber = SerialNumber;
                SelectedInsurance.BaseAmountValue = BARLL;
                SelectedInsurance.BaseAmountValue2 = BAmount;
                SelectedInsurance.BaseAmountValue3 = BAmount1;
                SelectedInsurance.BaseAmountValue4 = Instance.BaseTabel.BaseAmount;
                SelectedInsurance.AckValue = Context.BDController.CountAckvalueOt(SelectedInsurance.DeliveryDate, Context.IController.GetOPT(2), BARLL);
                SelectedInsurance.AckValue2 = Context.BDController.CountAckvalueOt(SelectedInsurance.DeliveryDate, Context.IController.GetOPT(3), BAmount);
                SelectedInsurance.AckValue3 = Context.BDController.CountAckvalueOt(SelectedInsurance.DeliveryDate, Context.IController.GetOPT(1), BAmount1);
                SelectedInsurance.AckValue4 = Instance.BaseTabel.AckValue;
                Context.IController.Edit(SelectedInsurance);
                MessageBox.Show("Försäkring är uppdaterad!", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                Insurances.Clear();
                foreach (var i in Context.IController.GetAllInsurances())
                {
                    if (i.SAI != null)
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
                            if (i.SAI != null)
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
        //Update all baseamounts for a specific sainsurances and for the delivery year.  
        public ICollection<BaseAmountTabel> UpdateBaseTable()
        {
            List<BaseAmountTabel> x = new List<BaseAmountTabel>();
            if (SelectedInsurance != null)
            {
                foreach (var e in Context.BDController.GetAllTables())
                {
                    if (DateTime.Today.Year.Equals(e.Date.Year) && SelectedInsurance.SAI.Tabels.Contains(e))
                    {
                        x?.Add(e);
                    }
                }
            }
            BaseAmountTabell = x;
            return BaseAmountTabell;
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
                if (e.SAI != null)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                {
                    if (i.SerialNumber.Contains(filter) || i.PersonTaker.SocialSecurityNumber.Contains(filter) || i.TypeName.Contains(filter)
                        || i.InsuredID.SocialSecurityNumberIP.Contains(filter) || i.InsuredID.LastName.Contains(filter) || i.PersonTaker.Lastname.Contains(filter)
                        || i.SAI.SAInsuranceType.Contains(filter) || i.BaseAmountValue4.ToString().Contains(filter) || i.AgentNo.AgentNumber.ToString().Contains(filter))
                    {
                        x.Add(i);
                    }
                }
            }
            x?.ForEach(i => Insurances.Add(i));
        }

        public void UpdateComboBoxes()
        {
            BaseAmountsOP1 = new List<int>() { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000 };
            OnPropertyChanged("BaseAmountsOP1");
            BaseAmountsOP2 = new List<int>() { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
            OnPropertyChanged("BaseAmountsOP2");
            SalesMens = UpdateSM();
            OnPropertyChanged("SalesMens");
            PayMentForms = new List<string>() { "Helår", "Halvår", "Kvartal", "Månad" };
            OnPropertyChanged("PayMentForms");
            SAInsuranceTypes = UpdateSA();
            OnPropertyChanged("SAInsuranceTypes");
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
                OnPropertyChanged("Insurances");
            }
        }
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
            get
            {
                if (SelectedInsurance != null)
                    return SelectedInsurance.BaseAmountValue;
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.BaseAmountValue = value;
                }
                OnPropertyChanged("BARLL");
            }
        }
        //Int for BaseAmountValue for Optionaltype: "Månadsersättning vid långvarig sjukskrivning"
        public int BAmount
        {
            get
            {
                if (SelectedInsurance != null)
                    return SelectedInsurance.BaseAmountValue2;
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.BaseAmountValue2 = value;
                }
                OnPropertyChanged("BAmount");
            }
        }
        //Int for BaseAmountValue for Optionaltype: "Invaliditet vid olycksfall"
        public int BAmount1
        {
            get
            {
                if (SelectedInsurance != null)
                    return SelectedInsurance.BaseAmountValue3;
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.BaseAmountValue3 = value;
                }
                OnPropertyChanged("BAmount1");
            }
        }
        public int BaseAmount
        {
            get
            {
                if (SelectedInsurance != null)
                    return SelectedInsurance.BaseAmountValue4;
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedInsurance != null)
                {
                    SelectedInsurance.BaseAmountValue4 = value;
                }
                OnPropertyChanged("BaseAmount");
            }
        }

        private Insurance _selectedInsurance;
        public Insurance SelectedInsurance
        {
            get => _selectedInsurance;
            set
            {
                _selectedInsurance = value;
                BaseAmountsOP1 = null;
                BaseAmountsOP2 = null;
                BARLL = 0;
                PayMentForms = null;
                SalesMens = null;
                BaseAmountTabell = null;
                OnPropertyChanged("SelectedInsurance");
                if (SelectedInsurance != null)
                {
                    UpdateComboBoxes();
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
                    BaseAmountTabel c = new BaseAmountTabel();
                    foreach (var b in SelectedInsurance.SAI.Tabels)
                    {
                        if (b.BaseAmount.Equals(SelectedInsurance.BaseAmountValue4))
                        {
                            c = b;
                        }
                    }
                    BaseTabel = c;
                    OnPropertyChanged("BaseTabel");
                }

            }

        }
    }

    #endregion

}

