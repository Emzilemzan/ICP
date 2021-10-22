using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
   public class GetTrendstatitcsViewModel : BaseViewModel
    {
        public static readonly GetTrendstatitcsViewModel Instance = new GetTrendstatitcsViewModel();

        public GetTrendstatitcsViewModel()
        {
            SalesMens = UpdateSM();
            Years = GetYears();
        }
        #region methods for view. 
        public List<int?> GetYears()
        {
            List<int?> Years1 = new List<int?>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if(i.PayYear != null)
                {
                    Years1?.Add(i.PayYear);
                }
            }
            
            return Years = Years1.Union(Years1).ToList();
            //return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }

        //Get all salesmen. 
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach(var i in Context.IController.GetAllInsurances())
            {
                if(i.SAI!= null && i.InsuranceStatus == Status.Tecknad)
                {
                    x?.Add(i.AgentNo);
                }
            }
            SalesMens = x;
            return SalesMens;
        }

        #endregion

        #region command export
        private ICommand trendstatic_btn;

        public ICommand ExportBtn => trendstatic_btn ?? (trendstatic_btn = new RelayCommand(x => { GetTrendstatitcs(); }));
        private void GetTrendstatitcs()
        {

        }
        #endregion

        #region properties
        public List<int?> Years { get; set; }
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
    }
}
