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
   public class PermissionValueTableViewModel: BaseViewModel
    {
        public static readonly PermissionValueTableViewModel Instance = new PermissionValueTableViewModel();
        
        private PermissionValueTableViewModel()
        {
            CommissionShares = UpdateCS();
            Years = GetYears();
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


        public List<int> Years { get; set; }
        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
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
            if (Instance._calendarYear != 0 && Instance._totalMinAckValue != 0 && Instance._totalMaxAckValue != 0 && Instance._commissionShareChildren != 0 && Instance._comissionShareAdults != 0)
            {
                ComissionShare comissionShare = new ComissionShare()
                {
                    CalenderYear = Instance._calendarYear,
                    TotalMinAckValue = Instance._totalMinAckValue,
                    TotalMaxAckValue = Instance._totalMaxAckValue,
                    CommissionShareChildren = Instance._commissionShareChildren,
                    ComissionShareAdults = Instance._comissionShareAdults
                };
                Context.BDController.AddCommissionShare(comissionShare);

                MessageBox.Show("Grunddatan är uppdaterad");
                CommissionShares.Clear();
                foreach (var o in Context.BDController.GetAllCommissionShares())
                {
                    CommissionShares?.Add(o);
                }
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
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }

        }
        #endregion

        #region Properties
        public ObservableCollection<ComissionShare> CommissionShares { get; set; }

        //Vilket år 
        private int _calendarYear;
        public int CalendarYear
        {
            get => _calendarYear;
            set
            {
                _calendarYear = value;

                OnPropertyChanged("CalendarYear");
            }
        }

        //Vilket min. Ack.Värde 
        private int _totalMinAckValue;
        public int TotalMinAckValue
        {
            get => _totalMinAckValue;
            set
            {
                _totalMinAckValue = value;

                OnPropertyChanged("TotalMinAckValue");
            }
        }

        //Vilket max. Ack.Värde 
        private int _totalMaxAckValue;
        public int TotalMaxAckValue
        {
            get => _totalMaxAckValue;
            set
            {
                _totalMaxAckValue = value;

                OnPropertyChanged("TotalMaxAckValue");
            }
        }

        //Vilket provisionsandel barn
        private int _commissionShareChildren;
        public int CommissionShareChildren
        {
            get => _commissionShareChildren;
            set
            {
                _commissionShareChildren = value;

                OnPropertyChanged("CommissionShareChildren");
            }
        }

        //Vilket provisionsandel vuxen
        private int _comissionShareAdults;
        public int ComissionShareAdults
        {
            get => _comissionShareAdults;
            set
            {
                _comissionShareAdults = value;

                OnPropertyChanged("ComissionShareAdults");
            }
        }

        //ID för att ta bort
        private int _pAID;
        public string PAId
        {
            get => _pAID > 0 ? _pAID.ToString() : "";
            set
            {
                if (int.TryParse(value, out _pAID))
                {
                    OnPropertyChanged("PAId");
                }
            }
        }
        #endregion

    }
}
