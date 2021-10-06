using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class ApplicationOverviewViewModels : BaseViewModel
    {
        public static readonly ApplicationOverviewViewModels Instance = new ApplicationOverviewViewModels();

        private ApplicationOverviewViewModels()
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