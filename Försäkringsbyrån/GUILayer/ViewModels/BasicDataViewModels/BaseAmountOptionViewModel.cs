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
    /// Viewmodel for managing the basedata: BaseAmountOption --> for Lifeinsurance, not optionaltype. 
    /// </summary>
    public class BaseAmountOptionViewModel : BaseViewModel
    {
        public static readonly BaseAmountOptionViewModel Instance = new BaseAmountOptionViewModel();

        private BaseAmountOptionViewModel()
        {
            BaseAmounts = UpdateBA();
            Date = DateTime.Today;
            LifeInsurances = UpdateLife();
        }
        #region update
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
        #endregion
        #region Commands
        private ICommand _addBtn;
        public ICommand AddBaseAmountOption_Btn => _addBtn ?? (_addBtn = new RelayCommand(x => { AddBaseAmountOption();}));

        private void AddBaseAmountOption()
        {
            if (Instance._date != null && Instance._baseAmount != 0 && Instance._lifeInsurance != null)
            {
                BaseAmount baseAmount = new BaseAmount()
                {
                    Baseamount = Instance._baseAmount,
                    Date = Instance._date,
                    LIFEID = Instance.LifeInsurance,

                };
                Context.BDController.CheckNbrOfBA(Instance.LifeInsurance, Instance._date, baseAmount);

                BaseAmounts.Clear();
                foreach (var o in Context.BDController.GetAllBaseAmount())
                {
                    BaseAmounts?.Add(o);
                }
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Inget fält få lämnas tomt!");
            }

        }

        public void EmptyAllChoices()
        {
            Check = true;
            Date = DateTime.Now;
            Instance.BaseAmount = string.Empty;
            LifeInsurance = null;
            BaseAmountId = string.Empty;
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountOption_Btn=> remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmountOption();}));
        private void RemoveBaseAmountOption()
        {
            if (Instance.BaseAmountId != null)
            {
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingBaseAmountOption(Instance._baseAmountOptionId);
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

        private int _baseAmount;
        public string BaseAmount
        {
            get => _baseAmount > 0 ? _baseAmount.ToString() : "";
            set
            {
                _baseAmount = 0;
                if (int.TryParse(value, out _baseAmount))
                { }
                else if (Check == false)
                {
                    MessageBox.Show("Grundbeloppet måste vara en siffra");
                }
                OnPropertyChanged("BaseAmount");
            }
        }

        private int _baseAmountOptionId;
        public string BaseAmountId
        {
            get => _baseAmountOptionId > 0 ? _baseAmountOptionId.ToString() : "";
            set
            {
                _baseAmountOptionId = 0;
                if (int.TryParse(value, out _baseAmountOptionId))
                {
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("BaseAmountId");
            }
        }
        #endregion
    }
}
