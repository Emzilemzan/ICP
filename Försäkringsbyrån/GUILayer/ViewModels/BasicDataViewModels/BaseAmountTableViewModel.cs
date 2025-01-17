﻿using GUILayer.Commands;
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
    /// <summary>
    /// Viewmodel for managing the basedata: BaseAmountTable --> for SAinsurance.
    /// </summary>
    public class BaseAmountTableViewModel : BaseViewModel
    {
        public static readonly BaseAmountTableViewModel Instance = new BaseAmountTableViewModel();
        private BaseAmountTableViewModel()
        {
            Tabels = UpdateTabels();
            Date = DateTime.Today;
            SAInsuranceTypes = UpdateSA();
            SAID = SAInsuranceTypes[0];
        }
        #region commands

        private ICommand _addBtn;
        public ICommand AddBaseAmountTableValue_Btn => _addBtn ?? (_addBtn = new RelayCommand(x => { AddBaseAmountTable();}));

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
                Context.BDController.CheckNbrOfBASA(Instance.SAID, Instance.Date, baseAmountTabel);

                Tabels.Clear();
                foreach (var t in Context.BDController.GetAllTables())
                {
                    Tabels?.Add(t);
                }
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Inget fält får lämnas tomt!");
            }
        }
        public void EmptyAllChoices()
        {
            Check = true;
            AckValue = string.Empty;
            BaseAmount = string.Empty;
            SAID = null;
            Date = DateTime.Now;
            BaseAmountId = string.Empty;
        }
        private void RemoveBaseAmountTable()
        {
            if (Instance.BaseAmountId != null)
            {
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingTable(Instance._baseAmountId);
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
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountTableValue_Btn => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmountTable(); }));
        #endregion
        #region Update
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

        public ObservableCollection<SAInsurance> UpdateSA()
        {
            ObservableCollection<SAInsurance> x = new ObservableCollection<SAInsurance>
            {
                new SAInsurance() { SAID = 0, SAInsuranceType = "inget" }
            };
            foreach (var e in Context.IController.GetAllSAI())
            {
                x?.Add(e);
            }

            SAInsuranceTypes = x;

            return SAInsuranceTypes;
        }
        #endregion
        #region Properties

        public ObservableCollection<BaseAmountTabel> Tabels { get; set; }

        private int _baseAmountId;
        public string BaseAmountId
        {
            get => _baseAmountId > 0 ? _baseAmountId.ToString() : "";
            set
            {
                _baseAmountId = 0;
                if (int.TryParse(value, out _baseAmountId))
                {
                    
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("BaseAmountId");
            }
        }


        private int _baseAmount;
        public string BaseAmount
        {
            get => _baseAmount > 0 ? _baseAmount.ToString() : "";
            set
            {
                _baseAmount = 0;
                if (int.TryParse(value, out _baseAmount))
                {  }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("BaseAmount");
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
                { 

                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("AckValue");

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
            get => _sAInsurance;
            set
            {
                _sAInsurance = value;

                OnPropertyChanged("SAInsurance");
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
        public ObservableCollection<SAInsurance> SAInsuranceTypes { get; set; }

        #endregion
    }
}
