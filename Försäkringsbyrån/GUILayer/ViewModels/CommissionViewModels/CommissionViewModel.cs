using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.CommissionViewModels
{
    /// <summary>
    /// Viewmodel for commissionview. 
    /// </summary>
    public class CommissionViewModel: BaseViewModel
    {
        public static readonly CommissionViewModel Instance = new CommissionViewModel();
        private CommissionViewModel()
        {
        }
        private ICommand getCommission_Btn;
        public ICommand GetCommssion_Btn => getCommission_Btn ?? (getCommission_Btn = new RelayCommand(x => { GetCommssion(); }));
        public static void GetCommssion()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = CommissionOverviewViewModel.Instance;
        }
    }
}
