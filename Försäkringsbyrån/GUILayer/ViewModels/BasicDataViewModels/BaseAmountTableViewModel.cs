using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class BaseAmountTableViewModel: BaseViewModel 
    {
        public static readonly BaseAmountTableViewModel Instance = new BaseAmountTableViewModel();
        private BaseAmountTableViewModel()
        {
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
            // Code to actually add baseamount to the database.
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountTableValue_Btn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmount(); CanCreate(); }));
        }

        private void RemoveBaseAmount()
        {
            // Code to actually add baseamount to the database.
        }
        #endregion
        #region Properties

        private int _baseAmountId;
        public int BaseAmountId
        {
            get => _baseAmountId;
            set
            {
                _baseAmountId = value;
                OnPropertyChanged("BaseAmountId");
            }
        }

        private double _baseAmount;
        public double BaseAmount
        {
            get => _baseAmount;
            set
            {
                _baseAmount= value;
                OnPropertyChanged("BaseAmount");
            }
        }
        private double _ackValue;
        public double AckValue
        {
            get => _ackValue;
            set
            {
                _ackValue = value;
                OnPropertyChanged("AckValue");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                if (_date != null)
                    return _date;
                else return DateTime.Now;
            }

            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }


        #endregion
    }
}
