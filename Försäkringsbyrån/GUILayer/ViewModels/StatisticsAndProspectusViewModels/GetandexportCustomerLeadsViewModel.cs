using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
   public class GetandexportCustomerLeadsViewModel: BaseViewModel
    {
        public static readonly GetandexportCustomerLeadsViewModel Instance = new GetandexportCustomerLeadsViewModel();

        public GetandexportCustomerLeadsViewModel()
        {

        }
        private ICommand Searchleads_Btn;
        public ICommand GetandexportCustomerLeadsViewModel_Btn
        {
            get => Searchleads_Btn ?? (Searchleads_Btn = new RelayCommand(x => { GetandexportCustomerLeads(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public static void GetandexportCustomerLeads()
        {

        }

    }


}
