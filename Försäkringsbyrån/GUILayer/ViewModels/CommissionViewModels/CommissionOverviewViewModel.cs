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
            Months = new List<string>() { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };
            Date = new List<string>() { "25e" + SelectedMonth};
            SalesMens = UpdateSM();
            VacationPays = UpdateVpay();
        }

        public List<Insurance> BaseAmounts;


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

        public ObservableCollection<VacationPay> UpdateVpay()
        {
            ObservableCollection<VacationPay> x = new ObservableCollection<VacationPay>();
            foreach (var e in Context.BDController.GetAllVPays())
            {
                x?.Add(e);
            }
            VacationPays = x;
            return VacationPays;
        }



        #endregion

        #region Count
        private void Count()
        {
            if (SelectedSalesMen == null)
                return;
            CSumAck = CountCSumAck();
            ASumAck = CountASumAck();
            SumAck = SumSAAck(CSumAck, ASumAck);
        }

        private double CountCSumAck()  
        {
            double sum = 0;
            if (SelectedSalesMen.Insurances != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)  
                {
                    if (i.InsuranceStatus == 0 && i.SAI.SAInsuranceType.Contains("barn") && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }

        private double CountASumAck()
        {
            double sum = 0;
            if (SelectedSalesMen.Insurances != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {

                   if (i.InsuranceStatus == 0 && i.SAI.SAInsuranceType.Contains("vuxen") && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                   {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                   }

                }
            }
            return sum;
        }

        public double SumSAAck(double CSumAck, double ASumAck)
        {
            double sum;
            sum = CSumAck + ASumAck;
            return sum;
        }

        //private double CountLifeSumAck()
        //{
        //    //double sum = 0;
        //    //if (SelectedSalesMen.Insurances != null)
        //    //{
        //    //    foreach (Insurance i in SelectedSalesMen.Insurances)
        //    //    {

        //    //        if (i.InsuranceStatus == 0 i. && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
        //    //        {
        //    //            sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
        //    //        }

        //    //    }
        //    //}
        //    //return sum;
        //}
        #endregion

        #region Properties for Salesman
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        public List<string> Months { get; set; }
        public List<string> Date { get; set; }
        public ObservableCollection<VacationPay> VacationPays { get; set; }

        private SalesMen _salesMen;
        public SalesMen SelectedSalesMen
        {
            get => _salesMen;
            set
            {
                _salesMen = value;
                Count();
                OnPropertyChanged("SelectedSalesMen");
            }
        }

        private string _month;
        public string SelectedMonth
        {
            get => _month;            
            set
            {
                _month = value;
                if(_month == Months[Months.Count - 1])
                {
                    PayDate = new DateTime(Year + 1, (Months.IndexOf(_month) + 2) % 12, 25).ToString("yyyy-MM-dd");
                }
                else
                {
                    
                    PayDate = new DateTime(Year, (Months.IndexOf(_month) + 2), 25).ToString("yyyy-MM-dd");
                }
                Count();
                OnPropertyChanged("SelectedMonths");
            }
        }

        public int Postalcode
        {
            get => SelectedSalesMen.Postalcode;
            set
            {
                SelectedSalesMen.Postalcode = value;
                OnPropertyChanged("Postalcode");
            }
        }

        public string StreetAdress
        {
            get => SelectedSalesMen.StreetAddress;
            set
            {
                SelectedSalesMen.StreetAddress = value;
                OnPropertyChanged("StreetAdress");
            }
        }

        private string _payDate;
        public string PayDate
        {
            get => _payDate;
            set
            {
                _payDate = value;
                OnPropertyChanged("PayDate");
            }
        }

        private int _year;
        public int Year
        {
            get => _year != 0 ? _year : DateTime.Now.Year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
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
        private VacationPay _vPayPercent;
        public VacationPay SelectedVPay
        {
            get => _vPayPercent;
            set
            {
                _vPayPercent = value;
                OnPropertyChanged("SelectedVPay");
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
