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
    public class AckValueVariableTableViewModel: BaseViewModel

    {
        public static readonly AckValueVariableTableViewModel Instance = new AckValueVariableTableViewModel();

   
        private AckValueVariableTableViewModel()
        {
            AckValues = UpdateAV();
            Date = DateTime.Today;
            SAInsuranceTypes = new List<SAInsurance>() { new SAInsurance(1, "Sjuk- och olycksfallsförsäkring för barn"), new SAInsurance(2, "Sjuk- och olycksfallsförsäkring för vuxen") };
            OptionalTypes = new List<OptionalType>() { new OptionalType(1, "Invaliditet vid olycksfall"), new OptionalType(2, "Höjning av livförsäkring"), new OptionalType(3, "Månadsersättning vid långvarig sjukskrivning") };
            
            //Ska Livförsäkring vara med här också? Se BaseAmountOptionViewModel
        }

       private ObservableCollection<AckValueVariable> UpdateAV()
        {
            ObservableCollection<AckValueVariable> av = new ObservableCollection<AckValueVariable>();
            foreach (var o in Context.BDController.GetAllAckValues())
            {
                av?.Add(o);
            }

            AckValues = av;
            return AckValues;
        }

        #region Commands
        private ICommand _addBtn;
        public ICommand AddAckValueVariableTable_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddAckValueVariableTable(); CanCreate(); }));
        }
        public bool CanCreate() => true;

         private void AddAckValueVariableTable()
        {
            if (Instance._date != null && Instance._ackValue != 0 && (Instance._sAInsurance != null || Instance._optionalType != null))
            {
                AckValueVariable ackValueVariable = new AckValueVariable()
                {
                    Date = Instance._date,
                    AckValue = Instance._ackValue,
                    SAInsurance = Instance._sAInsurance,
                    OptionalTypeId = Instance._optionalType

                };
                Context.BDController.AddAckValue(ackValueVariable);
                MessageBox.Show("Grunddatan är uppdaterad");
                AckValues.Clear();
                foreach (var o in Context.BDController.GetAllAckValues())
                {
                    AckValues?.Add(o);
                }
                Date = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Inget fält få lämnas tomt!");
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveAckValue_Btn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveAckValue(); CanCreate(); }));
        }
        private void RemoveAckValue()
        {
            if (Instance.AckValueId !=null)
            {
                AckValueVariable av = Context.BDController.GetAckValue(_ackValueId);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingAckValue(Instance._ackValueId, av);
                    AckValues.Clear();
                    foreach (var t in Context.BDController.GetAllAckValues())
                    {
                        AckValues?.Add(t);
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
        public ObservableCollection<AckValueVariable> AckValues { get; set; }

        public List<SAInsurance> SAInsuranceTypes { get; set; }
        public List<OptionalType> OptionalTypes { get; set; }

        //Vilket tillval 
        private SAInsurance _sAInsurance;
        public SAInsurance SAInsurance
        {
            get => _sAInsurance;
            set
            {
                _sAInsurance = value;

                OnPropertyChanged("SAInsurance");
            }
        }

        //Vilken försäkringstyp 
        private OptionalType _optionalType;
        public OptionalType OptionalType
        {
            get => _optionalType;
            set
            {
                _optionalType = value;

                OnPropertyChanged("OptionalType");
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

        //ID för att ta bort
        private int _ackValueId;
        public string AckValueId
        {
            get => _ackValueId > 0 ? _ackValueId.ToString() : "";
            set
            {
                if (int.TryParse(value, out _ackValueId))
                {
                    OnPropertyChanged("AckValueId");
                }
            }
        }

        #endregion

    }
}
