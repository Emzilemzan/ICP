using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.CommissionViewModels
{
    public class CommissionOverviewViewModel : BaseViewModel
    {
        public static readonly CommissionOverviewViewModel Instance = new CommissionOverviewViewModel();

        private CommissionOverviewViewModel()
        {
            Months = new List<string>() { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "November", "December" };
            SalesMens = UpdateSM();
        }


        #region Commands 
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach (var e in Context.SMController.GetAllSalesMen())
            {
                x?.Add(e);
            }
            SalesMens = x;
            return SalesMens;
        }

        
        #endregion

        #region Properties for Salesman
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public List<string> Months { get; set; }

        private SalesMen _salesMen;
        public SalesMen SelectedSalesMen
        {
            get => _salesMen;
            set
            {
                _salesMen = value;
                OnPropertyChanged("SelectedSalesMen");
            }
        }

        //private SalesMen _agentNo;

        //public SalesMen AgentNo
        //{
        //    get => _agentNo;
        //    set
        //    {
        //        _agentNo = value;
        //        OnPropertyChanged("AgentNo");
        //    }
        //}

        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }

       

        //private int _postalCode;
        //public int PostalCode
        //{
        //    get => _postalCode;
        //    set
        //    {
        //        _postalCode = value;
        //        OnPropertyChanged("PostalCode");
        //    }
        //}

        private string _adress;
        public string StreetAdress
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged("StreetAdress");
            }
        }

        private DateTime _bankDate;
        public DateTime BankDate
        {
            get => _bankDate;
            set
            {
                _bankDate = value;
                OnPropertyChanged("BankDate");
            }
        }


        #endregion

        #region Properties for Commission
        //Sum for child ack.
        private double _cSumAck;
        public double CSumAck
        {
            get => _cSumAck;
            set
            {
                _cSumAck = value;
                OnPropertyChanged("CSumAck");
            }
        }

        //Sum for adult ack.
        private double _aSumAck;
        public double ASumAck
        {
            get => _aSumAck;
            set
            {
                _aSumAck = value;
                OnPropertyChanged("ASumAck");
            }
        }

        //Sum ack.
        private double _sumAck;
        public double SumAck
        {
            get => _sumAck;
            set
            {
                _sumAck = value;
                OnPropertyChanged("SumAck");
            }
        }

        //Sum for life ack.
        private double _lSumAck;
        public double LSumAck
        {
            get => _lSumAck;
            set
            {
                _lSumAck = value;
                OnPropertyChanged("LSumAck");
            }
        }

        //other commission
        private double _otherCommission;
        public double OtherCommission
        {
            get => _otherCommission;
            set
            {
                _otherCommission = value;
                OnPropertyChanged("OtherCommission");
            }
        }

        //Vacation pay in %
        private double _vPayPercent;
        public double VPayPercent
        {
            get => _vPayPercent;
            set
            {
                _vPayPercent = value;
                OnPropertyChanged("VPayPercent");
            }
        }

        private double _vPay;
        public double VPay
        {
            get => _vPay;
            set
            {
                _vPay = value;
                OnPropertyChanged("VPay");
            }
        }

        //Taxes in %
        private double _taxesPercent;
        public double TaxesPercent
        {
            get => _taxesPercent;
            set
            {
                _taxesPercent = value;
                OnPropertyChanged("TaxesPercent");
            }
        }

        private double _sumCommission;
        public double SumCommission
        {
            get => _sumCommission;
            set
            {
                _sumCommission = value;
                OnPropertyChanged("SumCommission");
            }
        }

        private double _taxes;
        public double Taxes
        {
            get => _taxes;
            set
            {
                _taxes = value;
                OnPropertyChanged("Taxes");
            }
        }

        private double _toPay;
        public double ToPay
        {
            get => _toPay;
            set
            {
                _toPay = value;
                OnPropertyChanged("ToPay");
            }
        }

        private double _provSO;
        public double ProvSO
        {
            get => _provSO;
            set
            {
                _provSO = value;
                OnPropertyChanged("ProvSO");
            }
        }

        private double _provLiv;
        public double ProvLiv
        {
            get => _provLiv;
            set
            {
                _provLiv = value;
                OnPropertyChanged("ProvLiv");
            }
        }

        private double _provOther;
        public double ProvOther
        {
            get => _provOther;
            set
            {
                _provOther = value;
                OnPropertyChanged("ProvOther");
            }
        }
        #endregion

    }
}
