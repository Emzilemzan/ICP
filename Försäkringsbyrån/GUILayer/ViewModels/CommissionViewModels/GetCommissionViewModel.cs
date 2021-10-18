using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.CommissionViewModels
{
    public class GetCommissionViewModel : BaseViewModel
    {
        public static readonly GetCommissionViewModel Instance = new GetCommissionViewModel();

        public GetCommissionViewModel()
        {
            Months = new List<string>() {"Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "November", "December"};
            SalesMens = UpdateSM();
        }

        #region Commands
        private ICommand _getBtn;
        public ICommand GetCommissionOverView_Btn
        {
            get => _getBtn ?? (_getBtn = new RelayCommand(x => { GetCommissionOverView(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public static void GetCommissionOverView()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = CommissionOverviewViewModel.Instance;
        }

        //För att visa månader i combobox. 
        public List<string> Months { get; set; }
        
        #endregion

        //Get all salesmen. 
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



        #region Properties
        public ObservableCollection<SalesMen> SalesMens { get; set; }

        private SalesMen _agentNo;

        public SalesMen AgentNo
        {
            get => _agentNo;
            set 
            { 
                _agentNo = value;
                OnPropertyChanged("AgentNo");
            }
        }

        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

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
        #endregion

       
    }
}
