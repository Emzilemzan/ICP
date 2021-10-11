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
            get => amountTable_Btn ?? (amountTable_Btn = new RelayCommand(x => { BaseAmountTable(); CanCreate(); }));
        }

        public bool CanCreate() => true;
        public static void BaseAmountTable()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = BaseAmountTableViewModel.Instance;
        }

        private ICommand ackvaluevariableTable_Btn;
        public ICommand AckvaluevariableTable_Btn
        {
            get => ackvaluevariableTable_Btn ?? (ackvaluevariableTable_Btn = new RelayCommand(x => { AckValueVariableTable(); CanCreate(); }));
        }

        public static void AckValueVariableTable()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = AckValueVariableTableViewModel.Instance;
        }

        private ICommand amountOption_Btn;
        public ICommand AmountOption_Btn
        {
            get => amountOption_Btn ?? (amountOption_Btn = new RelayCommand(x => { BaseAmountOption(); CanCreate(); }));
        }

        public static void BaseAmountOption()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = BaseAmountOptionViewModel.Instance;
        }

        private ICommand vPay_Btn;
        public ICommand VPayBtn
        {
            get => vPay_Btn ?? (vPay_Btn = new RelayCommand(x => { VPay(); CanCreate(); }));
        }

        public static void VPay()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = VacationPayViewModel.Instance;
        }

        private ICommand commissionShare_Btn;
        public ICommand CommissionShare_Btn
        {
            get => commissionShare_Btn ?? (commissionShare_Btn = new RelayCommand(x => { CommissionShare(); CanCreate(); }));
        }

        public static void CommissionShare()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = PermissionValueTableViewModel.Instance;
        }
    }
}
