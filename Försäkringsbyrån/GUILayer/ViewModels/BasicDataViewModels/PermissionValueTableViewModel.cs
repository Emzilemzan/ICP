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
    /// Viewmodel for managing the basedata: CommissionsShare. Used for commission. 
    /// </summary>
    public class PermissionValueTableViewModel : BaseViewModel
    {
        public static readonly PermissionValueTableViewModel Instance = new PermissionValueTableViewModel();
        private PermissionValueTableViewModel()
        {
            CommissionShares = UpdateCS();
            YearsString = GetYearsString();
        }
        private ObservableCollection<ComissionShare> UpdateCS()
        {
            ObservableCollection<ComissionShare> cs = new ObservableCollection<ComissionShare>();
            foreach (var o in Context.BDController.GetAllCommissionShares())
            {
                cs?.Add(o);
            }
            CommissionShares = cs;
            return CommissionShares;
        }

        public List<int> YearsInt { get; set; }
        public List<int> GetYearsInt()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }

        public List<string> YearsString { get; set; }

        public List<string> GetYearsString()
        {
            YearsInt = GetYearsInt();
            List<string> strings = new List<string>();
            foreach (var i in YearsInt)
            {
                strings.Add(i.ToString());
            }
            return strings;
        }

        public void EmptyAllChoices()
        {
            Check = true;
            CalendarYear = null;
            TotalMaxAckValue = string.Empty;
            TotalMinAckValue = string.Empty;
            CommissionShareChildren = string.Empty;
            ComissionShareAdults = string.Empty;
            PAId = string.Empty;
        }

        #region Commands
        private ICommand _addBtn;
        public ICommand AddCommissionShare_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddCommissionShare(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        private void AddCommissionShare()
        {
            if (Instance.CalendarYear != null && Instance._totalMinAckValue != 0 && Instance._totalMaxAckValue != 0 && Instance._commissionShareChildren != 0 && Instance._comissionShareAdults != 0)
            {
                ComissionShare comissionShare = new ComissionShare()
                {
                    CalenderYear = int.Parse(Instance._calendarYear),
                    TotalMinAckValue = Instance._totalMinAckValue,
                    TotalMaxAckValue = Instance._totalMaxAckValue,
                    CommissionShareChildren = Instance._commissionShareChildren,
                    ComissionShareAdults = Instance._comissionShareAdults
                };
                Context.BDController.CheckNbrOfCS(comissionShare, int.Parse(Instance._calendarYear));
                CommissionShares.Clear();
                foreach (var o in Context.BDController.GetAllCommissionShares())
                {
                    CommissionShares?.Add(o);
                }
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Inget fält få lämnas tomt!");
            }

        }


        private ICommand _removeBtn;
        public ICommand RemoveCommissionShare_Btn
        {
            get => _removeBtn ?? (_removeBtn = new RelayCommand(x => { RemoveCommissionShare(); CanCreate(); }));
        }

        private void RemoveCommissionShare()
        {
            if (Instance._pAID != 0)
            {
                ComissionShare cs = Context.BDController.GetCommissionShare(_pAID);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingCommissionShare(Instance._pAID, cs);
                    CommissionShares.Clear();
                    foreach (var t in Context.BDController.GetAllCommissionShares())
                    {
                        CommissionShares?.Add(t);
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
        public ObservableCollection<ComissionShare> CommissionShares { get; set; }

        
        private string _calendarYear;
        public string CalendarYear
        {
            get => _calendarYear;
            set
            {
                _calendarYear = value;
                OnPropertyChanged("CalendarYear");
            }
        }

        private int _totalMinAckValue;
        public string TotalMinAckValue
        {
            get => _totalMinAckValue > 0 ? _totalMinAckValue.ToString() : "";
            set
            {
                _totalMinAckValue = 0;
                if (int.TryParse(value, out _totalMinAckValue))
                { }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("TotalMinAckValue");
            }
        }

        private int _totalMaxAckValue;
        public string TotalMaxAckValue
        {
            get => _totalMaxAckValue > 0 ? _totalMaxAckValue.ToString() : "";
            set
            {
                _totalMaxAckValue = 0;
                if (int.TryParse(value, out _totalMaxAckValue))
                { }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("TotalMaxAckValue");
            }
        }

        private double _commissionShareChildren;
        public string CommissionShareChildren
        {
            get => _commissionShareChildren > 0 ? _commissionShareChildren.ToString() : "";
            set
            {
                _commissionShareChildren = 0;
                if (double.TryParse(value, out _commissionShareChildren))
                { }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }

                OnPropertyChanged("CommissionShareChildren");
            }
        }

        private double _comissionShareAdults;
        public string ComissionShareAdults
        {
            get => _comissionShareAdults > 0 ? _comissionShareAdults.ToString() : "";
            set
            {
                _comissionShareAdults = 0;
                if (double.TryParse(value, out _comissionShareAdults))
                { }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("ComissionShareAdults");
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
        private int _pAID;
        public string PAId
        {
            get => _pAID > 0 ? _pAID.ToString() : "";
            set
            {
                _pAID = 0;
                if (int.TryParse(value, out _pAID))
                {
                    
                }
                else if(Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("PAId");
            }
        }
        #endregion

    }
}
