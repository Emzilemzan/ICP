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


        private ICommand permissionValueTable_Btn;
        public ICommand PermissionValueTable_Btn
        {
            get => permissionValueTable_Btn ?? (permissionValueTable_Btn = new RelayCommand(x => { AddPermissionValue(); CanCreate(); }));
        }

        public static void AddPermissionValue()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = ""; 
            MainViewModel.Instance.SelectedViewModel = PermissionValueTableViewModel.Instance;
        }



    }
}
