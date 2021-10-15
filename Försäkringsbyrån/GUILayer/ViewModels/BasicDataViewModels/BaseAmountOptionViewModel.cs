using BussinessLayer;
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

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class BaseAmountOptionViewModel : BaseViewModel
    {
        public static readonly BaseAmountOptionViewModel Instance = new BaseAmountOptionViewModel();

        private BaseAmountOptionViewModel()
        {
            BaseAmounts = UpdateBA();
            Date = DateTime.Today;
            OptionalTypes = UpdateOptionalType();
            LifeInsurances = UpdateLife();
        }
        private ObservableCollection<BaseAmount> UpdateBA()
        {
            ObservableCollection<BaseAmount> ba = new ObservableCollection<BaseAmount>();
            foreach (var o in Context.BDController.GetAllBaseAmount())
            {
                ba?.Add(o);
            }

            BaseAmounts = ba;
            return BaseAmounts;
        }

        public ObservableCollection<OptionalType> UpdateOptionalType()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            foreach (var e in Context.IController.GetAllOPT())
            {
                if(e.OptionalName != "Höjning av livförsäkring")
                    x?.Add(e);
            }

            OptionalTypes = x;
            return OptionalTypes;
        }

        public ObservableCollection<LifeInsurance> UpdateLife()
        {
            ObservableCollection<LifeInsurance> x = new ObservableCollection<LifeInsurance>();
            foreach (var e in Context.IController.GetAllLIFE())
            {
                x?.Add(e);
            }

            LifeInsurances = x;
            
            return LifeInsurances;
        }


        #region Commands
        private ICommand _addBtn;
        public ICommand AddBaseAmountOption_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddBaseAmountOption(); CanCreate(); }));
        }

        public bool CanCreate() => true;

        private void AddBaseAmountOption()
        {
            if(Instance._lifeInsurance != null && Instance._optionType != null)
            {
                Instance.LifeInsurance = null;
                Instance.OptionalTypeId = null;
                MessageBox.Show("Du får antingen lägga till grundbelopp till tillvals eller livförsäkring. Det går inte att ha valt båda");
            }
            else
            {
                if (Instance._date != null && Instance._baseAmount != 0 && (Instance._lifeInsurance != null || Instance._optionType != null))
                {
                    BaseAmount baseAmount = new BaseAmount()
                    {
                        Baseamount = Instance._baseAmount,
                        Date = Instance._date,
                        LIFEID = Instance.LifeInsurance,
                        OptionalTypeId = Instance._optionType

                    };
                    Context.BDController.AddBaseAmountOption(baseAmount);

                    MessageBox.Show("Grunddatan är uppdaterad");
                    BaseAmounts.Clear();
                    foreach (var o in Context.BDController.GetAllBaseAmount())
                    {
                        BaseAmounts?.Add(o);
                    }
                    Check = true;
                    Date = DateTime.Now;
                    Instance.BaseAmount = string.Empty;
                    Instance.LifeInsurance = null;
                    Instance.OptionalTypeId = null;
                }
                else
                {
                    MessageBox.Show("Inget fält få lämnas tomt!");
                }
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountOption_Btn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmountOption(); CanCreate(); }));
        }

        private void RemoveBaseAmountOption()
        {
            if (Instance._baseAmountOptionId != 0)
            {
                BaseAmount ot = Context.BDController.GetBaseAmount(_baseAmountOptionId);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingBaseAmountOption(Instance._baseAmountOptionId, ot);
                    BaseAmounts.Clear();
                    foreach (var t in Context.BDController.GetAllBaseAmount())
                    {
                        BaseAmounts?.Add(t);
                    }
                }
                else
                {
                    MessageBox.Show("Grunddatan togs inte bort.");
                }
                Check = true;
                Instance.BaseAmountId = string.Empty;
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<BaseAmount> BaseAmounts { get; set; }
        public ObservableCollection<OptionalType> OptionalTypes { get; set; }
        public ObservableCollection<LifeInsurance> LifeInsurances { get; set; }

        private bool _check;
        public bool Check
        {
            get => _check;
            set
            {
                _check = value;
                OnPropertyChanged("Check");
            }
        }

        //Vilket tillval 
        private OptionalType _optionType;
        public OptionalType OptionalTypeId
        {
            get => _optionType;
            set
            {
                _optionType = value;
                OnPropertyChanged("OptionalTypeId");
            }
        }
        

        //Vilken försäkringstyp 
        private LifeInsurance _lifeInsurance;
        public LifeInsurance LifeInsurance
        {
            get => _lifeInsurance;
            set
            {
                _lifeInsurance = value;
                OnPropertyChanged("LifeInsurance");
            }
        }

        //Datum
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

        //Grundbelopp
        private int _baseAmount;
        public string BaseAmount
        {
            get => _baseAmount > 0 ? _baseAmount.ToString() : "";
            set
            {
                    if (int.TryParse(value, out _baseAmount))
                        OnPropertyChanged("BaseAmount");
                else if(Check == false)
                    {
                    MessageBox.Show("Grundbeloppet måste vara en siffra");
                    }
            }
        }

        //ID för att ta bort
        private int _baseAmountOptionId;
        public string BaseAmountId
        {
            get => _baseAmountOptionId > 0 ? _baseAmountOptionId.ToString() : "";
            set
            {
                if (Check == false)
                {
                    if (int.TryParse(value, out _baseAmountOptionId))
                    {
                        OnPropertyChanged("BaseAmountId");
                    }
                }
            }
        }
        #endregion

    }

}
