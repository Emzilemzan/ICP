using GUILayer.Commands;
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

        private BasicDataViewModel()
        {
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

        private ICommand ackvaluevariableTable_Btn;
        public ICommand AckvaluevariableTable_Btn
        {
            get => ackvaluevariableTable_Btn ?? (ackvaluevariableTable_Btn = new RelayCommand(x => { AddAckValueVariableTable(); CanCreate(); }));
        }

        public static void AddAckValueVariableTable()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = AckValueVariableTableViewModel.Instance;
        }

        private ICommand amountOption_Btn;
        public ICommand AmountOption_Btn
        {
            get => amountOption_Btn ?? (amountOption_Btn = new RelayCommand(x => { AddBaseAmountOption(); CanCreate(); }));
        }

        public static void AddBaseAmountOption()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = BaseAmountOptionViewModel.Instance;
        }
    }
}
