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
    public class GetTrendstatitcsViewModel : BaseViewModel
    {
        public static readonly GetTrendstatitcsViewModel Instance = new GetTrendstatitcsViewModel();
        public GetTrendstatitcsViewModel()
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
                if (i.PayYear != null && i.SAI != null)
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
                if (i.InsuranceStatus == Status.Tecknad && i.SAI != null)
                {
                    x?.Add(i.AgentNo);
                }
            }
            var sm = x.Union(x).ToList();
            foreach(var s in sm)
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
        private ICommand trendstatic_btn;

        public ICommand ExportBtn => trendstatic_btn ?? (trendstatic_btn = new RelayCommand(x => { SendStatisticsToExcel(); }));

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
            Worksheet xlWorksheet1;
            xlApp.DisplayAlerts = false;
            object value = Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(value);
            xlWorksheet = xlWorkBook.ActiveSheet as Worksheet;
            xlWorksheet.Name = "Alla säljares trendstatstik";
            xlWorksheet1 = xlWorkBook.Sheets.Add(value, value, 1, value) as Worksheet;
            xlWorksheet1.Name = "Vald säljares trendstatistik";
            
            ExcelPrint(xlWorksheet, xlWorksheet1, year);
            CreateChart(xlWorksheet1, value, salesMen, year);
            xlWorksheet.get_Range("A1", "AN1").Font.Bold = true;
            xlWorksheet1.get_Range("A1", "M1").Font.Bold = true;
            xlWorksheet.get_Range("A1", "AN1").VerticalAlignment = XlVAlign.xlVAlignCenter;

            if (System.IO.File.Exists("Excel_Trendstatistik"))
            {
                xlWorkBook.SaveAs("Excel_Trendstatistik", Excel.XlFileFormat.xlWorkbookNormal,
                value, value, value, value, Excel.XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            else
            {
                xlWorkBook.SaveAs("Excel_Trendstatistik", Excel.XlFileFormat.xlWorkbookNormal,
                value, value, value, value, Excel.XlSaveAsAccessMode.xlExclusive,
                value, value, value, value, value);
            }
            xlApp.Visible = true;
            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorkBook);
            xlApp.DisplayAlerts = true;
            Marshal.ReleaseComObject(xlApp);
        }
        /// <summary>
        /// Method to create the chart
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="value"></param>
        private void CreateChart(Worksheet xlWorksheet, object value, SalesMen sm, int year)
        {
            Range chartRange;
            ChartObjects xlCharts = (ChartObjects)xlWorksheet.ChartObjects(Type.Missing);
            ChartObject myChart = xlCharts.Add(150, 80, 700, 300);
            Chart chartPage = myChart.Chart;
            int rangeColumns = xlWorksheet.UsedRange.Columns.Count;
            int rangeRows = xlWorksheet.UsedRange.Rows.Count;
            chartRange = xlWorksheet.get_Range("A1", "M" + rangeRows);
            chartPage.SetSourceData(chartRange, value);
            chartPage.ChartType = XlChartType.xlColumnClustered;
            chartPage.ChartStyle = 209;
            Series series = chartPage.SeriesCollection(1) as Series;


            Trendlines trendlines = (Trendlines)series.Trendlines(Type.Missing);
            Trendline line = trendlines.Add(XlTrendlineType.xlLinear);
            line.Name = "Trendlinje";
            line.Select();
            


        }

        private void ExcelPrint(Worksheet xlWorksheet, Worksheet xlWorksheet1, int year)
        {
            int row = 1;
            #region columnwidth
            xlWorksheet.Columns[1].ColumnWidth = 20;
            xlWorksheet1.Columns[1].ColumnWidth = 20;
            xlWorksheet1.Columns[2].ColumnWidth = 20;
            xlWorksheet1.Columns[3].ColumnWidth = 20;
            xlWorksheet1.Columns[4].ColumnWidth = 20;
            xlWorksheet1.Columns[5].ColumnWidth = 20;
            xlWorksheet1.Columns[6].ColumnWidth = 20;
            xlWorksheet1.Columns[7].ColumnWidth = 20;
            xlWorksheet1.Columns[8].ColumnWidth = 20;
            xlWorksheet1.Columns[9].ColumnWidth = 20;
            xlWorksheet1.Columns[10].ColumnWidth = 20;
            xlWorksheet1.Columns[11].ColumnWidth = 20;
            xlWorksheet1.Columns[12].ColumnWidth = 20;
            xlWorksheet1.Columns[13].ColumnWidth = 20;
            #endregion
            int row2 = 2;
            int column = 1;

            #region Named ranges for xlWorksheet1
            xlWorksheet1.Cells[row, 1] = "År";
            xlWorksheet1.Cells[row, 2] = "Januari";
            xlWorksheet1.Cells[row, 3] = "Februari";
            xlWorksheet1.Cells[row, 4] = "Mars";
            xlWorksheet1.Cells[row, 5] = "April";
            xlWorksheet1.Cells[row, 6] = "Maj";
            xlWorksheet1.Cells[row, 7] = "Juni";
            xlWorksheet1.Cells[row, 8] = "Juli";
            xlWorksheet1.Cells[row, 9] = "Augusti";
            xlWorksheet1.Cells[row, 10] = "September";
            xlWorksheet1.Cells[row, 11] = "Oktober";
            xlWorksheet1.Cells[row, 12] = "November";
            xlWorksheet1.Cells[row, 13] = "December";
            #endregion

            #region Named ranges for xlWorksheet
            Range range = xlWorksheet.get_Range("A1", Type.Missing);
            range.Merge();
            range.Name = "Test_Range";
            xlWorksheet.Range["Test_Range"].Value = year.ToString();

            Range range2 = xlWorksheet.get_Range("B1:D1", Type.Missing);
            range2.Merge();
            range2.Name = "Test_Range2";
            xlWorksheet.Range["Test_Range2"].Value = "Januari";

            Range range3 = xlWorksheet.get_Range("E1:G1", Type.Missing);
            range3.Merge();
            range3.Name = "Test_Range3";
            xlWorksheet.Range["Test_Range3"].Value = "Februari";

            Range range4 = xlWorksheet.get_Range("H1:J1", Type.Missing);
            range4.Merge();
            range4.Name = "Test_Range4";
            xlWorksheet.Range["Test_Range4"].Value = "Mars";

            Range range5 = xlWorksheet.get_Range("K1:M1", Type.Missing);
            range5.Merge();
            range5.Name = "Test_Range5";
            xlWorksheet.Range["Test_Range5"].Value = "April";

            Range range6 = xlWorksheet.get_Range("N1:P1", Type.Missing);
            range6.Merge();
            range6.Name = "Test_Range6";
            xlWorksheet.Range["Test_Range6"].Value = "Maj";

            Range range7 = xlWorksheet.get_Range("Q1: S1", Type.Missing);
            range7.Merge();
            range7.Name = "Test_Range7";
            xlWorksheet.Range["Test_Range7"].Value = "Juni";

            Range range8 = xlWorksheet.get_Range("T1:V1", Type.Missing);
            range8.Merge();
            range8.Name = "Test_Range8";
            xlWorksheet.Range["Test_Range8"].Value = "Juli";

            Range range9 = xlWorksheet.get_Range("W1:Y1", Type.Missing);
            range9.Merge();
            range9.Name = "Test_Range9";
            xlWorksheet.Range["Test_Range9"].Value = "Augusti";

            Range range10 = xlWorksheet.get_Range("Z1: AB1", Type.Missing);
            range10.Merge();
            range10.Name = "Test_Range10";
            xlWorksheet.Range["Test_Range10"].Value = "September";

            Range range1 = xlWorksheet.get_Range("AC1:AE1", Type.Missing);
            range1.Merge();
            range1.Name = "Test_Range1";
            xlWorksheet.Range["Test_Range1"].Value = "Oktober";

            Range range11 = xlWorksheet.get_Range("AF1:AH1", Type.Missing);
            range11.Merge();
            range11.Name = "Test_Range11";
            xlWorksheet.Range["Test_Range11"].Value = "November";

            Range range12 = xlWorksheet.get_Range("AI1:AK1", Type.Missing);
            range12.Merge();
            range12.Name = "Test_Range12";
            xlWorksheet.Range["Test_Range12"].Value = "December";

            Range range13 = xlWorksheet.get_Range("AL1:AN1", Type.Missing);
            range13.Merge();
            range13.Name = "Test_Range13";
            xlWorksheet.Range["Test_Range13"].Value = "Året";

            xlWorksheet.Cells[row2, 1] = "Säljare";
            xlWorksheet.Cells[row2, 2] = "Barn";
            xlWorksheet.Cells[row2, 3] = "Vuxen";
            xlWorksheet.Cells[row2, 4] = "Totalt";
            xlWorksheet.Cells[row2, 5] = "Barn";
            xlWorksheet.Cells[row2, 6] = "Vuxen";
            xlWorksheet.Cells[row2, 7] = "Totalt";
            xlWorksheet.Cells[row2, 8] = "Barn";
            xlWorksheet.Cells[row2, 9] = "Vuxen";
            xlWorksheet.Cells[row2, 10] = "Totalt";
            xlWorksheet.Cells[row2, 11] = "Barn";
            xlWorksheet.Cells[row2, 12] = "Vuxen";
            xlWorksheet.Cells[row2, 13] = "Totalt";
            xlWorksheet.Cells[row2, 14] = "Barn";
            xlWorksheet.Cells[row2, 15] = "Vuxen";
            xlWorksheet.Cells[row2, 16] = "Totalt";
            xlWorksheet.Cells[row2, 17] = "Barn";
            xlWorksheet.Cells[row2, 18] = "Vuxen";
            xlWorksheet.Cells[row2, 19] = "Totalt";
            xlWorksheet.Cells[row2, 20] = "Barn";
            xlWorksheet.Cells[row2, 21] = "Vuxen";
            xlWorksheet.Cells[row2, 22] = "Totalt";
            xlWorksheet.Cells[row2, 23] = "Barn";
            xlWorksheet.Cells[row2, 24] = "Vuxen";
            xlWorksheet.Cells[row2, 25] = "Totalt";
            xlWorksheet.Cells[row2, 26] = "Barn";
            xlWorksheet.Cells[row2, 27] = "Vuxen";
            xlWorksheet.Cells[row2, 28] = "Totalt";
            xlWorksheet.Cells[row2, 29] = "Barn";
            xlWorksheet.Cells[row2, 30] = "Vuxen";
            xlWorksheet.Cells[row2, 31] = "Totalt";
            xlWorksheet.Cells[row2, 32] = "Barn";
            xlWorksheet.Cells[row2, 33] = "Vuxen";
            xlWorksheet.Cells[row2, 34] = "Totalt";
            xlWorksheet.Cells[row2, 35] = "Barn";
            xlWorksheet.Cells[row2, 36] = "Vuxen";
            xlWorksheet.Cells[row2, 37] = "Totalt";
            xlWorksheet.Cells[row2, 38] = "Totalt";
            xlWorksheet.Cells[row2, 39] = "Medel";
            xlWorksheet.Cells[row2, 40] = CountAverageForAllSm(year);
            #endregion
            WriteDataToExcel(xlWorksheet, row2, column, year);
            WriteDataToEx1(xlWorksheet1, row, column, year, Instance.SalesMen);
        }
        /// <summary>
        /// worksheet for selected salesmen
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="year"></param>
        /// <param name="sm"></param>
        private void WriteDataToEx1(Worksheet xlWorksheet, int row, int column, int year, SalesMen sm)
        {
            row++;
            xlWorksheet.Cells[row, 1] = sm.Firstname + " " + sm.Lastname;
            xlWorksheet.Cells[row, 2] = CountMonthTotalSM(sm, 1, year);
            xlWorksheet.Cells[row, 3] = CountMonthTotalSM(sm, 2, year);
            xlWorksheet.Cells[row, 4] = CountMonthTotalSM(sm, 3, year);
            xlWorksheet.Cells[row, 5] = CountMonthTotalSM(sm, 4, year);
            xlWorksheet.Cells[row, 6] = CountMonthTotalSM(sm, 5, year);
            xlWorksheet.Cells[row, 7] = CountMonthTotalSM(sm, 6, year);
            xlWorksheet.Cells[row, 8] = CountMonthTotalSM(sm, 7, year);
            xlWorksheet.Cells[row, 9] = CountMonthTotalSM(sm, 8, year);
            xlWorksheet.Cells[row, 10] = CountMonthTotalSM(sm, 9, year);
            xlWorksheet.Cells[row, 11] = CountMonthTotalSM(sm, 10, year);
            xlWorksheet.Cells[row, 12] = CountMonthTotalSM(sm, 11, year);
            xlWorksheet.Cells[row, 13] = CountMonthTotalSM(sm, 12, year);
        }
        /// <summary>
        /// Worksheet for all salesmens trendstatistics
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="year"></param>
        private void WriteDataToExcel(Worksheet xlWorksheet, int row, int column, int year)
        {
            foreach (var s in SalesMens)
            {
                row++;
                xlWorksheet.Cells[row, 1] = s.Firstname + " " + s.Lastname;
                xlWorksheet.Cells[row, 2] = CreateSMAVC(s, 1, year);
                xlWorksheet.Cells[row, 5] = CreateSMAVC(s, 2, year);
                xlWorksheet.Cells[row, 8] = CreateSMAVC(s, 3, year);
                xlWorksheet.Cells[row, 11] = CreateSMAVC(s, 4, year);
                xlWorksheet.Cells[row, 14] = CreateSMAVC(s, 5, year);
                xlWorksheet.Cells[row, 17] = CreateSMAVC(s, 6, year);
                xlWorksheet.Cells[row, 20] = CreateSMAVC(s, 7, year);
                xlWorksheet.Cells[row, 23] = CreateSMAVC(s, 8, year);
                xlWorksheet.Cells[row, 26] = CreateSMAVC(s, 9, year);
                xlWorksheet.Cells[row, 29] = CreateSMAVC(s, 10, year);
                xlWorksheet.Cells[row, 32] = CreateSMAVC(s, 11, year);
                xlWorksheet.Cells[row, 35] = CreateSMAVC(s, 12, year);

                xlWorksheet.Cells[row, 3] = CreateSMAVA(s, 1, year);
                xlWorksheet.Cells[row, 6] = CreateSMAVA(s, 2, year);
                xlWorksheet.Cells[row, 9] = CreateSMAVA(s, 3, year);
                xlWorksheet.Cells[row, 12] = CreateSMAVA(s, 4, year);
                xlWorksheet.Cells[row, 15] = CreateSMAVA(s, 5, year);
                xlWorksheet.Cells[row, 18] = CreateSMAVA(s, 6, year);
                xlWorksheet.Cells[row, 21] = CreateSMAVA(s, 7, year);
                xlWorksheet.Cells[row, 24] = CreateSMAVA(s, 8, year);
                xlWorksheet.Cells[row, 27] = CreateSMAVA(s, 9, year);
                xlWorksheet.Cells[row, 30] = CreateSMAVA(s, 10, year);
                xlWorksheet.Cells[row, 33] = CreateSMAVA(s, 11, year);
                xlWorksheet.Cells[row, 36] = CreateSMAVA(s, 12, year);

                xlWorksheet.Cells[row, 4] = CountMonthTotalSM(s, 1, year);
                xlWorksheet.Cells[row, 7] = CountMonthTotalSM(s, 2, year);
                xlWorksheet.Cells[row, 10] = CountMonthTotalSM(s, 3, year);
                xlWorksheet.Cells[row, 13] = CountMonthTotalSM(s, 4, year);
                xlWorksheet.Cells[row, 16] = CountMonthTotalSM(s, 5, year);
                xlWorksheet.Cells[row, 19] = CountMonthTotalSM(s, 6, year);
                xlWorksheet.Cells[row, 22] = CountMonthTotalSM(s, 7, year);
                xlWorksheet.Cells[row, 25] = CountMonthTotalSM(s, 8, year);
                xlWorksheet.Cells[row, 28] = CountMonthTotalSM(s, 9, year);
                xlWorksheet.Cells[row, 31] = CountMonthTotalSM(s, 10, year);
                xlWorksheet.Cells[row, 34] = CountMonthTotalSM(s, 11, year);
                xlWorksheet.Cells[row, 37] = CountMonthTotalSM(s, 12, year);

                xlWorksheet.Cells[row, 38] = CountTotalSM(s, year);
                xlWorksheet.Cells[row, 39] = CountAverage(s, year);
                xlWorksheet.Cells[row, 40] = CountDifference(s, year);
            }
        }


        #endregion

        #region properties
        public List<int?> Years { get; set; }
        public List<int?> Months { get; set; }
        private int? _year;
        public int? Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
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

        #endregion

        #region methods for excel int/doubles. 
        /// <summary>
        /// Get Average for all salesmens 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int CountAverageForAllSm(int year)
        {
            int sum = 0;
            double sum1 = 0;
            foreach (var sm in Context.SMController.GetAllSalesMen())
            {
                foreach (var i in sm.Insurances)
                {
                    if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.SAI != null)
                    {
                        sum1 += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            sum = Convert.ToInt32(sum1);
            return sum;
        }
        /// <summary>
        /// Get Average for all salesmens 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int CountDifference(SalesMen sm, int year)
        {
            double sum = CountAverage(sm, year) - CountAverageForAllSm(year);
            int sum1 = 0;
            return sum1 = Convert.ToInt32(sum);
        }


        /// <summary>
        /// method to get average for all months for salesmen
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private double CountAverage(SalesMen sm, int year)
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
            var arr = new double[] { a, b, c, d, e, f, g, h, i, j, k, l };
            double avg = Queryable.Average(arr.AsQueryable());
            return avg;
        }


        /// <summary>
        /// All acvisi
        /// </summary>
        /// <param name="salesMens"></param>
        /// <returns></returns>
        private int CountTotalSM(SalesMen sm, int year)
        {
            int sum1 = 0;
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year)
                {
                    if (i.SAI.SAID.Equals(2) || i.SAI.SAID.Equals(1))
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            sum1 = Convert.ToInt32(sum);
            return sum1;
        }
        /// <summary>
        /// get both child and adults per month per salesmen. 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="Month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private double CountMonthTotalSM(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year)
                {
                    if (i.SAI.SAID.Equals(2) || i.SAI.SAID.Equals(1))
                    {
                        if (i.PayMonth == Month)
                        {
                            sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                        }
                    }
                }
            }

            return sum;
        }
        /// <summary>
        /// Create month ackvalue for child per salesmen. 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="Month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private double CreateSMAVC(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.SAI.SAID.Equals(1))
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Create month ackvalue for adult per salesmen. 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="Month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private double CreateSMAVA(SalesMen sm, int Month, int year)
        {
            double sum = 0;
            foreach (Insurance i in sm.Insurances)
            {
                if (i.InsuranceStatus == Status.Tecknad && i.PayYear == year && i.SAI.SAID.Equals(2))
                {
                    if (i.PayMonth == Month)
                    {
                        sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                    }
                }
            }
            return sum;
        }
        #endregion 
    }
}
