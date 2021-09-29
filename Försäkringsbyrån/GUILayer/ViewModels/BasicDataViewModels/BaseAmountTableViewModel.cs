using GUILayer.Commands.BasicDataCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class BaseAmountTableViewModel: BaseViewModel
    {
        public static readonly BaseAmountTableViewModel Instance = new BaseAmountTableViewModel();

        public AddBaseAmountTableValueBtn _addBtn { get; }
        public RemoveBaseAmountTableValueBtn _removeBtn { get; }

        private BaseAmountTableViewModel()
        {
            _removeBtn = new RemoveBaseAmountTableValueBtn();
            _addBtn = new AddBaseAmountTableValueBtn();
        }


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
