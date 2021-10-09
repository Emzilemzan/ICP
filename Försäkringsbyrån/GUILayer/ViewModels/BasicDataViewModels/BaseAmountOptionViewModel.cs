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

        BasedataController controller = new BasedataController();

        private BaseAmountOptionViewModel()
        {
            Date = DateTime.Today;
            OptionalTypes = new List<OptionalType>() { new OptionalType (1, "Invaliditet vid olycksfall"), new OptionalType(2, "Höjning av livförsäkring"), new OptionalType(3, "Månadsersättning vid långvarig sjukskrivning") };
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
            //if (Instance._optionType !=null & Instance._date !=null && Instance._baseAmount !=0)
            //{
            //    OptionalType optionalType = new OptionalType()
            //    {
            //        OptionalTypeId = Instance._optionType,
            //        Date = Instance._date,
            //        BaseAmount = Instance._baseAmount

            //    };
            //    Context.BDController.AddBaseAmountOption(optionalType);

            //    MessageBox.Show("Grunddatan är uppdaterad");
            //    OptionalTypes.Clear();
            //    foreach (var o in Context.BDController.GetAllOptionalTypes())
            //    {
            //        OptionalTypes?.Add(o);
            //    }
            //    Date = DateTime.Now;
            //    BaseAmount = string.Empty;
            //}
            //else
            //{
            //    MessageBox.Show("Inget fält få lämnas tomt!");
            //}
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
                OptionalType ot = Context.BDController.GetOptionalType(_baseAmountOptionId);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingBaseAmountOption(Instance._baseAmountOptionId, ot);
                    OptionalTypes.Clear();
                    foreach (var t in Context.BDController.GetAllOptionalTypes())
                    {
                        OptionalTypes?.Add(t);
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

        #endregion

        #region Properties

        public List<OptionalType> OptionalTypes { get; set; }

        //Vilket tillval
        private OptionalType _optionType;
        public OptionalType OptionalTypeId
        {
            get => _optionType;
            set
            {
                _optionType = value;

                OnPropertyChanged("OptionType");
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

        //ID för att ta bort
        private int _baseAmountOptionId;
        public string BaseAmountOptionId
        {
            get => _baseAmountOptionId > 0 ? _baseAmountOptionId.ToString() : "";
            set
            {
                if (int.TryParse(value, out _baseAmountOptionId))
                {
                    OnPropertyChanged("BaseAmountId");
                }
            }
        }

        #endregion

    }
}
