using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
    public class GetandexportSalesstatisticsViewModel : BaseViewModel
    {
        public static readonly GetandexportSalesstatisticsViewModel Instance = new GetandexportSalesstatisticsViewModel();
        private GetandexportSalesstatisticsViewModel()
        {

        }

        private ICommand GetStatics_Btn;

        public ICommand GetandexportSalesstatisticsViewModel_Btn
        {
            get => GetStatics_Btn ?? (GetStatics_Btn = new RelayCommand(x => { GetandexportSalesstatistics(); CanCreate(); }));
        }
    public bool CanCreate() => true;

    public static void GetandexportSalesstatistics()
    {

    }

    }
}
