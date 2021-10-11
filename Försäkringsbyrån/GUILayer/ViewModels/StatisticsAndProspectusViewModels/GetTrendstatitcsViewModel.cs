using GUILayer.Commands;
using System;
using System.Collections.Generic;
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

        }
        private ICommand trendstatic_btn;

        public ICommand GetTrendstatitcsViewModel_btn
        {
            get => trendstatic_btn ?? (trendstatic_btn = new RelayCommand(x => { GetTrendstatitcs(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public static void GetTrendstatitcs()
        {

        }
    }
}
