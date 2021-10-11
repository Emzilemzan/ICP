using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class BaseAmountTableViewModel: BaseViewModel 
    {
        public static readonly BaseAmountTableViewModel Instance = new BaseAmountTableViewModel();
        private BaseAmountTableViewModel()
        {
            Tabels = UpdateTabels();
            Date = DateTime.Today;
            SAInsuranceTypes = new List<SAInsurance>() { new SAInsurance(1, "Sjuk- och olycksfallsförsäkring för barn"), new SAInsurance(2, "Sjuk- och olycksfallsförsäkring för vuxen") };
        }
        #region commands
        private ICommand _addBtn;
        public ICommand AddBaseAmountTableValue_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddBaseAmountTable(); CanCreate(); }));
        }

        public bool CanCreate() => true;

        private void AddBaseAmountTable()
        {
            if (Instance._baseAmount != 0 && Instance._ackValue != 0 && Instance.Date != null && Instance.SAID != null)
            {
                BaseAmountTabel baseAmountTabel = new BaseAmountTabel()
                {
                    BaseAmount = Instance._baseAmount,
                    AckValue = Instance._ackValue,
                    Date = Instance._date,
                    SAID = Instance.SAID,
                };
                Context.BDController.AddBaseAmountTable(baseAmountTabel);
 
                MessageBox.Show("Grunddatan är uppdaterad");
                Tabels.Clear();
                foreach (var t in Context.BDController.GetAllTables())
                {
                    Tabels?.Add(t);
                }
                AckValue = string.Empty;
                BaseAmount = string.Empty;
                SAID = null;
                Date = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Inget fält får lämnas tomt!");
            }
        }
        //Method that removes baseamounttabel based on what id it has in database. 
        //Needs a control here, if it exists in insurance you can't delete it. 
        private void RemoveBaseAmountTable()
        {
            if (Instance._baseAmountId != 0)
            {
                BaseAmountTabel bdt = Context.BDController.GetBaseAmountTable(_baseAmountId);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingTable(Instance._baseAmountId, bdt);
                    Tabels.Clear();
                    foreach (var t in Context.BDController.GetAllTables())
                    {
                        Tabels?.Add(t);
                    }
                }
                else
                {
                    MessageBox.Show("Grunddatan togs inte bort.");
                }
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountTableValue_Btn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmountTable(); CanCreate(); }));
        }


        #endregion

        private ObservableCollection<BaseAmountTabel> UpdateTabels()
        {
            ObservableCollection<BaseAmountTabel> x = new ObservableCollection<BaseAmountTabel>();
            foreach (var t in Context.BDController.GetAllTables())
            {
                x?.Add(t);
            }
            Tabels = x;
            return Tabels;
        }

        #region Properties

        public ObservableCollection<BaseAmountTabel> Tabels { get; set; }

        private int _baseAmountId;
        public string BaseAmountId
        {
            get => _baseAmountId > 0 ? _baseAmountId.ToString() : "";
            set
            {
                if (int.TryParse(value, out _baseAmountId))
                {
                    OnPropertyChanged("BaseAmountId");
                }
            }
        }
     

        private double _baseAmount;
        public string BaseAmount
        {
            get => _baseAmount > 0 ? _baseAmount.ToString() : "";
            set
            {
                if (double.TryParse(value, out _baseAmount))
                { OnPropertyChanged("BaseAmount"); }
            }
        }
        
        private double _ackValue;
        public string AckValue
        {
            get => _ackValue > 0 ? _ackValue.ToString() : "";
            set
            {
                if (double.TryParse(value, out _ackValue))
                { OnPropertyChanged("AckValue"); }
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date != null ? _date : DateTime.Now;
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public DateTime Today => DateTime.Today.Date;

        private SAInsurance _sAInsurance;
        public SAInsurance SAID
        {
            get =>  _sAInsurance;
            set
            {
                _sAInsurance = value;
                
                OnPropertyChanged("SAInsurance");
            }
        }


        public List<SAInsurance> SAInsuranceTypes { get; set; }

        #endregion
    }
}
