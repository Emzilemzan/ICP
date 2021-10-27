﻿using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Date = new List<string>() { "25e" + SelectedMonth };
            SalesMens = UpdateSM();
        }

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
            xlWorksheet.get_Range("D21").Font.Bold = true;

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
            xlWorksheet.Columns[2].ColumnWidth = 15;
            xlWorksheet.Columns[3].ColumnWidth = 5;
            xlWorksheet.Columns[4].ColumnWidth = 20;
            xlWorksheet.Columns[5].ColumnWidth = 15;

            xlWorksheet.Cells[1, 1] = SelectedSalesMen.Firstname + " " + SelectedSalesMen.Lastname;
            xlWorksheet.Cells[1, 2] = "Provisionsbesked";
            xlWorksheet.Cells[2, 1] = SelectedSalesMen.StreetAddress;
            xlWorksheet.Cells[3, 1] = SelectedSalesMen.Postalcode + " " + SelectedSalesMen.City;

            xlWorksheet.Cells[5, 1] = "Period";
            xlWorksheet.Cells[5, 2] = SelectedMonth + " " + Year;

            xlWorksheet.Cells[7, 1] = "Bu summa ackvärde: ";
            xlWorksheet.Cells[7, 2] = Instance.CSumAck;
            xlWorksheet.Cells[8, 1] = "Vu summa ackvärde: ";
            xlWorksheet.Cells[8, 2] = Instance.ASumAck;
            xlWorksheet.Cells[9, 1] = "Summa ackvärde: ";
            xlWorksheet.Cells[9, 2] = Instance.SumAck;
            xlWorksheet.Cells[9, 4] = "Prov so: ";
            xlWorksheet.Cells[9, 5] = Instance.ProvSO;

            xlWorksheet.Cells[11, 1] = "Liv summa ackvärde: ";
            xlWorksheet.Cells[11, 2] = Instance.LSumAck;
            xlWorksheet.Cells[11, 4] = "Prov liv: ";
            xlWorksheet.Cells[11, 5] = Instance.ProvLiv;

            xlWorksheet.Cells[13, 1] = "Övrigt provision: ";
            xlWorksheet.Cells[13, 2] = Instance.OtherCommission;
            xlWorksheet.Cells[13, 4] = "Prov övrigt: ";
            xlWorksheet.Cells[13, 5] = Instance.ProvOther;

            xlWorksheet.Cells[15, 1] = "Semesterersättning: ";
            xlWorksheet.Cells[15, 2] = Instance.SelectedVPay.AdditionalPercentage;
            xlWorksheet.Cells[15, 4] = "Semesterersättning: ";
            xlWorksheet.Cells[15, 5] = Instance.VPay;

            xlWorksheet.Cells[17, 4] = "Summa prov: ";
            xlWorksheet.Cells[17, 5] = Instance.SumCommission;

            xlWorksheet.Cells[19, 1] = "Preliminär skatt: ";
            xlWorksheet.Cells[19, 2] = Instance.SelectedSalesMen.TaxRate;
            xlWorksheet.Cells[19, 4] = "Avgår skatt: ";
            xlWorksheet.Cells[19, 5] = Instance.Taxes;

            xlWorksheet.Cells[21, 1] = "Bankkonto insättning: ";
            xlWorksheet.Cells[21, 2] = Instance.PayDate;
            xlWorksheet.Cells[21, 4] = "Att utbetala: ";
            xlWorksheet.Cells[21, 5] = Instance.ToPay;
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
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        if (i.SAI != null)
                        {
                            if (i.SAI.SAID == 1)
                                sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                        }
                        else
                        {
                            sum = 0;
                        }
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
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        if (i.SAI != null)
                        {
                            if (i.SAI.SAID == 2)
                                sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                        }
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            return sum;
        }


        public double CountLSumAck()
        {
            double sum = 0;
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        if (i.LIFE != null)
                            if(i.LIFE.LifeID == 1)
                            { sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4; }
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            return sum;
        }
        public int CountOtherSumAck()
        {
            int? sum = 0;
            int sum1;
            if (SelectedSalesMen.Insurances != null && SelectedMonth != null)
            {
                foreach (Insurance i in SelectedSalesMen.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(_month) + 1) % 12)
                    {
                        if (i.PossibleComisson != null)
                            sum += i.PossibleComisson;
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            sum1 = sum.Value;
            return sum1;
        }
        private double CountProvOther()
        {
            double sum1;
            double sum;
            if (OtherCommission != null)
            {
                double? newsum = _otherCommission * (1 - (SelectedVPay.AdditionalPercentage / 100));
                sum1 = newsum.Value;
                sum = Math.Round(sum1);
            }
            else
            {
                sum = 0;
            }
            return sum;
        }

        private double CountVPay()
        {
            double sum;
            double sum1;
            if (SelectedVPay != null)
            {
                sum1 = (_provLiv + _provSO + _provOther) * (SelectedVPay.AdditionalPercentage / 100);
                sum = Math.Round(sum1);
            }
            else
            {
                sum = 0;
            }
            return sum;
        }

        private double CountSumCom()
        {
            return _provLiv + _provSO + _provOther + _vPay;
        }

        private double LeavingTax()
        {
            double? sum1 = _sumCommission * (SelectedSalesMen.TaxRate / 100);
            double sum = sum1.Value;
            return Math.Round(sum);
        }

        private double CountToPay()
        {
            return _sumCommission - _taxes;
        }

        private double CountProvLiv()
        {
            double sum = _lSumAck * (1 - (SelectedVPay.AdditionalPercentage / 100));
            return Math.Round(sum);
        }

        private double GetPermissionChild(double child)
        {
            double permission = 0;
            List<ComissionShare> cs = new List<ComissionShare>();
            foreach(ComissionShare c in Context.BDController.GetAllCommissionShares())
            {
                if(c.CalenderYear == Year)
                {
                    cs?.Add(c);
                }
            }
            foreach(ComissionShare c in cs)
            {
                if(c.TotalMinAckValue <= child && c.TotalMaxAckValue >= child)
                {
                    permission = c.CommissionShareChildren;
                }
            }
            return permission;
        }

        private double GetPermissionAdult(double adult)
        {
            double permission = 0;
            List<ComissionShare> cs = new List<ComissionShare>();
            foreach (ComissionShare c in Context.BDController.GetAllCommissionShares())
            {
                if (c.CalenderYear == Year)
                {
                    cs?.Add(c);
                }
            }
            foreach (ComissionShare c in cs)
            {
                if (c.TotalMinAckValue <= adult && c.TotalMaxAckValue >= adult)
                {
                    permission = c.ComissionShareAdults;
                }
            }
            return permission;
        }

        private double CountProvSo()
        {
            double pChild = GetPermissionChild(_cSumAck);
            double pAdult = GetPermissionAdult(_aSumAck);
            double sumChild = _cSumAck * pChild;
            double sumAdult = _aSumAck * pAdult;
            double sum = (sumChild + sumAdult) * (1 - (SelectedVPay.AdditionalPercentage / 100));
            return Math.Round(sum);
        }

        private void Count()
        {
            _cSumAck = CountCSumAck();
            OnPropertyChanged("CSumAck");
            _aSumAck = CountASumAck();
            OnPropertyChanged("ASumAck");
            _sumAck = _cSumAck + _aSumAck;
            OnPropertyChanged("SumAck");
            _lSumAck = CountLSumAck();
            OnPropertyChanged("LSumAck");
            _otherCommission = CountOtherSumAck();
            OnPropertyChanged("OtherCommission");
            _provSO = CountProvSo();
            OnPropertyChanged("ProvSO");
            _provLiv = CountProvLiv();
            OnPropertyChanged("ProvLiv");
            _provOther = CountProvOther();
            OnPropertyChanged("ProvOther");
            _vPay = CountVPay();
            OnPropertyChanged("VPay");
            _sumCommission = CountSumCom();
            OnPropertyChanged("SumCommission");
            _taxes = LeavingTax();
            OnPropertyChanged("Taxes");
            _toPay = CountToPay();
            OnPropertyChanged("ToPay");
        }

        private void EmptyAllChoices()
        {
            SelectedMonth = null;
            Taxes = string.Empty;
            ToPay = string.Empty;
            SumCommission = string.Empty;
            ProvLiv = string.Empty;
            ProvSO = string.Empty;
            ProvOther = string.Empty;
            VPay = string.Empty;
            OtherCommission = string.Empty;
            LSumAck = string.Empty;
            CSumAck = string.Empty;
            ASumAck = string.Empty;
            SumAck = string.Empty;
            PayDate = string.Empty;
        }

        public void EmptyAllChoices1()
        {
            SelectedSalesMen = null;
            SelectedVPay = null;
            SelectedMonth = null;
            Taxes = string.Empty;
            ToPay = string.Empty;
            SumCommission = string.Empty;
            ProvLiv = string.Empty;
            ProvSO = string.Empty;
            ProvOther = string.Empty;
            VPay = string.Empty;
            OtherCommission = string.Empty;
            LSumAck = string.Empty;
            CSumAck = string.Empty;
            ASumAck = string.Empty;
            SumAck = string.Empty;
            PayDate = string.Empty;
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
                EmptyAllChoices();
                if(SelectedSalesMen != null)
                {
                    Months = new List<string>() { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };
                    OnPropertyChanged("Months");
                }
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
        public string CSumAck
        {
            get => _cSumAck > 0 ? _cSumAck.ToString() : ""; 
            set
            {
                _cSumAck = 0;
                if (double.TryParse(value, out _cSumAck))
                {
                }
                OnPropertyChanged("CSumAck");
            }
        }

        //Sum for adult ack.
        private double _aSumAck;
        public string ASumAck
        {
            get => _aSumAck > 0 ? _aSumAck.ToString() : "";
            set
            {
                _aSumAck = 0;
                if (double.TryParse(value, out _aSumAck))
                {
                }
                OnPropertyChanged("ASumAck");
            }
        }

        //Sum ack.
        private double _sumAck;
        public string SumAck
        {
            get => _sumAck > 0 ? _sumAck.ToString() : "";
            set
            {
                _sumAck = 0;
                if (double.TryParse(value, out _sumAck))
                {
                }
                OnPropertyChanged("SumAck");
            }
        }

        //Sum for life ack.
        private double _lSumAck;
        public string LSumAck
        {
            get => _lSumAck > 0 ? _lSumAck.ToString() : "";
            set
            {
                _lSumAck = 0;
                if (double.TryParse(value, out _lSumAck))
                {
                }
                OnPropertyChanged("LSumAck");
            }
        }

        //other commission
        private int _otherCommission;
        public string OtherCommission
        {
            get => _otherCommission > 0 ? _otherCommission.ToString() : "";
            set
            {
                _otherCommission = 0;
                if (int.TryParse(value, out _otherCommission))
                {
                }
                OnPropertyChanged("OtherCommission");
            }
        }

        //Vacation pay in %
        private VacationPay _vPayPercent;
        public VacationPay SelectedVPay
        {
            get => _vPayPercent = GetVPay();
            set
            {
                _vPayPercent = value;
                OnPropertyChanged("SelectedVPay");
            }
        }

        private double _vPay;
        public string VPay
        {
            get => _vPay > 0 ? _vPay.ToString() : "";
            set
            {
                _vPay = 0;
                if (double.TryParse(value, out _vPay))
                {
                }
                OnPropertyChanged("VPay");
            }
        }

        private double _sumCommission;
        public string SumCommission
        {
            get => _sumCommission > 0 ? _sumCommission.ToString() : "";
            set
            {
                _sumCommission  = 0;
                if (double.TryParse(value, out _sumCommission))
                {
                }
                OnPropertyChanged("SumCommission");
            }
        }

        private double _taxes;
        public string Taxes
        {
            get => _taxes > 0 ? _taxes.ToString() : "";
            set
            {
                _taxes = 0;
                if (double.TryParse(value, out _taxes))
                {
                }
                OnPropertyChanged("Taxes");
            }
        }

        private double _toPay;
        public string ToPay
        {
            get => _toPay > 0 ? _toPay.ToString() : "";
            set
            {
                _toPay = 0;
                if (double.TryParse(value, out _toPay))
                {
                }
                OnPropertyChanged("ToPay");
            }
        }

        private double _provSO;
        public string ProvSO
        {
            get => _provSO > 0 ? _provSO.ToString() : "";
            set
            {
                _provSO = 0;
                if (double.TryParse(value, out _provSO))
                {

                }
                OnPropertyChanged("ProvSO");
            }
        }

        private double _provLiv;
        public string ProvLiv
        {
            get => _provLiv > 0 ? _provLiv.ToString() : "";
            set
            {
                _provLiv = 0;
                if (double.TryParse(value, out _provLiv))
                {

                }
                OnPropertyChanged("ProvLiv");
            }
        }

        private double _provOther;
        public string ProvOther
        {
            get => _provOther > 0 ? _provOther.ToString() : "";
            set
            {
                _provOther = 0;
                if (double.TryParse(value, out _provOther))
                {
                }
                OnPropertyChanged("ProvOther");
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
