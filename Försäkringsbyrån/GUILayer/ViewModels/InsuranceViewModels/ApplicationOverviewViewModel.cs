
using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
  public class ApplicationOverviewViewModel: BaseViewModel
    {
        public static readonly ApplicationOverviewViewModel Instance  = new ApplicationOverviewViewModel();


        private ApplicationOverviewViewModel()
        {
        }



     private ICommand _addBtn;
      public ICommand SearchApplicationBtn
       {
           get => _addBtn ?? (_addBtn = new RelayCommand(x => { SearchApplication(); CanCreate(); }));
       }
        public bool CanCreate() => true;

        public static void SearchApplication()
        {

        }


    }
}
