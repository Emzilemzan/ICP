using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.CommissionViewModels
{
    public class GetCommissionViewModel : BaseViewModel
    {
        public static readonly GetCommissionViewModel Instance = new GetCommissionViewModel();


        private GetCommissionViewModel()
        {
            Months = GetMonths();
            SalesMens = GetAllSM();
            Salesman = SalesMens[0];
        }

        public ObservableCollection<SalesMen> GetAllSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach (var e in Context.SMController.GetAllSalesMen())
            {
                x?.Add(e);
            }
            SalesMens = x;
            return SalesMens;
        }
        


        #region Commands
        private ICommand _getBtn;
        public ICommand GetCommissionOverView_Btn
        {
            get => _getBtn ?? (_getBtn = new RelayCommand(x => { GetCommissionOverView(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        private void GetCommissionOverView()
        {
        }

        //För att visa månader i combobox. 
        public List<int> Months { get; set; }
        public List<int> GetMonths()
        { 
            Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            return Months;
    
        }
        #endregion

        #region Properties

        public ObservableCollection<SalesMen> SalesMens { get; set; }

        //Vilken säljare 
        private SalesMen _salesMan;
        public SalesMen Salesman
        {
            get => _salesMan;
            set
            {
                _salesMan = value;

                OnPropertyChanged("Salesman");
            }
        }

        //Vilken månad 
        private int _month;
        public int Month
        {
            get => _month;
            set
            {
                _month = value;

                OnPropertyChanged("Month");
            }
        }
        #endregion
    }
}
