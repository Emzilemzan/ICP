using GUILayer.Commands;
using GUILayer.Commands.BasicDataCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    /// <summary>
    /// ViewModel for BasicDataView. 
    /// </summary>
    public class BasicDataViewModel: BaseViewModel
    {
        public static readonly BasicDataViewModel Instance = new BasicDataViewModel();

        //public BaseAmountTableBtn AmountTable_Btn { get; }
        private BasicDataViewModel()
        {
            //AmountTable_Btn = new BaseAmountTableBtn();
        }

        private ICommand amountTable_Btn;
        public ICommand AmountTable_Btn
        {
            get => amountTable_Btn ?? (amountTable_Btn = new RelayCommand(x => { AddBaseAmountTableHome(); CanCreate(); }));
        }

        public bool CanCreate() => true;
        public static void AddBaseAmountTableHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = BaseAmountTableViewModel.Instance;
        }
    }
}
