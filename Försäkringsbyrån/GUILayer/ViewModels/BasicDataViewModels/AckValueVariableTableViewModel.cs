using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class AckValueVariableTableViewModel: BaseViewModel
    {
        public static readonly AckValueVariableTableViewModel Instance = new AckValueVariableTableViewModel();

        private AckValueVariableTableViewModel()
        {

        }

        private ICommand _addBtn;
        public ICommand AddAckValueVariableTable_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddAckValueVariableTable(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public static void AddAckValueVariableTable()
        {
           
        }

        public static void RemoveAckValue()
        {
           

        }
    }
}
