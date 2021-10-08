using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
   public class PermissionValueTableViewModel: BaseViewModel
    {
        public static readonly PermissionValueTableViewModel Instance = new PermissionValueTableViewModel();
        
        private PermissionValueTableViewModel()
        { 
            
        }

        private ICommand _addBtn;

        public ICommand AddPermissionValue_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddPermissionValue(); CanCreate(); }));
        }
        public bool CanCreate() => true;

        public static void AddPermissionValue()
        {

        }

        public static void RemovePermissionValue()
        {


        }

    }
}
