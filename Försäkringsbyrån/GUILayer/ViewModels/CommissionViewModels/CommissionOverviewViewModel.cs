using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GUILayer.ViewModels.CommissionViewModels
{
    public class CommissionOverviewViewModel : BaseViewModel
    {
        public static readonly CommissionOverviewViewModel Instance = new CommissionOverviewViewModel();

        private CommissionOverviewViewModel()
        {
            Months = new List<string>() { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };
            Date = new List<string>() { "25e" + SelectedMonth };
            SalesMens = UpdateSM();
        }

        public List<Insurance> BaseAmounts;

        #region update
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
        private VacationPay GetVPay()
        {
            VacationPays = UpdateVpay();
            VacationPay p = new VacationPay();
            foreach (var e in VacationPays)
            {
                if (DateTime.Today.Year.Equals(e.Year))
                    p = e;
            }
            return p;

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
        #region excel
        private ICommand _export;
        public ICommand ExportBtn => _export ?? (_export = new RelayCommand(x => { Export(); }));

        private void Export()
        {
            if (SelectedMonth != null && SelectedSalesMen != null)
            {
                ExportCommission(SelectedMonth, SelectedSalesMen);
            }
            else MessageBox.Show("Du måste välja en månad och en säljare");
        }

        /// <summary>
        /// method to create pdf document and open it when button is clicked. 
        /// </summary>
        private void ExportCommission(string month, SalesMen salesMen)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkBook;
            Worksheet xlWorksheet;
            xlApp.DisplayAlerts = false;
            object value = Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(value);
            xlWorksheet = xlWorkBook.ActiveSheet as Worksheet;
            xlWorksheet.Name = "Provisionsbesked";

            WriteDataToExcel(xlWorksheet);
            xlWorksheet.get_Range("A1", "D1").Font.Bold = true;
            xlWorksheet.get_Range("A1", "D1").VerticalAlignment = XlVAlign.xlVAlignCenter;
            xlWorksheet.get_Range("A5", "D5").Font.Bold = true;

            if (System.IO.File.Exists("Provisionsbesked"))
            {
                xlWorkBook.SaveAs("Provisionsbesked", XlFileFormat.xlWorkbookNormal,
                value, value, value, value, XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            else
            {
                xlWorkBook.SaveAs("Provisionsbesked", XlFileFormat.xlWorkbookNormal,
                value, value, value, value, XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            xlApp.Visible = true;
            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorkBook);
            xlApp.DisplayAlerts = true;
            Marshal.ReleaseComObject(xlApp);

        }

        private void WriteDataToExcel(Worksheet xlWorksheet)
        {
            xlWorksheet.Columns[1].ColumnWidth = 20;
            xlWorksheet.Columns[2].ColumnWidth = 20;
            xlWorksheet.Columns[3].ColumnWidth = 20;
            xlWorksheet.Columns[4].ColumnWidth = 20;
            xlWorksheet.Columns[5].ColumnWidth = 20;
            xlWorksheet.Columns[6].ColumnWidth = 20;
            xlWorksheet.Columns[7].ColumnWidth = 20;
            xlWorksheet.Columns[8].ColumnWidth = 20;
            xlWorksheet.Columns[9].ColumnWidth = 20;
            xlWorksheet.Columns[10].ColumnWidth = 20;
            xlWorksheet.Columns[11].ColumnWidth = 20;
            xlWorksheet.Columns[12].ColumnWidth = 20;
            xlWorksheet.Columns[13].ColumnWidth = 20;

            xlWorksheet.Cells[1, 1] = SelectedSalesMen.Firstname + SelectedSalesMen.Lastname;
            xlWorksheet.Cells[1, 3] = "Provisionsbesked";
            xlWorksheet.Cells[2, 1] = SelectedSalesMen.StreetAddress;
            xlWorksheet.Cells[3, 1] = SelectedSalesMen.Postalcode + SelectedSalesMen.City;

            xlWorksheet.Cells[5, 1] = "Period";
            xlWorksheet.Cells[5, 2] = SelectedMonth + " " + Year;

            xlWorksheet.Cells[7, 1] = "Bu summa ackvärde: ";
            xlWorksheet.Cells[7, 2] = Instance.CSumAck;
            xlWorksheet.Cells[8, 1] = "Vu summa ackvärde: ";
            xlWorksheet.Cells[8, 2] = Instance.ASumAck;
            xlWorksheet.Cells[9, 1] = "Summa ackvärde: ";
            xlWorksheet.Cells[9, 2] = Instance.SumAck;
            xlWorksheet.Cells[9, 3] = "Prov so: ";
            xlWorksheet.Cells[9, 4] = Instance.ProvSO;

            xlWorksheet.Cells[11, 1] = "Liv summa ackvärde: ";
            xlWorksheet.Cells[11, 2] = Instance.LSumAck;
            xlWorksheet.Cells[11, 3] = "Prov liv: ";
            xlWorksheet.Cells[11, 4] = Instance.ProvLiv;

            xlWorksheet.Cells[13, 1] = "Övrigt provision: ";
            xlWorksheet.Cells[13, 2] = Instance.OtherCommission;
            xlWorksheet.Cells[13, 3] = "Prov övrigt: ";
            xlWorksheet.Cells[13, 4] = Instance.ProvOther;

            xlWorksheet.Cells[15, 1] = "Semesterersättning: ";
            xlWorksheet.Cells[15, 2] = Instance.SelectedVPay;
            xlWorksheet.Cells[15, 3] = "Semesterersättning: ";
            xlWorksheet.Cells[15, 4] = Instance.VPay;

            xlWorksheet.Cells[17, 3] = "Summa prov: ";
            xlWorksheet.Cells[17, 4] = Instance.SumCommission;

            xlWorksheet.Cells[19, 1] = "Preliminär skatt: ";
            xlWorksheet.Cells[19, 2] = Instance.TaxesPercent;
            xlWorksheet.Cells[19, 3] = "Avgår skatt: ";
            xlWorksheet.Cells[19, 4] = Instance.Taxes;

            xlWorksheet.Cells[21, 1] = "Bankkonto insättning: ";
            xlWorksheet.Cells[21, 2] = Instance.PayDate;
            xlWorksheet.Cells[21, 3] = "Att utbetala: ";
            xlWorksheet.Cells[21, 4] = Instance.ToPay;
        }
        #endregion

        #region Count
        private double CountCSumAck()
        {
            double sum = 0;
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.SAI.SAID == 1 && i.PayYear != null && i.PayMonth != null && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
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
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
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

        public double CountLSumAck()
        {
            double sum = 0;
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.LIFE != null && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        public int? CountOtherSumAck()
        {
            int? sum = 0;
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12 && i.PossibleComisson != null)
                    {
                        sum += i.PossibleComisson;
                    }
                }
            }
            return sum;
        }
        private double? CountProvOther()
        {
            return OtherCommission * (1 - (SelectedVPay.AdditionalPercentage / 100));
        }
        private void Count()
        {
            CSumAck = CountCSumAck();
            OnPropertyChanged("CSumAck");
            ASumAck = CountASumAck();
            OnPropertyChanged("ASumAck");
            SumAck = SumSAAck(CSumAck, ASumAck);
            OnPropertyChanged("SumAck");
            LSumAck = CountLSumAck();
            OnPropertyChanged("LSumAck");
            OtherCommission = CountOtherSumAck();
            OnPropertyChanged("OtherCommission");
            ProvOther = CountProvOther();
            OnPropertyChanged("ProvOther");
        }
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
                OnPropertyChanged("SelectedMonth");
                if (SelectedMonth != null)
                {
                    if (_month == Months[Months.Count - 1])
                    {
                        PayDate = new DateTime(Year + 1, (Months.IndexOf(_month) + 2) % 12, 25).ToString("yyyy-MM-dd");
                    }
                    else
                    {

                        PayDate = new DateTime(Year, (Months.IndexOf(_month) + 2), 25).ToString("yyyy-MM-dd");
                    }
                    OnPropertyChanged("PayDate");
                    Count();
                }
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
        private int? _otherCommission;
        public int? OtherCommission
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
            get => _vPayPercent = GetVPay();
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

        private double? _provOther;
        public double? ProvOther
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
