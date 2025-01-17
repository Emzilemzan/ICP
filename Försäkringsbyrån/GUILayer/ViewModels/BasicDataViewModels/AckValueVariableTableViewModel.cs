﻿using BussinessLayer;
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
    /// <summary>
    /// Viewmodel for managing the basedata: AckValueVariable
    /// </summary>
    public class AckValueVariableTableViewModel : BaseViewModel
    {
        public static readonly AckValueVariableTableViewModel Instance = new AckValueVariableTableViewModel();
        private AckValueVariableTableViewModel()
        {
            AckValues = UpdateAV();
            OptionalTypes = UpdateOptionalType();
            Date = DateTime.Today;
            LifeInsuranceTypes = UpdateLife();
        }
        #region listupdating
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
        public ObservableCollection<OptionalType> UpdateOptionalType()
        {
            ObservableCollection<OptionalType> x = new ObservableCollection<OptionalType>();
            foreach (var e in Context.IController.GetAllOPT())
            {
                    x?.Add(e);
            }
            OptionalTypes = x;
            return OptionalTypes;
        }
        #endregion
        #region Commands
        private ICommand _addBtn;
        public ICommand AddAckValueVariableTable_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddAckValueVariableTable(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public void EmptyAllChoices()
        {
            Check = true;
            Date = DateTime.Now;
            Instance.AckValue = string.Empty;
            LifInsurance = null;
            OptionalType = null;
            AckValueId = string.Empty;
        }
        private void AddAckValueVariableTable()
        {

            if (Instance._LifInsurance != null && Instance._optionalType != null)
            {
                Instance.LifInsurance = null;
                Instance.OptionalTypes = null;
                MessageBox.Show("Du får antingen lägga till grundbelopp till tillvals eller livförsäkring. Det går inte att ha valt båda");
            }
            else
            {
                if (Instance._date != null && Instance._ackValue != 0 && (Instance._LifInsurance != null || Instance._optionalType != null))
                {
                    AckValueVariable ackValueVariable = new AckValueVariable()

                    {
                        AckValue = Instance._ackValue,
                        Date = Instance._date,
                        LIFEID = Instance.LifInsurance,
                        OptionalTypeId = Instance.OptionalType

                    };
                    Context.BDController.CheckNbrOfAV(Instance.LifInsurance, Instance.OptionalType, Instance._date, ackValueVariable);

                    AckValues.Clear();
                    foreach (var o in Context.BDController.GetAllAckValues())
                    {
                        AckValues?.Add(o);
                    }
                    EmptyAllChoices();
                }
                else
                {
                    MessageBox.Show("Inget fält få lämnas tomt!");
                }
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveAckValue_Btn => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveAckValue();}));

        private void RemoveAckValue()
        {
            if (Instance.AckValueId != null)
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
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }

        }
        #endregion

        #region Properties
        public ObservableCollection<AckValueVariable> AckValues { get; set; }
        public ObservableCollection<LifeInsurance> LifeInsuranceTypes { get; set; }
        public ObservableCollection<OptionalType> OptionalTypes { get; set; }

        private LifeInsurance _LifInsurance;
        public LifeInsurance LifInsurance
        {
            get => _LifInsurance;
            set
            {
                _LifInsurance = value;

                OnPropertyChanged("LifInsurance");
            }
        }

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
                _ackValue = 0;
                if (double.TryParse(value, out _ackValue))
                {  }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("AckValue");
            }
        }

        private int _ackValueId;
        public string AckValueId
        {
            get => _ackValueId > 0 ? _ackValueId.ToString() : "";
            set
            {
                _ackValueId = 0;
                if (int.TryParse(value, out _ackValueId))
                {
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("AckValueId");
            }
        }

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
        #endregion
    }
}

