using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Media;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
    public class GetandexportSalesstatisticsViewModel : BaseViewModel
    {
        public static readonly GetandexportSalesstatisticsViewModel Instance = new GetandexportSalesstatisticsViewModel();
        private GetandexportSalesstatisticsViewModel()
        {
            Update();
        }

        #region methods for view. 
        /// <summary>
        /// only shows years that actually has signed insurances. 
        /// </summary>
        /// <returns></returns>
        public List<int?> GetYears()
        {
            List<int?> Years1 = new List<int?>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.PayYear != null)
                {
                    Years1?.Add(i.PayYear);
                }
            }

            return Years = Years1.Union(Years1).ToList();

        }

        //Get all salesmen. 
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            ObservableCollection<SalesMen> y = new ObservableCollection<SalesMen>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.InsuranceStatus == Status.Tecknad)
                {
                    x?.Add(i.AgentNo);
                }
            }
            var sm = x.Union(x).ToList();
            foreach (var s in sm)
            {
                y?.Add(s);
            }
            return SalesMens = y;
        }

        public void Update()
        {
            SalesMens = UpdateSM();
            Years = GetYears();
        }

        #endregion

        #region command export
        private ICommand static_btn;

        public ICommand GetandexportSalesstatisticsViewModel_Btn => static_btn ?? (static_btn = new RelayCommand(x => { SendStatisticsToExcel(); }));

        private void SendStatisticsToExcel()
        {
            if (Instance.Year != null && Instance.SalesMen != null)
            {
                ExcelFireUp(int.Parse(Year?.ToString()), Instance.SalesMen);
            }
            else MessageBox.Show("Du måste välja ett år och en säljare");
        }
        /// <summary>
        /// method for start excel
        /// </summary>
        /// <param name="year"></param>
        /// <param name="salesMen"></param>
        public void ExcelFireUp(int year, SalesMen salesMen)
        {
            Excel.Application xlApp = new Excel.Application();
            Workbook xlWorkBook;
            Worksheet xlWorksheet;
            xlApp.DisplayAlerts = false;
            object value = Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(value);
            xlWorksheet = xlWorkBook.ActiveSheet as Worksheet;
            xlWorksheet.Name = "Försäljningsstatistik";

            ExcelPrint(xlWorksheet, year);
            xlWorksheet.get_Range("A1", "AN1").Font.Bold = true;
            xlWorksheet.get_Range("A4").Font.Bold = true;
            xlWorksheet.get_Range("A8").Font.Bold = true;
            xlWorksheet.get_Range("A12").Font.Bold = true;
            xlWorksheet.get_Range("A16").Font.Bold = true;
            xlWorksheet.get_Range("A20").Font.Bold = true;
            xlWorksheet.get_Range("A24").Font.Bold = true;

            if (System.IO.File.Exists("Excel_Försäljningsstatistik"))
            {
                xlWorkBook.SaveAs("Excel_Försäljningsstatistik", Excel.XlFileFormat.xlWorkbookNormal,
                value, value, value, value, Excel.XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            else
            {
                xlWorkBook.SaveAs("Excel_Försäljningsstatistik", Excel.XlFileFormat.xlWorkbookNormal,
                value, value, value, value, Excel.XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            xlApp.Visible = true;
            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorkBook);
            xlApp.DisplayAlerts = true;
            Marshal.ReleaseComObject(xlApp);
        }


        private void ExcelPrint(Worksheet xlWorksheet, int year)
        {
            int row = 1;
            #region columnwidth
            xlWorksheet.Columns[1].ColumnWidth = 30;
            xlWorksheet.Columns[1].ColumnWidth = 13;
            xlWorksheet.Columns[2].ColumnWidth = 13;
            xlWorksheet.Columns[3].ColumnWidth = 13;
            xlWorksheet.Columns[4].ColumnWidth = 13;
            xlWorksheet.Columns[5].ColumnWidth = 13;
            xlWorksheet.Columns[6].ColumnWidth = 13;
            xlWorksheet.Columns[7].ColumnWidth = 13;
            xlWorksheet.Columns[8].ColumnWidth = 13;
            xlWorksheet.Columns[9].ColumnWidth = 13;
            xlWorksheet.Columns[10].ColumnWidth = 13;
            xlWorksheet.Columns[11].ColumnWidth = 13;
            xlWorksheet.Columns[12].ColumnWidth = 13;
            xlWorksheet.Columns[13].ColumnWidth = 13;
            xlWorksheet.Columns[14].ColumnWidth = 13;
            #endregion

            #region Named ranges for xlWorksheet
            xlWorksheet.Cells[row, 1] = "Säljare";
            xlWorksheet.Cells[2, 1] = Instance.SalesMen.Firstname + Instance.SalesMen.Lastname;
            xlWorksheet.Cells[row, 2] = "Januari";
            xlWorksheet.Cells[row, 3] = "Februari";
            xlWorksheet.Cells[row, 4] = "Mars";
            xlWorksheet.Cells[row, 5] = "April";
            xlWorksheet.Cells[row, 6] = "Maj";
            xlWorksheet.Cells[row, 7] = "Juni";
            xlWorksheet.Cells[row, 8] = "Juli";
            xlWorksheet.Cells[row, 9] = "Augusti";
            xlWorksheet.Cells[row, 10] = "September";
            xlWorksheet.Cells[row, 11] = "Oktober";
            xlWorksheet.Cells[row, 12] = "November";
            xlWorksheet.Cells[row, 13] = "December";
            xlWorksheet.Cells[row, 14] = "Året";

            xlWorksheet.Cells[4, 1] = "SOB";
            xlWorksheet.Cells[5, 1] = "Antal tecknade";
            xlWorksheet.Cells[6, 1] = "Totalt ack.värde";

            xlWorksheet.Cells[8, 1] = "SOV";
            xlWorksheet.Cells[9, 1] = "Antal tecknade";
            xlWorksheet.Cells[10, 1] = "Totalt ack.värde";

            xlWorksheet.Cells[12, 1] = "LIV";
            xlWorksheet.Cells[13, 1] = "Antal tecknade";
            xlWorksheet.Cells[14, 1] = "Totalt ack.värde";

            xlWorksheet.Cells[16, 1] = "ÖFP";
            xlWorksheet.Cells[17, 1] = "Antal tecknade";
            xlWorksheet.Cells[18, 1] = "Totalt ack.värde";

            xlWorksheet.Cells[20, 1] = "FF";
            xlWorksheet.Cells[21, 1] = "Antal tecknade";
            xlWorksheet.Cells[22, 1] = "Totalt ack.värde";

            xlWorksheet.Cells[24, 1] = "Totalt";
            xlWorksheet.Cells[25, 1] = "Antal tecknade";
            xlWorksheet.Cells[26, 1] = "Totalt ack.värde";
            #endregion

            WriteDataToExcel(xlWorksheet, year);

        }
        /// <summary>
        /// Worksheet for all salesmens trendstatistics
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="year"></param>
        private void WriteDataToExcel(Worksheet xlWorksheet, int year)
        {
            SalesMen s = Instance.SalesMen;

            #region number total
            xlWorksheet.Cells[5, 2] = UpdateCSAC(1);
            xlWorksheet.Cells[5, 3] = UpdateCSAC(2);
            xlWorksheet.Cells[5, 4] = UpdateCSAC(3);
            xlWorksheet.Cells[5, 5] = UpdateCSAC(4);
            xlWorksheet.Cells[5, 6] = UpdateCSAC(5);
            xlWorksheet.Cells[5, 7] = UpdateCSAC(6);
            xlWorksheet.Cells[5, 8] = UpdateCSAC(7);
            xlWorksheet.Cells[5, 9] = UpdateCSAC(8);
            xlWorksheet.Cells[5, 10] = UpdateCSAC(9);
            xlWorksheet.Cells[5, 11] = UpdateCSAC(10);
            xlWorksheet.Cells[5, 12] = UpdateCSAC(11);
            xlWorksheet.Cells[5, 13] = UpdateCSAC(12);
            xlWorksheet.Cells[5, 14] = UpdateCSAC(1) + UpdateCSAC(2) + UpdateCSAC(3) + UpdateCSAC(4) + UpdateCSAC(5) + UpdateCSAC(6) + UpdateCSAC(7) + UpdateCSAC(8) + UpdateCSAC(9) + UpdateCSAC(10) + UpdateCSAC(11) + UpdateCSAC(12);

            xlWorksheet.Cells[9, 2] = UpdateASAC(1);
            xlWorksheet.Cells[9, 3] = UpdateASAC(2);
            xlWorksheet.Cells[9, 4] = UpdateASAC(3);
            xlWorksheet.Cells[9, 5] = UpdateASAC(4);
            xlWorksheet.Cells[9, 6] = UpdateASAC(5);
            xlWorksheet.Cells[9, 7] = UpdateASAC(6);
            xlWorksheet.Cells[9, 8] = UpdateASAC(7);
            xlWorksheet.Cells[9, 9] = UpdateASAC(8);
            xlWorksheet.Cells[9, 10] = UpdateASAC(9);
            xlWorksheet.Cells[9, 11] = UpdateASAC(10);
            xlWorksheet.Cells[9, 12] = UpdateASAC(11);
            xlWorksheet.Cells[9, 13] = UpdateASAC(12);
            xlWorksheet.Cells[9, 14] = UpdateASAC(1) + UpdateASAC(2) + UpdateASAC(3) + UpdateASAC(4) + UpdateASAC(5) + UpdateASAC(6) + UpdateASAC(7) + UpdateASAC(8) + UpdateASAC(9) + UpdateASAC(10) + UpdateASAC(11) + UpdateASAC(12);

            xlWorksheet.Cells[13, 2] = UpdateLife(1);
            xlWorksheet.Cells[13, 3] = UpdateLife(2);
            xlWorksheet.Cells[13, 4] = UpdateLife(3);
            xlWorksheet.Cells[13, 5] = UpdateLife(4);
            xlWorksheet.Cells[13, 6] = UpdateLife(5);
            xlWorksheet.Cells[13, 7] = UpdateLife(6);
            xlWorksheet.Cells[13, 8] = UpdateLife(7);
            xlWorksheet.Cells[13, 9] = UpdateLife(8);
            xlWorksheet.Cells[13, 10] = UpdateLife(9);
            xlWorksheet.Cells[13, 11] = UpdateLife(10);
            xlWorksheet.Cells[13, 12] = UpdateLife(11);
            xlWorksheet.Cells[13, 13] = UpdateLife(12);
            xlWorksheet.Cells[13, 14] = UpdateLife(1) + UpdateLife(2) + UpdateLife(3) + UpdateLife(4) + UpdateLife(5) + UpdateLife(6) + UpdateLife(7) + UpdateLife(8) + UpdateLife(9) + UpdateLife(10) + UpdateLife(11) + UpdateLife(12);

            xlWorksheet.Cells[17, 2] = UpdateOP(1);
            xlWorksheet.Cells[17, 3] = UpdateOP(2);
            xlWorksheet.Cells[17, 4] = UpdateOP(3);
            xlWorksheet.Cells[17, 5] = UpdateOP(4);
            xlWorksheet.Cells[17, 6] = UpdateOP(5);
            xlWorksheet.Cells[17, 7] = UpdateOP(6);
            xlWorksheet.Cells[17, 8] = UpdateOP(7);
            xlWorksheet.Cells[17, 9] = UpdateOP(8);
            xlWorksheet.Cells[17, 10] = UpdateOP(9);
            xlWorksheet.Cells[17, 11] = UpdateOP(10);
            xlWorksheet.Cells[17, 12] = UpdateOP(11);
            xlWorksheet.Cells[17, 13] = UpdateOP(12);
            xlWorksheet.Cells[17, 14] = UpdateOP(1) + UpdateOP(2) + UpdateOP(3) + UpdateOP(4) + UpdateOP(5) + UpdateOP(6) + UpdateOP(7) + UpdateOP(8) + UpdateOP(9) + UpdateOP(10) + UpdateOP(11) + UpdateOP(12);

            xlWorksheet.Cells[21, 2] = UpdateCI(1);
            xlWorksheet.Cells[21, 3] = UpdateCI(2);
            xlWorksheet.Cells[21, 4] = UpdateCI(3);
            xlWorksheet.Cells[21, 5] = UpdateCI(4);
            xlWorksheet.Cells[21, 6] = UpdateCI(5);
            xlWorksheet.Cells[21, 7] = UpdateCI(6);
            xlWorksheet.Cells[21, 8] = UpdateCI(7);
            xlWorksheet.Cells[21, 9] = UpdateCI(8);
            xlWorksheet.Cells[21, 10] = UpdateCI(9);
            xlWorksheet.Cells[21, 11] = UpdateCI(10);
            xlWorksheet.Cells[21, 12] = UpdateCI(11);
            xlWorksheet.Cells[21, 13] = UpdateCI(12);
            xlWorksheet.Cells[21, 14] = UpdateCI(1) + UpdateCI(2) + UpdateCI(3) + UpdateCI(4) + UpdateCI(5) + UpdateCI(6) + UpdateCI(7) + UpdateCI(8) + UpdateCI(9) + UpdateCI(10) + UpdateCI(11) + UpdateCI(12);

            xlWorksheet.Cells[25, 2] = UpdateTotal(1);
            xlWorksheet.Cells[25, 3] = UpdateTotal(2);
            xlWorksheet.Cells[25, 4] = UpdateTotal(3);
            xlWorksheet.Cells[25, 5] = UpdateTotal(4);
            xlWorksheet.Cells[25, 6] = UpdateTotal(5);
            xlWorksheet.Cells[25, 7] = UpdateTotal(6);
            xlWorksheet.Cells[25, 8] = UpdateTotal(7);
            xlWorksheet.Cells[25, 9] = UpdateTotal(8);
            xlWorksheet.Cells[25, 10] = UpdateTotal(9);
            xlWorksheet.Cells[25, 11] = UpdateTotal(10);
            xlWorksheet.Cells[25, 12] = UpdateTotal(11);
            xlWorksheet.Cells[25, 13] = UpdateTotal(12);
            xlWorksheet.Cells[25, 14] = UpdateTotal(1) + UpdateTotal(2) + UpdateTotal(3) + UpdateTotal(4) + UpdateTotal(5) + UpdateTotal(6) + UpdateTotal(7) + UpdateTotal(8) + UpdateTotal(9) + UpdateTotal(10) + UpdateTotal(11) + UpdateTotal(12);
            #endregion
            #region all ackvisitions. 
            xlWorksheet.Cells[6, 2] = CreateSMAVC(s, 1, year);
            xlWorksheet.Cells[6, 3] = CreateSMAVC(s, 2, year);
            xlWorksheet.Cells[6, 4] = CreateSMAVC(s, 3, year);
            xlWorksheet.Cells[6, 5] = CreateSMAVC(s, 4, year);
            xlWorksheet.Cells[6, 6] = CreateSMAVC(s, 5, year);
            xlWorksheet.Cells[6, 7] = CreateSMAVC(s, 6, year);
            xlWorksheet.Cells[6, 8] = CreateSMAVC(s, 7, year);
            xlWorksheet.Cells[6, 9] = CreateSMAVC(s, 8, year);
            xlWorksheet.Cells[6, 10] = CreateSMAVC(s, 9, year);
            xlWorksheet.Cells[6, 11] = CreateSMAVC(s, 10, year);
            xlWorksheet.Cells[6, 12] = CreateSMAVC(s, 11, year);
            xlWorksheet.Cells[6, 13] = CreateSMAVC(s, 12, year);
            xlWorksheet.Cells[6, 14] = CreateSMAVC(s, 1, year) + CreateSMAVC(s, 2, year) + CreateSMAVC(s, 3, year) + CreateSMAVC(s, 4, year) + CreateSMAVC(s, 5, year) + CreateSMAVC(s, 6, year) + CreateSMAVC(s, 7, year) + CreateSMAVC(s, 8, year) + CreateSMAVC(s, 9, year) + CreateSMAVC(s, 10, year) + CreateSMAVC(s, 11, year) + CreateSMAVC(s, 12, year);

            xlWorksheet.Cells[10, 2] = CreateSMAVA(s, 1, year);
            xlWorksheet.Cells[10, 3] = CreateSMAVA(s, 2, year);
            xlWorksheet.Cells[10, 4] = CreateSMAVA(s, 3, year);
            xlWorksheet.Cells[10, 5] = CreateSMAVA(s, 4, year);
            xlWorksheet.Cells[10, 6] = CreateSMAVA(s, 5, year);
            xlWorksheet.Cells[10, 7] = CreateSMAVA(s, 6, year);
            xlWorksheet.Cells[10, 8] = CreateSMAVA(s, 7, year);
            xlWorksheet.Cells[10, 9] = CreateSMAVA(s, 8, year);
            xlWorksheet.Cells[10, 10] = CreateSMAVA(s, 9, year);
            xlWorksheet.Cells[10, 11] = CreateSMAVA(s, 10, year);
            xlWorksheet.Cells[10, 12] = CreateSMAVA(s, 11, year);
            xlWorksheet.Cells[10, 13] = CreateSMAVA(s, 12, year);
            xlWorksheet.Cells[10, 14] = CreateSMAVA(s, 1, year) + CreateSMAVA(s, 2, year) + CreateSMAVA(s, 3, year) + CreateSMAVA(s, 4, year) + CreateSMAVA(s, 5, year) + CreateSMAVA(s, 6, year) + CreateSMAVA(s, 7, year) + CreateSMAVA(s, 8, year) + CreateSMAVA(s, 9, year) + CreateSMAVA(s, 10, year) + CreateSMAVA(s, 11, year) + CreateSMAVA(s, 12, year);

            xlWorksheet.Cells[14, 2] = CreateLife(s, 1, year);
            xlWorksheet.Cells[14, 3] = CreateLife(s, 2, year);
            xlWorksheet.Cells[14, 4] = CreateLife(s, 3, year);
            xlWorksheet.Cells[14, 5] = CreateLife(s, 4, year);
            xlWorksheet.Cells[14, 6] = CreateLife(s, 5, year);
            xlWorksheet.Cells[14, 7] = CreateLife(s, 6, year);
            xlWorksheet.Cells[14, 8] = CreateLife(s, 7, year);
            xlWorksheet.Cells[14, 9] = CreateLife(s, 8, year);
            xlWorksheet.Cells[14, 10] = CreateLife(s, 9, year);
            xlWorksheet.Cells[14, 11] = CreateLife(s, 10, year);
            xlWorksheet.Cells[14, 12] = CreateLife(s, 11, year);
            xlWorksheet.Cells[14, 13] = CreateLife(s, 12, year);
            xlWorksheet.Cells[14, 14] = CreateLife(s, 1, year) + CreateLife(s, 2, year) + CreateLife(s, 3, year) + CreateLife(s, 4, year) + CreateLife(s, 5, year) + CreateLife(s, 6, year) + CreateLife(s, 7, year) + CreateLife(s, 8, year) + CreateLife(s, 9, year) + CreateLife(s, 10, year) + CreateLife(s, 11, year) + CreateLife(s, 12, year);

            xlWorksheet.Cells[18, 2] = CreateOP(s, 1, year);
            xlWorksheet.Cells[18, 3] = CreateOP(s, 2, year);
            xlWorksheet.Cells[18, 4] = CreateOP(s, 3, year);
            xlWorksheet.Cells[18, 5] = CreateOP(s, 4, year);
            xlWorksheet.Cells[18, 6] = CreateOP(s, 5, year);
            xlWorksheet.Cells[18, 7] = CreateOP(s, 6, year);
            xlWorksheet.Cells[18, 8] = CreateOP(s, 7, year);
            xlWorksheet.Cells[18, 9] = CreateOP(s, 8, year);
            xlWorksheet.Cells[18, 10] = CreateOP(s, 9, year);
            xlWorksheet.Cells[18, 11] = CreateOP(s, 10, year);
            xlWorksheet.Cells[18, 12] = CreateOP(s, 11, year);
            xlWorksheet.Cells[18, 13] = CreateOP(s, 12, year);
            xlWorksheet.Cells[18, 14] = CreateOP(s, 1, year) + CreateOP(s, 2, year) + CreateOP(s, 3, year) + CreateOP(s, 4, year) + CreateOP(s, 5, year) + CreateOP(s, 6, year) + CreateOP(s, 7, year) + CreateOP(s, 8, year) + CreateOP(s, 9, year) + CreateOP(s, 10, year) + CreateOP(s, 11, year) + CreateOP(s, 12, year);

            xlWorksheet.Cells[22, 2] = CreateCI(s, 1, year);
            xlWorksheet.Cells[22, 3] = CreateCI(s, 2, year);
            xlWorksheet.Cells[22, 4] = CreateCI(s, 3, year);
            xlWorksheet.Cells[22, 5] = CreateCI(s, 4, year);
            xlWorksheet.Cells[22, 6] = CreateCI(s, 5, year);
            xlWorksheet.Cells[22, 7] = CreateCI(s, 6, year);
            xlWorksheet.Cells[22, 8] = CreateCI(s, 7, year);
            xlWorksheet.Cells[22, 9] = CreateCI(s, 8, year);
            xlWorksheet.Cells[22, 10] = CreateCI(s, 9, year);
            xlWorksheet.Cells[22, 11] = CreateCI(s, 10, year);
            xlWorksheet.Cells[22, 12] = CreateCI(s, 11, year);
            xlWorksheet.Cells[22, 13] = CreateCI(s, 12, year);
            xlWorksheet.Cells[22, 14] = CreateCI(s, 1, year) + CreateCI(s, 2, year) + CreateCI(s, 3, year) + CreateCI(s, 4, year) + CreateCI(s, 5, year) + CreateCI(s, 6, year) + CreateCI(s, 7, year) + CreateCI(s, 8, year) + CreateCI(s, 9, year) + CreateCI(s, 10, year) + CreateCI(s, 11, year) + CreateCI(s, 12, year);

            xlWorksheet.Cells[26, 2] = CountMonthTotalSM(s, 1, year);
            xlWorksheet.Cells[26, 3] = CountMonthTotalSM(s, 2, year);
            xlWorksheet.Cells[26, 4] = CountMonthTotalSM(s, 3, year);
            xlWorksheet.Cells[26, 5] = CountMonthTotalSM(s, 4, year);
            xlWorksheet.Cells[26, 6] = CountMonthTotalSM(s, 5, year);
            xlWorksheet.Cells[26, 7] = CountMonthTotalSM(s, 6, year);
            xlWorksheet.Cells[26, 8] = CountMonthTotalSM(s, 7, year);
            xlWorksheet.Cells[26, 9] = CountMonthTotalSM(s, 8, year);
            xlWorksheet.Cells[26, 10] = CountMonthTotalSM(s, 9, year);
            xlWorksheet.Cells[26, 11] = CountMonthTotalSM(s, 10, year);
            xlWorksheet.Cells[26, 12] = CountMonthTotalSM(s, 11, year);
            xlWorksheet.Cells[26, 13] = CountMonthTotalSM(s, 12, year);
            xlWorksheet.Cells[26, 14] = CountTotal(s, year);

            #endregion
        }


        #endregion

        #region comboboxproperties
        public List<int?> Years { get; set; }
        private int? _year;
        public int? Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
                if (SalesMen != null)
                {
                    UpdateAllFields();
                }
            }
        }
        public ObservableCollection<SalesMen> SalesMens { get; set; }
        private SalesMen _salesMen;
        public SalesMen SalesMen
        {
            get => _salesMen;
            set
            {
                _salesMen = value;
                OnPropertyChanged("SalesMen");
            }
        }

        private void UpdateAllFields()
        {
            int year = int.Parse(Year?.ToString());
            #region number total
            CountSACJanuary = UpdateCSAC(1);
            OnPropertyChanged("CountSACJanuary");
            CountSACFebruary = UpdateCSAC(2);
            OnPropertyChanged("CountSACFebruary");
            CountSACMars = UpdateCSAC(3);
            OnPropertyChanged("CountSACMars");
            CountSACApril = UpdateCSAC(4);
            OnPropertyChanged("CountSACApril");
            CountSACMay = UpdateCSAC(5);
            OnPropertyChanged("CountSACMay");
            CountSACJune = UpdateCSAC(6);
            OnPropertyChanged("CountSACJune");
            CountSACJuly = UpdateCSAC(7);
            OnPropertyChanged("CountSACJuly");
            CountSACAugust = UpdateCSAC(8);
            OnPropertyChanged("CountSACAugust");
            CountSACSeptember = UpdateCSAC(9);
            OnPropertyChanged("CountSACSeptember");
            CountSACOctober = UpdateCSAC(10);
            OnPropertyChanged("CountSACOctober");
            CountSACNovember = UpdateCSAC(11);
            OnPropertyChanged("CountSACNovember");
            CountSACDecember = UpdateCSAC(12);
            OnPropertyChanged("CountSACDecember");
            CountSACTotal = UpdateCSAC(1) + UpdateCSAC(2) + UpdateCSAC(3) + UpdateCSAC(4) + UpdateCSAC(5) + UpdateCSAC(6) + UpdateCSAC(7) + UpdateCSAC(8) + UpdateCSAC(9) + UpdateCSAC(10) + UpdateCSAC(11) + UpdateCSAC(12);
            OnPropertyChanged("CountSACTotal");

            CountSAAJanuary = UpdateASAC(1);
            OnPropertyChanged("CountSAAJanuary");
            CountSAAFebruary = UpdateASAC(2);
            OnPropertyChanged("CountSAAFebruary");
            CountSAAMars = UpdateASAC(3);
            OnPropertyChanged("CountSAAMars");
            CountSAAApril = UpdateASAC(4);
            OnPropertyChanged("CountSAAApril");
            CountSAAMay = UpdateASAC(5);
            OnPropertyChanged("CountSAAMay");
            CountSAAJune = UpdateASAC(6);
            OnPropertyChanged("CountSAAJune");
            CountSAAJuly = UpdateASAC(7);
            OnPropertyChanged("CountSAAFJuly");
            CountSAAAugust = UpdateASAC(8);
            OnPropertyChanged("CountSAAAugust");
            CountSAASeptember = UpdateASAC(9);
            OnPropertyChanged("CountSAASeptember");
            CountSAAOctober = UpdateASAC(10);
            OnPropertyChanged("CountSAAOctober");
            CountSAANovember = UpdateASAC(11);
            OnPropertyChanged("CountSAANovember");
            CountSAADecember = UpdateASAC(12);
            OnPropertyChanged("CountSAADecember");
            CountSAATotal = UpdateASAC(1) + UpdateASAC(2) + UpdateASAC(3) + UpdateASAC(4) + UpdateASAC(5) + UpdateASAC(6) + UpdateASAC(7) + UpdateASAC(8) + UpdateASAC(9) + UpdateASAC(10) + UpdateASAC(11) + UpdateASAC(12);
            OnPropertyChanged("CountSAATotal");

            CountLivJanuary = UpdateLife(1);
            OnPropertyChanged("CountLivJanuary");
            CountLivFebruary = UpdateLife(2);
            OnPropertyChanged("CountLivFebruary");
            CountLivMars = UpdateLife(3);
            OnPropertyChanged("CountLivMars");
            CountLivApril = UpdateLife(4);
            OnPropertyChanged("CountLivApril");
            CountLivMay = UpdateLife(5);
            OnPropertyChanged("CountLivMay");
            CountLivJune = UpdateLife(6);
            OnPropertyChanged("CountLivJune");
            CountLivJuly = UpdateLife(7);
            OnPropertyChanged("CountLivFJuly");
            CountLivAugust = UpdateLife(8);
            OnPropertyChanged("CountLivAugust");
            CountLivSeptember = UpdateLife(9);
            OnPropertyChanged("CountLivSeptember");
            CountLivOctober = UpdateLife(10);
            OnPropertyChanged("CountLivOctober");
            CountLivNovember = UpdateLife(11);
            OnPropertyChanged("CountLivNovember");
            CountLivDecember = UpdateLife(12);
            OnPropertyChanged("CountLivDecember");
            CountLivTotal = UpdateLife(1) + UpdateLife(2) + UpdateLife(3) + UpdateLife(4) + UpdateLife(5) + UpdateLife(6) + UpdateLife(7) + UpdateLife(8) + UpdateLife(9) + UpdateLife(10) + UpdateLife(11) + UpdateLife(12);
            OnPropertyChanged("CountLivTotal");

            CountOPJanuary = UpdateOP(1);
            OnPropertyChanged("CountOPJanuary");
            CountOPFebruary = UpdateOP(2);
            OnPropertyChanged("CountOPFebruary");
            CountOPMars = UpdateOP(3);
            OnPropertyChanged("CountOPMars");
            CountOPApril = UpdateOP(4);
            OnPropertyChanged("CountOPApril");
            CountOPMay = UpdateOP(5);
            OnPropertyChanged("CountOPMay");
            CountOPJune = UpdateOP(6);
            OnPropertyChanged("CountOPJune");
            CountOPJuly = UpdateOP(7);
            OnPropertyChanged("CountOPJuly");
            CountOPAugust = UpdateOP(8);
            OnPropertyChanged("CountOPAugust");
            CountOPSeptember = UpdateOP(9);
            OnPropertyChanged("CountOPSeptember");
            CountOPOctober = UpdateOP(10);
            OnPropertyChanged("CountOPOctober");
            CountOPNovember = UpdateOP(11);
            OnPropertyChanged("CountOPNovember");
            CountOPDecember = UpdateOP(12);
            OnPropertyChanged("CountOPDecember");
            CountOPTotal = UpdateOP(1) + UpdateOP(2) + UpdateOP(3) + UpdateOP(4) + UpdateOP(5) + UpdateOP(6) + UpdateOP(7) + UpdateOP(8) + UpdateOP(9) + UpdateOP(10) + UpdateOP(11) + UpdateOP(12);
            OnPropertyChanged("CountOPTotal");

            CountCIJanuary = UpdateCI(1);
            OnPropertyChanged("CountCIJanuary");
            CountCIFebruary = UpdateCI(2);
            OnPropertyChanged("CountCIFebruary");
            CountCIMars = UpdateCI(3);
            OnPropertyChanged("CountCIMars");
            CountCIApril = UpdateCI(4);
            OnPropertyChanged("CountCIApril");
            CountCIMay = UpdateCI(5);
            OnPropertyChanged("CountCIMay");
            CountCIJune = UpdateCI(6);
            OnPropertyChanged("CountCIJune");
            CountCIJuly = UpdateCI(7);
            OnPropertyChanged("CountCIJuly");
            CountCIAugust = UpdateCI(8);
            OnPropertyChanged("CountCIAugust");
            CountCISeptember = UpdateCI(9);
            OnPropertyChanged("CountCISeptember");
            CountCIOctober = UpdateCI(10);
            OnPropertyChanged("CountCIOctober");
            CountCINovember = UpdateCI(11);
            OnPropertyChanged("CountCINovember");
            CountCIDecember = UpdateCI(12);
            OnPropertyChanged("CountCIDecember");
            CountCITotal = UpdateCI(1) + UpdateCI(2) + UpdateCI(3) + UpdateCI(4) + UpdateCI(5) + UpdateCI(6) + UpdateCI(7) + UpdateCI(8) + UpdateCI(9) + UpdateCI(10) + UpdateCI(11) + UpdateCI(12);
            OnPropertyChanged("CountCITotal");

            CountJanuary = UpdateTotal(1);
            OnPropertyChanged("CountJanuary");
            CountFebruary = UpdateTotal(2);
            OnPropertyChanged("CountFebruary");
            CountMars = UpdateTotal(3);
            OnPropertyChanged("CountMars");
            CountApril = UpdateTotal(4);
            OnPropertyChanged("CountApril");
            CountMay = UpdateTotal(5);
            OnPropertyChanged("CountMay");
            CountJune = UpdateTotal(6);
            OnPropertyChanged("CountJune");
            CountJuly = UpdateTotal(7);
            OnPropertyChanged("CountJuly");
            CountAugust = UpdateTotal(8);
            OnPropertyChanged("CountAugust");
            CountSeptember = UpdateTotal(9);
            OnPropertyChanged("CountSeptember");
            CountOctober = UpdateTotal(10);
            OnPropertyChanged("CountOctober");
            CountNovember = UpdateTotal(11);
            OnPropertyChanged("CountNovember");
            CountDecember = UpdateTotal(12);
            OnPropertyChanged("CountDecember");
            CountTotalM = UpdateTotal(1) + UpdateTotal(2) + UpdateTotal(3) + UpdateTotal(4) + UpdateTotal(5) + UpdateTotal(6) + UpdateTotal(7) + UpdateTotal(8) + UpdateTotal(9) + UpdateTotal(10) + UpdateTotal(11) + UpdateTotal(12);
            OnPropertyChanged("CountTotalM");
            #endregion
            #region all ackvisitions. 
            SalesMen s = Instance.SalesMen;

            AckSACJanuary = CreateSMAVC(s, 1, year);
            OnPropertyChanged("AckSACJanuary");
            AckSACFebruary = CreateSMAVC(s, 2, year);
            OnPropertyChanged("AckSACFebruary");
            AckSACMars = CreateSMAVC(s, 3, year);
            OnPropertyChanged("AckSACMars");
            AckSACApril = CreateSMAVC(s, 4, year);
            OnPropertyChanged("AckSACApril");
            AckSACMay = CreateSMAVC(s, 5, year);
            OnPropertyChanged("AckSACMay");
            AckSACJune = CreateSMAVC(s, 6, year);
            OnPropertyChanged("AckSACJune");
            AckSACJuly = CreateSMAVC(s, 7, year);
            OnPropertyChanged("AckSACJuly");
            AckSACAugust = CreateSMAVC(s, 8, year);
            OnPropertyChanged("AckSACAugust");
            AckSACSeptember = CreateSMAVC(s, 9, year);
            OnPropertyChanged("AckSACSeptember");
            AckSACOctober = CreateSMAVC(s, 10, year);
            OnPropertyChanged("AckSACOctober");
            AckSACNovember = CreateSMAVC(s, 11, year);
            OnPropertyChanged("AckSACNovember");
            AckSACDecember = CreateSMAVC(s, 12, year);
            OnPropertyChanged("AckSACDecember");
            AckSACTotal = CreateSMAVC(s, 1, year) + CreateSMAVC(s, 2, year) + CreateSMAVC(s, 3, year) + CreateSMAVC(s, 4, year) + CreateSMAVC(s, 5, year) + CreateSMAVC(s, 6, year) + CreateSMAVC(s, 7, year) + CreateSMAVC(s, 8, year) + CreateSMAVC(s, 9, year) + CreateSMAVC(s, 10, year) + CreateSMAVC(s, 11, year) + CreateSMAVC(s, 12, year);
            OnPropertyChanged("AckSACTotal");

            AckSAAJanuary = CreateSMAVA(s, 1, year);
            OnPropertyChanged("AckSAAJanuary");
            AckSAAFebruary = CreateSMAVA(s, 2, year);
            OnPropertyChanged("AckSAAFebruary");
            AckSAAMars = CreateSMAVA(s, 3, year);
            OnPropertyChanged("AckSAAMars");
            AckSAAApril = CreateSMAVA(s, 4, year);
            OnPropertyChanged("AckSAAApril");
            AckSAAMay = CreateSMAVA(s, 5, year);
            OnPropertyChanged("AckSAAMay");
            AckSAAJune = CreateSMAVA(s, 6, year);
            OnPropertyChanged("AckSAAJune");
            AckSAAJuly = CreateSMAVA(s, 7, year);
            OnPropertyChanged("AckSAAJuly");
            AckSAAAugust = CreateSMAVA(s, 8, year);
            OnPropertyChanged("AckSAAAugust");
            AckSAASeptember = CreateSMAVA(s, 9, year);
            OnPropertyChanged("AckSAASeptember");
            AckSAAOctober = CreateSMAVA(s, 10, year);
            OnPropertyChanged("AckSAAOctober");
            AckSAANovember = CreateSMAVA(s, 11, year);
            OnPropertyChanged("AckSAANovember");
            AckSAADecember = CreateSMAVA(s, 12, year);
            OnPropertyChanged("AckSAADecember");
            AckSAATotal = CreateSMAVA(s, 1, year) + CreateSMAVA(s, 2, year) + CreateSMAVA(s, 3, year) + CreateSMAVA(s, 4, year) + CreateSMAVA(s, 5, year) + CreateSMAVA(s, 6, year) + CreateSMAVA(s, 7, year) + CreateSMAVA(s, 8, year) + CreateSMAVA(s, 9, year) + CreateSMAVA(s, 10, year) + CreateSMAVA(s, 11, year) + CreateSMAVA(s, 12, year);
            OnPropertyChanged("AckSAATotal");

            AckLivJanuary = CreateLife(s, 1, year);
            OnPropertyChanged("AckLivJanuary");
            AckLivFebruary = CreateLife(s, 2, year);
            OnPropertyChanged("AckLivFebruary");
            AckLivMars = CreateLife(s, 3, year);
            OnPropertyChanged("AckLivMars");
            AckLivApril = CreateLife(s, 4, year);
            OnPropertyChanged("AckLivApril");
            AckLivMay = CreateLife(s, 5, year);
            OnPropertyChanged("AckLivMay");
            AckLivJune = CreateLife(s, 6, year);
            OnPropertyChanged("AckLivJune");
            AckLivJuly = CreateLife(s, 7, year);
            OnPropertyChanged("AckLivJuly");
            AckLivAugust = CreateLife(s, 8, year);
            OnPropertyChanged("AckLivAugust");
            AckLivSeptember = CreateLife(s, 9, year);
            OnPropertyChanged("AckLivSeptember");
            AckLivOctober = CreateLife(s, 10, year);
            OnPropertyChanged("AckLivOctober");
            AckLivNovember = CreateLife(s, 11, year);
            OnPropertyChanged("AckLivNovember");
            AckLivDecember = CreateLife(s, 12, year);
            OnPropertyChanged("AckLivDecember");
            AckLivTotal = CreateLife(s, 1, year) + CreateLife(s, 2, year) + CreateLife(s, 3, year) + CreateLife(s, 4, year) + CreateLife(s, 5, year) + CreateLife(s, 6, year) + CreateLife(s, 7, year) + CreateLife(s, 8, year) + CreateLife(s, 9, year) + CreateLife(s, 10, year) + CreateLife(s, 11, year) + CreateLife(s, 12, year);
            OnPropertyChanged("AckLivTotal");

            AckOPJanuary = CreateOP(s, 1, year);
            OnPropertyChanged("AckOPJanuary");
            AckOPFebruary = CreateOP(s, 2, year);
            OnPropertyChanged("AckOPFebruary");
            AckOPMars = CreateOP(s, 3, year);
            OnPropertyChanged("AckOPMars");
            AckOPApril = CreateOP(s, 4, year);
            OnPropertyChanged("AckOPApril");
            AckOPMay = CreateOP(s, 5, year);
            OnPropertyChanged("AckOPMay");
            AckOPJune = CreateOP(s, 6, year);
            OnPropertyChanged("AckOPJune");
            AckOPJuly = CreateOP(s, 7, year);
            OnPropertyChanged("AckOPJuly");
            AckOPAugust = CreateOP(s, 8, year);
            OnPropertyChanged("AckOPAugust");
            AckOPSeptember = CreateOP(s, 9, year);
            OnPropertyChanged("AckOPSeptember");
            AckOPOctober = CreateOP(s, 10, year);
            OnPropertyChanged("AckOPOctober");
            AckOPNovember = CreateOP(s, 11, year);
            OnPropertyChanged("AckOPNovember");
            AckOPDecember = CreateOP(s, 12, year);
            OnPropertyChanged("AckOPDecember");
            AckOPTotal = CreateOP(s, 1, year) + CreateOP(s, 2, year) + CreateOP(s, 3, year) + CreateOP(s, 4, year) + CreateOP(s, 5, year) + CreateOP(s, 6, year) + CreateOP(s, 7, year) + CreateOP(s, 8, year) + CreateOP(s, 9, year) + CreateOP(s, 10, year) + CreateOP(s, 11, year) + CreateOP(s, 12, year);
            OnPropertyChanged("AckOPTotal");

            AckCIJanuary = CreateCI(s, 1, year);
            OnPropertyChanged("AckCIJanuary");
            AckCIFebruary = CreateCI(s, 2, year);
            OnPropertyChanged("AckCIFebruary");
            AckCIMars = CreateCI(s, 3, year);
            OnPropertyChanged("AckCIMars");
            AckCIApril = CreateCI(s, 4, year);
            OnPropertyChanged("AckCIApril");
            AckCIMay = CreateCI(s, 5, year);
            OnPropertyChanged("AckCIMay");
            AckCIJune = CreateCI(s, 6, year);
            OnPropertyChanged("AckCIJune");
            AckCIJuly = CreateCI(s, 7, year);
            OnPropertyChanged("AckCIJuly");
            AckCIAugust = CreateCI(s, 8, year);
            OnPropertyChanged("AckCIAugust");
            AckCISeptember = CreateCI(s, 9, year);
            OnPropertyChanged("AckCISeptember");
            AckCIOctober = CreateCI(s, 10, year);
            OnPropertyChanged("AckCIOctober");
            AckCINovember = CreateCI(s, 11, year);
            OnPropertyChanged("AckCINovember");
            AckCIDecember = CreateCI(s, 12, year);
            OnPropertyChanged("AckCIDecember");
            AckCITotal = CreateCI(s, 1, year) + CreateCI(s, 2, year) + CreateCI(s, 3, year) + CreateCI(s, 4, year) + CreateCI(s, 5, year) + CreateCI(s, 6, year) + CreateCI(s, 7, year) + CreateCI(s, 8, year) + CreateCI(s, 9, year) + CreateCI(s, 10, year) + CreateCI(s, 11, year) + CreateCI(s, 12, year);
            OnPropertyChanged("AckCITotal");

            AckJanuary = CountMonthTotalSM(s, 1, year);
            OnPropertyChanged("AckJanuary");
            AckFebruary = CountMonthTotalSM(s, 2, year);
            OnPropertyChanged("AckFebruary");
            AckMars = CountMonthTotalSM(s, 3, year);
            OnPropertyChanged("AckMars");
            AckApril = CountMonthTotalSM(s, 4, year);
            OnPropertyChanged("AckApril");
            AckMay = CountMonthTotalSM(s, 5, year);
            OnPropertyChanged("AckMay");
            AckJune = CountMonthTotalSM(s, 6, year);
            OnPropertyChanged("AckJune");
            AckJuly = CountMonthTotalSM(s, 7, year);
            OnPropertyChanged("AckJuly");
            AckAugust = CountMonthTotalSM(s, 8, year);
            OnPropertyChanged("AckAugust");
            AckSeptember = CountMonthTotalSM(s, 9, year);
            OnPropertyChanged("AckSeptember");
            AckOctober = CountMonthTotalSM(s, 10, year);
            OnPropertyChanged("AckOctober");
            AckNovember = CountMonthTotalSM(s, 11, year);
            OnPropertyChanged("AckNovember");
            AckDecember = CountMonthTotalSM(s, 12, year);
            OnPropertyChanged("AckDecember");
            AckTotalM = CountTotal(s, year);
            OnPropertyChanged("AckTotalM");
            #endregion
        }

        #endregion

        #region Properties Ackvalue per month and total. 
        private double _AckSACJanuary;

        public double AckSACJanuary
        {
            get => _AckSACJanuary;
            set { _AckSACJanuary = value; OnPropertyChanged("AckSACJanuary"); }
        }

        private double _AckSACFebruary;

        public double AckSACFebruary
        {
            get => _AckSACFebruary;
            set { _AckSACFebruary = value; OnPropertyChanged("AckSACFebruary"); }
        }

        private double _AckSACMars;
        public double AckSACMars
        {
            get => _AckSACMars;
            set { _AckSACMars = value; OnPropertyChanged("AckSACMars"); }
        }

        private double _AckSACApril;
        public double AckSACApril
        {
            get => _AckSACApril;
            set { _AckSACApril = value; OnPropertyChanged("AckSACApril"); }
        }

        private double _AckSACMay;

        public double AckSACMay
        {
            get => _AckSACMay;
            set { _AckSACMay = value; OnPropertyChanged("AckSACMay"); }
        }

        private double _AckSACJune;

        public double AckSACJune
        {
            get => _AckSACJune;
            set { _AckSACJune = value; OnPropertyChanged("AckSACJune"); }
        }

        private double _AckSACJuly;
        public double AckSACJuly
        {
            get => _AckSACJuly;
            set { _AckSACJuly = value; OnPropertyChanged("AckSACJuly"); }
        }

        private double _AckSACAugust;
        public double AckSACAugust
        {
            get => _AckSACAugust;
            set { _AckSACAugust = value; OnPropertyChanged("AckSACAugust"); }
        }

        private double _AckSACSeptember;

        public double AckSACSeptember
        {
            get => _AckSACSeptember;
            set { _AckSACSeptember = value; OnPropertyChanged("AckSACSeptember"); }
        }

        private double _AckSACOctober;

        public double AckSACOctober
        {
            get => _AckSACOctober;
            set { _AckSACOctober = value; OnPropertyChanged("AckSACOctober"); }
        }

        private double _AckSACNovember;
        public double AckSACNovember
        {
            get => _AckSACNovember;
            set { _AckSACNovember = value; OnPropertyChanged("AckSACNovember"); }
        }

        private double _AckSACDecember;
        public double AckSACDecember
        {
            get => _AckSACDecember;
            set { _AckSACDecember = value; OnPropertyChanged("AckSACDecember"); }
        }

        private double _AckSACTotal;

        public double AckSACTotal
        {
            get => _AckSACTotal;
            set { _AckSACTotal = value; OnPropertyChanged("AckSACTotal"); }
        }

        private double _AckSAAJanuary;

        public double AckSAAJanuary
        {
            get => _AckSAAJanuary;
            set { _AckSAAJanuary = value; OnPropertyChanged("AckSAAJanuary"); }
        }

        private double _AckSAAFebruary;

        public double AckSAAFebruary
        {
            get => _AckSAAFebruary;
            set { _AckSAAFebruary = value; OnPropertyChanged("AckSAAFebruary"); }
        }

        private double _AckSAAMars;
        public double AckSAAMars
        {
            get => _AckSAAMars;
            set { _AckSAAMars = value; OnPropertyChanged("AckSAAMars"); }
        }

        private double _AckSAAApril;
        public double AckSAAApril
        {
            get => _AckSAAApril;
            set { _AckSAAApril = value; OnPropertyChanged("AckSAAApril"); }
        }

        private double _AckSAAMay;

        public double AckSAAMay
        {
            get => _AckSAAMay;
            set { _AckSAAMay = value; OnPropertyChanged("AckSAAMay"); }
        }

        private double _AckSAAJune;

        public double AckSAAJune
        {
            get => _AckSAAJune;
            set { _AckSAAJune = value; OnPropertyChanged("AckSAAJune"); }
        }

        private double _AckSAAJuly;
        public double AckSAAJuly
        {
            get => _AckSAAJuly;
            set { _AckSAAJuly = value; OnPropertyChanged("AckSAAJuly"); }
        }

        private double _AckSAAAugust;
        public double AckSAAAugust
        {
            get => _AckSAAAugust;
            set { _AckSAAAugust = value; OnPropertyChanged("AckSAAAugust"); }
        }

        private double _AckSAASeptember;

        public double AckSAASeptember
        {
            get => _AckSAASeptember;
            set { _AckSAASeptember = value; OnPropertyChanged("AckSAASeptember"); }
        }

        private double _AckSAAOctober;

        public double AckSAAOctober
        {
            get => _AckSAAOctober;
            set { _AckSAAOctober = value; OnPropertyChanged("AckSAAOctober"); }
        }

        private double _AckSAANovember;
        public double AckSAANovember
        {
            get => _AckSAANovember;
            set { _AckSAANovember = value; OnPropertyChanged("AckSAANovember"); }
        }

        private double _AckSAADecember;
        public double AckSAADecember
        {
            get => _AckSAADecember;
            set { _AckSAADecember = value; OnPropertyChanged("AckSAADecember"); }
        }

        private double _AckSAATotal;

        public double AckSAATotal
        {
            get => _AckSAATotal;
            set { _AckSAATotal = value; OnPropertyChanged("AckSAATotal"); }
        }

        private double _AckLivJanuary;

        public double AckLivJanuary
        {
            get => _AckLivJanuary;
            set { _AckLivJanuary = value; OnPropertyChanged("AckLivJanuary"); }
        }

        private double _AckLivFebruary;

        public double AckLivFebruary
        {
            get => _AckLivFebruary;
            set { _AckLivFebruary = value; OnPropertyChanged("AckLivFebruary"); }
        }

        private double _AckLivMars;
        public double AckLivMars
        {
            get => _AckLivMars;
            set { _AckLivMars = value; OnPropertyChanged("AckLivMars"); }
        }

        private double _AckLivApril;
        public double AckLivApril
        {
            get => _AckLivApril;
            set { _AckLivApril = value; OnPropertyChanged("AckLivApril"); }
        }

        private double _AckLivMay;

        public double AckLivMay
        {
            get => _AckLivMay;
            set { _AckLivMay = value; OnPropertyChanged("AckLivMay"); }
        }

        private double _AckLivJune;

        public double AckLivJune
        {
            get => _AckLivJune;
            set { _AckLivJune = value; OnPropertyChanged("AckLivJune"); }
        }

        private double _AckLivJuly;
        public double AckLivJuly
        {
            get => _AckLivJuly;
            set { _AckLivJuly = value; OnPropertyChanged("AckLivJuly"); }
        }

        private double _AckLivAugust;
        public double AckLivAugust
        {
            get => _AckLivAugust;
            set { _AckLivAugust = value; OnPropertyChanged("AckLivAugust"); }
        }
        private double _AckLivSeptember;

        public double AckLivSeptember
        {
            get => _AckLivSeptember;
            set { _AckLivSeptember = value; OnPropertyChanged("AckLivSeptember"); }
        }

        private double _AckLivOctober;

        public double AckLivOctober
        {
            get => _AckLivOctober;
            set { _AckLivOctober = value; OnPropertyChanged("AckLivOctober"); }
        }

        private double _AckLivNovember;
        public double AckLivNovember
        {
            get => _AckLivNovember;
            set { _AckLivNovember = value; OnPropertyChanged("AckLivNovember"); }
        }

        private double _AckLivDecember;
        public double AckLivDecember
        {
            get => _AckLivDecember;
            set { _AckLivDecember = value; OnPropertyChanged("AckLivDecember"); }
        }

        private double _AckLivTotal;

        public double AckLivTotal
        {
            get => _AckLivTotal;
            set { _AckLivTotal = value; OnPropertyChanged("AckLivTotal"); }
        }

        private double _AckOPJanuary;

        public double AckOPJanuary
        {
            get => _AckOPJanuary;
            set { _AckOPJanuary = value; OnPropertyChanged("AckOPJanuary"); }
        }

        private double _AckOPFebruary;

        public double AckOPFebruary
        {
            get => _AckOPFebruary;
            set { _AckOPFebruary = value; OnPropertyChanged("AckOPFebruary"); }
        }

        private double _AckOPMars;
        public double AckOPMars
        {
            get => _AckOPMars;
            set { _AckOPMars = value; OnPropertyChanged("AckOPMars"); }
        }

        private double _AckOPApril;
        public double AckOPApril
        {
            get => _AckOPApril;
            set { _AckOPApril = value; OnPropertyChanged("AckOPApril"); }
        }

        private double _AckOPMay;

        public double AckOPMay
        {
            get => _AckOPMay;
            set { _AckOPMay = value; OnPropertyChanged("AckOPMay"); }
        }

        private double _AckOPJune;

        public double AckOPJune
        {
            get => _AckOPJune;
            set { _AckOPJune = value; OnPropertyChanged("AckOPJune"); }
        }

        private double _AckOPJuly;
        public double AckOPJuly
        {
            get => _AckOPJuly;
            set { _AckOPJuly = value; OnPropertyChanged("AckOPJuly"); }
        }

        private double _AckOPAugust;
        public double AckOPAugust
        {
            get => _AckOPAugust;
            set { _AckOPAugust = value; OnPropertyChanged("AckOPAugust"); }
        }


        private double _AckOPSeptember;

        public double AckOPSeptember
        {
            get => _AckOPSeptember;
            set { _AckOPSeptember = value; OnPropertyChanged("AckOPSeptember"); }
        }

        private double _AckOPOctober;

        public double AckOPOctober
        {
            get => _AckOPOctober;
            set { _AckOPOctober = value; OnPropertyChanged("AckOPOctober"); }
        }

        private double _AckOPNovember;
        public double AckOPNovember
        {
            get => _AckOPNovember;
            set { _AckOPNovember = value; OnPropertyChanged("AckOPNovember"); }
        }

        private double _AckOPDecember;
        public double AckOPDecember
        {
            get => _AckOPDecember;
            set { _AckOPDecember = value; OnPropertyChanged("AckOPDecember"); }
        }

        private double _AckOPTotal;

        public double AckOPTotal
        {
            get => _AckOPTotal;
            set { _AckOPTotal = value; OnPropertyChanged("AckOPTotal"); }
        }

        private double _AckCIJanuary;

        public double AckCIJanuary
        {
            get => _AckCIJanuary;
            set { _AckCIJanuary = value; OnPropertyChanged("AckCIJanuary"); }
        }

        private double _AckCIFebruary;

        public double AckCIFebruary
        {
            get => _AckCIFebruary;
            set { _AckCIFebruary = value; OnPropertyChanged("AckCIFebruary"); }
        }

        private double _AckCIMars;
        public double AckCIMars
        {
            get => _AckCIMars;
            set { _AckCIMars = value; OnPropertyChanged("AckCIMars"); }
        }

        private double _AckCIApril;
        public double AckCIApril
        {
            get => _AckCIApril;
            set { _AckCIApril = value; OnPropertyChanged("AckCIApril"); }
        }

        private double _AckCIMay;

        public double AckCIMay
        {
            get => _AckCIMay;
            set { _AckCIMay = value; OnPropertyChanged("AckCIMay"); }
        }

        private double _AckCIJune;

        public double AckCIJune
        {
            get => _AckCIJune;
            set { _AckCIJune = value; OnPropertyChanged("AckCIJune"); }
        }

        private double _AckCIJuly;
        public double AckCIJuly
        {
            get => _AckCIJuly;
            set { _AckCIJuly = value; OnPropertyChanged("AckCIJuly"); }
        }

        private double _AckCIAugust;
        public double AckCIAugust
        {
            get => _AckCIAugust;
            set { _AckCIAugust = value; OnPropertyChanged("AckCIAugust"); }
        }


        private double _AckCISeptember;

        public double AckCISeptember
        {
            get => _AckCISeptember;
            set { _AckCISeptember = value; OnPropertyChanged("AckCISeptember"); }
        }

        private double _AckCIOctober;

        public double AckCIOctober
        {
            get => _AckCIOctober;
            set { _AckCIOctober = value; OnPropertyChanged("AckCIOctober"); }
        }

        private double _AckCINovember;
        public double AckCINovember
        {
            get => _AckCINovember;
            set { _AckCINovember = value; OnPropertyChanged("AckCINovember"); }
        }

        private double _AckCIDecember;
        public double AckCIDecember
        {
            get => _AckCIDecember;
            set { _AckCIDecember = value; OnPropertyChanged("AckCIDecember"); }
        }

        private double _AckCITotal;

        public double AckCITotal
        {
            get => _AckCITotal;
            set { _AckCITotal = value; OnPropertyChanged("AckCITotal"); }
        }

        private double _AckJanuary;

        public double AckJanuary
        {
            get => _AckJanuary;
            set { _AckJanuary = value; OnPropertyChanged("AckJanuary"); }
        }

        private double _AckFebruary;

        public double AckFebruary
        {
            get => _AckFebruary;
            set { _AckFebruary = value; OnPropertyChanged("AckFebruary"); }
        }

        private double _AckMars;
        public double AckMars
        {
            get => _AckMars;
            set { _AckMars = value; OnPropertyChanged("AckMars"); }
        }

        private double _AckApril;
        public double AckApril
        {
            get => _AckApril;
            set { _AckApril = value; OnPropertyChanged("AckApril"); }
        }

        private double _AckMay;

        public double AckMay
        {
            get => _AckMay;
            set { _AckMay = value; OnPropertyChanged("AckMay"); }
        }

        private double _AckJune;

        public double AckJune
        {
            get => _AckJune;
            set { _AckJune = value; OnPropertyChanged("AckJune"); }
        }

        private double _AckJuly;
        public double AckJuly
        {
            get => _AckJuly;
            set { _AckJuly = value; OnPropertyChanged("AckJuly"); }
        }

        private double _AckAugust;
        public double AckAugust
        {
            get => _AckAugust;
            set { _AckAugust = value; OnPropertyChanged("AckAugust"); }
        }

        private double _AckSeptember;

        public double AckSeptember
        {
            get => _AckSeptember;
            set { _AckSeptember = value; OnPropertyChanged("AckSeptember"); }
        }

        private double _AckOctober;

        public double AckOctober
        {
            get => _AckOctober;
            set { _AckOctober = value; OnPropertyChanged("AckOctober"); }
        }

        private double _November;
        public double AckNovember
        {
            get => _November;
            set { _November = value; OnPropertyChanged("AckNovember"); }
        }

        private double _AckDecember;
        public double AckDecember
        {
            get => _AckDecember;
            set { _AckDecember = value; OnPropertyChanged("AckDecember"); }
        }

        private double _AckTotalM;

        public double AckTotalM
        {
            get => _AckTotalM;
            set { _AckTotalM = value; OnPropertyChanged("AckTotalM"); }
        }
        #endregion

        #region Properties for number of each type per month and total. 
        private int _countSACJanuary;

        public int CountSACJanuary
        {
            get => _countSACJanuary;
            set { _countSACJanuary = value; OnPropertyChanged("CountSACJanuary"); }
        }

        private int _countSACFebruary;

        public int CountSACFebruary
        {
            get => _countSACFebruary;
            set { _countSACFebruary = value; OnPropertyChanged("CountSACFebruary"); }
        }

        private int _countSACMars;
        public int CountSACMars
        {
            get => _countSACMars;
            set { _countSACMars = value; OnPropertyChanged("CountSACMars"); }
        }

        private int _countSACApril;
        public int CountSACApril
        {
            get => _countSACApril;
            set { _countSACApril = value; OnPropertyChanged("CountSACApril"); }
        }

        private int _countSACMay;

        public int CountSACMay
        {
            get => _countSACMay;
            set { _countSACMay = value; OnPropertyChanged("CountSACMay"); }
        }

        private int _countSACJune;

        public int CountSACJune
        {
            get => _countSACJune;
            set { _countSACJune = value; OnPropertyChanged("CountSACJune"); }
        }

        private int _countSACJuly;
        public int CountSACJuly
        {
            get => _countSACJuly;
            set { _countSACJuly = value; OnPropertyChanged("CountSACJuly"); }
        }

        private int _countSACAugust;
        public int CountSACAugust
        {
            get => _countSACAugust;
            set { _countSACAugust = value; OnPropertyChanged("CountSACAugust"); }
        }


        private int _countSACSeptember;

        public int CountSACSeptember
        {
            get => _countSACSeptember;
            set { _countSACSeptember = value; OnPropertyChanged("CountSACSeptember"); }
        }

        private int _countSACOctober;

        public int CountSACOctober
        {
            get => _countSACOctober;
            set { _countSACOctober = value; OnPropertyChanged("CountSACOctober"); }
        }

        private int _countSACNovember;
        public int CountSACNovember
        {
            get => _countSACNovember;
            set { _countSACNovember = value; OnPropertyChanged("CountSACNovember"); }
        }

        private int _countSACDecember;
        public int CountSACDecember
        {
            get => _countSACDecember;
            set { _countSACDecember = value; OnPropertyChanged("CountSACDecember"); }
        }
       
        private int _countSACTotal;

        public int CountSACTotal
        {
            get => _countSACTotal;
            set { _countSACTotal = value; OnPropertyChanged("CountSACTotal"); }
        }

        private int _countSAAJanuary;

        public int CountSAAJanuary
        {
            get => _countSAAJanuary;
            set { _countSAAJanuary = value; OnPropertyChanged("CountSAAJanuary"); }
        }

        private int _countSAAFebruary;

        public int CountSAAFebruary
        {
            get => _countSAAFebruary;
            set { _countSAAFebruary = value; OnPropertyChanged("CountSAAFebruary"); }
        }

        private int _countSAAMars;
        public int CountSAAMars
        {
            get => _countSAAMars;
            set { _countSAAMars = value; OnPropertyChanged("CountSAAMars"); }
        }

        private int _countSAAApril;
        public int CountSAAApril
        {
            get => _countSAAApril;
            set { _countSAAApril = value; OnPropertyChanged("CountSAAApril"); }
        }

        private int _countSAAMay;

        public int CountSAAMay
        {
            get => _countSAAMay;
            set { _countSAAMay = value; OnPropertyChanged("CountSAAMay"); }
        }

        private int _countSAAJune;

        public int CountSAAJune
        {
            get => _countSAAJune;
            set { _countSAAJune = value; OnPropertyChanged("CountSAAJune"); }
        }

        private int _countSAAJuly;
        public int CountSAAJuly
        {
            get => _countSAAJuly;
            set { _countSAAJuly = value; OnPropertyChanged("CountSAAJuly"); }
        }

        private int _countSAAAugust;
        public int CountSAAAugust
        {
            get => _countSAAAugust;
            set { _countSAAAugust = value; OnPropertyChanged("CountSAAAugust"); }
        }


        private int _countSAASeptember;

        public int CountSAASeptember
        {
            get => _countSAASeptember;
            set { _countSAASeptember = value; OnPropertyChanged("CountSAASeptember"); }
        }

        private int _countSAAOctober;

        public int CountSAAOctober
        {
            get => _countSAAOctober;
            set { _countSAAOctober = value; OnPropertyChanged("CountSAAOctober"); }
        }

        private int _countSAANovember;
        public int CountSAANovember
        {
            get => _countSAANovember;
            set { _countSAANovember = value; OnPropertyChanged("CountSAANovember"); }
        }

        private int _countSAADecember;
        public int CountSAADecember
        {
            get => _countSAADecember;
            set { _countSAADecember = value; OnPropertyChanged("CountSAADecember"); }
        }

        private int _countSAATotal;

        public int CountSAATotal
        {
            get => _countSAATotal;
            set { _countSAATotal = value; OnPropertyChanged("CountSAATotal"); }
        }

        private int _countLivJanuary;

        public int CountLivJanuary
        {
            get => _countLivJanuary;
            set { _countLivJanuary = value; OnPropertyChanged("CountLivJanuary"); }
        }

        private int _countLivFebruary;

        public int CountLivFebruary
        {
            get => _countLivFebruary;
            set { _countLivFebruary = value; OnPropertyChanged("CountLivFebruary"); }
        }

        private int _countLivMars;
        public int CountLivMars
        {
            get => _countLivMars;
            set { _countLivMars = value; OnPropertyChanged("CountLivMars"); }
        }

        private int _countLivApril;
        public int CountLivApril
        {
            get => _countLivApril;
            set { _countLivApril = value; OnPropertyChanged("CountLivApril"); }
        }

        private int _countLivMay;

        public int CountLivMay
        {
            get => _countLivMay;
            set { _countLivMay = value; OnPropertyChanged("CountLivMay"); }
        }

        private int _countLivJune;

        public int CountLivJune
        {
            get => _countLivJune;
            set { _countLivJune = value; OnPropertyChanged("CountLivJune"); }
        }

        private int _countLivJuly;
        public int CountLivJuly
        {
            get => _countLivJuly;
            set { _countLivJuly = value; OnPropertyChanged("CountLivJuly"); }
        }

        private int _countLivAugust;
        public int CountLivAugust
        {
            get => _countLivAugust;
            set { _countLivAugust = value; OnPropertyChanged("CountLivAugust"); }
        }


        private int _countLivSeptember;

        public int CountLivSeptember
        {
            get => _countLivSeptember;
            set { _countLivSeptember = value; OnPropertyChanged("CountLivSeptember"); }
        }

        private int _countLivOctober;

        public int CountLivOctober
        {
            get => _countLivOctober;
            set { _countLivOctober = value; OnPropertyChanged("CountLivOctober"); }
        }

        private int _countLivNovember;
        public int CountLivNovember
        {
            get => _countLivNovember;
            set { _countLivNovember = value; OnPropertyChanged("CountLivNovember"); }
        }

        private int _countLivDecember;
        public int CountLivDecember
        {
            get => _countLivDecember;
            set { _countLivDecember = value; OnPropertyChanged("CountLivDecember"); }
        }

        private int _countLivTotal;

        public int CountLivTotal
        {
            get => _countLivTotal;
            set { _countLivTotal = value; OnPropertyChanged("CountLivTotal"); }
        }

        private int _countOPJanuary;

        public int CountOPJanuary
        {
            get => _countOPJanuary;
            set { _countOPJanuary = value; OnPropertyChanged("CountOPJanuary"); }
        }

        private int _countOPFebruary;

        public int CountOPFebruary
        {
            get => _countOPFebruary;
            set { _countOPFebruary = value; OnPropertyChanged("CountOPFebruary"); }
        }

        private int _countOPMars;
        public int CountOPMars
        {
            get => _countOPMars;
            set { _countOPMars = value; OnPropertyChanged("CountOPMars"); }
        }

        private int _countOPApril;
        public int CountOPApril
        {
            get => _countOPApril;
            set { _countOPApril = value; OnPropertyChanged("CountOPApril"); }
        }

        private int _countOPMay;

        public int CountOPMay
        {
            get => _countOPMay;
            set { _countOPMay = value; OnPropertyChanged("CountOPMay"); }
        }

        private int _countOPJune;

        public int CountOPJune
        {
            get => _countOPJune;
            set { _countOPJune = value; OnPropertyChanged("CountOPJune"); }
        }

        private int _countOPJuly;
        public int CountOPJuly
        {
            get => _countOPJuly;
            set { _countOPJuly = value; OnPropertyChanged("CountOPJuly"); }
        }

        private int _countOPAugust;
        public int CountOPAugust
        {
            get => _countOPAugust;
            set { _countOPAugust = value; OnPropertyChanged("CountOPAugust"); }
        }


        private int _countOPSeptember;

        public int CountOPSeptember
        {
            get => _countOPSeptember;
            set { _countOPSeptember = value; OnPropertyChanged("CountOPSeptember"); }
        }

        private int _countOPOctober;

        public int CountOPOctober
        {
            get => _countOPOctober;
            set { _countOPOctober = value; OnPropertyChanged("CountOPOctober"); }
        }

        private int _countOPNovember;
        public int CountOPNovember
        {
            get => _countOPNovember;
            set { _countOPNovember = value; OnPropertyChanged("CountOPNovember"); }
        }

        private int _countOPDecember;
        public int CountOPDecember
        {
            get => _countOPDecember;
            set { _countOPDecember = value; OnPropertyChanged("CountOPDecember"); }
        }

        private int _countOPTotal;

        public int CountOPTotal
        {
            get => _countOPTotal;
            set { _countOPTotal = value; OnPropertyChanged("CountOPTotal"); }
        }

        private int _countCIJanuary;

        public int CountCIJanuary
        {
            get => _countCIJanuary;
            set { _countCIJanuary = value; OnPropertyChanged("CountCIJanuary"); }
        }

        private int _countCIFebruary;

        public int CountCIFebruary
        {
            get => _countCIFebruary;
            set { _countCIFebruary = value; OnPropertyChanged("CountCIFebruary"); }
        }

        private int _countCIMars;
        public int CountCIMars
        {
            get => _countCIMars;
            set { _countCIMars = value; OnPropertyChanged("CountCIMars"); }
        }

        private int _countCIApril;
        public int CountCIApril
        {
            get => _countCIApril;
            set { _countCIApril = value; OnPropertyChanged("CountCIApril"); }
        }

        private int _countCIMay;

        public int CountCIMay
        {
            get => _countCIMay;
            set { _countCIMay = value; OnPropertyChanged("CountCIMay"); }
        }

        private int _countCIJune;

        public int CountCIJune
        {
            get => _countCIJune;
            set { _countCIJune = value; OnPropertyChanged("CountCIJune"); }
        }

        private int _countCIJuly;
        public int CountCIJuly
        {
            get => _countCIJuly;
            set { _countCIJuly = value; OnPropertyChanged("CountCIJuly"); }
        }

        private int _countCIAugust;
        public int CountCIAugust
        {
            get => _countCIAugust;
            set { _countCIAugust = value; OnPropertyChanged("CountCIAugust"); }
        }


        private int _countCISeptember;

        public int CountCISeptember
        {
            get => _countCISeptember;
            set { _countCISeptember = value; OnPropertyChanged("CountCISeptember"); }
        }

        private int _countCIOctober;

        public int CountCIOctober
        {
            get => _countCIOctober;
            set { _countCIOctober = value; OnPropertyChanged("CountCIOctober"); }
        }

        private int _countCINovember;
        public int CountCINovember
        {
            get => _countCINovember;
            set { _countCINovember = value; OnPropertyChanged("CountCINovember"); }
        }

        private int _countCIDecember;
        public int CountCIDecember
        {
            get => _countCIDecember;
            set { _countCIDecember = value; OnPropertyChanged("CountCIDecember"); }
        }

        private int _countCITotal;

        public int CountCITotal
        {
            get => _countCITotal;
            set { _countCITotal = value; OnPropertyChanged("CountCITotal"); }
        }

        private int _countJanuary;

        public int CountJanuary
        {
            get => _countJanuary;
            set { _countJanuary = value; OnPropertyChanged("CountJanuary"); }
        }

        private int _countFebruary;

        public int CountFebruary
        {
            get => _countFebruary;
            set { _countFebruary = value; OnPropertyChanged("CountFebruary"); }
        }

        private int _countMars;
        public int CountMars
        {
            get => _countMars;
            set { _countMars = value; OnPropertyChanged("CountMars"); }
        }

        private int _countApril;
        public int CountApril
        {
            get => _countApril;
            set { _countApril = value; OnPropertyChanged("CountApril"); }
        }

        private int _countMay;

        public int CountMay
        {
            get => _countMay;
            set { _countMay = value; OnPropertyChanged("CountMay"); }
        }

        private int _countJune;

        public int CountJune
        {
            get => _countJune;
            set { _countJune = value; OnPropertyChanged("CountJune"); }
        }

        private int _countJuly;
        public int CountJuly
        {
            get => _countJuly;
            set { _countJuly = value; OnPropertyChanged("CountJuly"); }
        }

        private int _countAugust;
        public int CountAugust
        {
            get => _countAugust;
            set { _countAugust = value; OnPropertyChanged("CountAugust"); }
        }

        private int _countSeptember;

        public int CountSeptember
        {
            get => _countSeptember;
            set { _countSeptember = value; OnPropertyChanged("CountSeptember"); }
        }

        private int _countOctober;

        public int CountOctober
        {
            get => _countOctober;
            set { _countOctober = value; OnPropertyChanged("CountOctober"); }
        }

        private int _countNovember;
        public int CountNovember
        {
            get => _countNovember;
            set { _countNovember = value; OnPropertyChanged("CountNovember"); }
        }

        private int _countDecember;
        public int CountDecember
        {
            get => _countDecember;
            set { _countDecember = value; OnPropertyChanged("CountDecember"); }
        }

        private int _countTotalM;

        public int CountTotalM
        {
            get => _countTotalM;
            set { _countTotalM = value; OnPropertyChanged("CountTotalM"); }
        }
        #endregion

        #region methods for excel int/doubles. 

        private double CountTotal(SalesMen sm, int year)
        {
            double a = CountMonthTotalSM(sm, 1, year);
            double b = CountMonthTotalSM(sm, 2, year);
            double c = CountMonthTotalSM(sm, 3, year);
            double d = CountMonthTotalSM(sm, 4, year);
            double e = CountMonthTotalSM(sm, 5, year);
            double f = CountMonthTotalSM(sm, 6, year);
            double g = CountMonthTotalSM(sm, 7, year);
            double h = CountMonthTotalSM(sm, 8, year);
            double i = CountMonthTotalSM(sm, 9, year);
            double j = CountMonthTotalSM(sm, 10, year);
            double k = CountMonthTotalSM(sm, 11, year);
            double l = CountMonthTotalSM(sm, 12, year);
            double avg = a + b + c + d + e + f + g + h + i + j + k + l;
            return avg;
        }
        private double CountMonthTotalSM(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year)
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private double CreateSMAVC(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.SAI != null)
                {
                    if (i.PayMonth == Month && i.SAI.SAID == 1)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private double CreateLife(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.LIFE != null)
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private double CreateOP(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.OPI != null)
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private double CreateCI(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.COI != null)
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private double CreateSMAVA(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.SAI != null)
                {
                    if (i.PayMonth == Month && i.SAI.SAID == 2)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        private int UpdateASAC(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.AgentNo.Equals(Instance.SalesMen) && insurance.PayMonth == month && insurance.PayYear == Year && insurance.SAI != null && insurance.InsuranceStatus == Status.Tecknad)
                {
                    if (insurance.SAI.SAID == 2)
                        k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        private int UpdateCSAC(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.AgentNo.Equals(Instance.SalesMen) && insurance.PayMonth == month && insurance.PayYear == Year && insurance.SAI != null && insurance.InsuranceStatus == Status.Tecknad)
                {
                    if (insurance.SAI.SAID == 1)
                        k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        private int UpdateLife(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.InsuranceStatus == Status.Tecknad && insurance.AgentNo.Equals(Instance.SalesMen) && insurance.PayMonth == month && insurance.PayYear == Year && insurance.LIFE != null)
                {
                    k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        private int UpdateCI(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.InsuranceStatus == Status.Tecknad && insurance.AgentNo.Equals(Instance.SalesMen) && insurance.PayMonth == month && insurance.PayYear == Year && insurance.COI != null)
                {
                    k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        private int UpdateOP(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.InsuranceStatus == Status.Tecknad && insurance.AgentNo.Equals(Instance.SalesMen) && insurance.PayMonth == month && insurance.PayYear == Year && insurance.OPI != null)
                {
                    k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        private int UpdateTotal(int month)
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if (insurance.AgentNo.Equals(Instance.SalesMen) && insurance.InsuranceStatus == Status.Tecknad && insurance.PayMonth == month && insurance.PayYear == Year)
                {
                    k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }
        #endregion
    }
}
