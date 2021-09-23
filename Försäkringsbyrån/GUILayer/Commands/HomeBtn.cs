using GUILayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands
{
    public class HomeBtn : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;
        // {MainViewModel.Instance.Context.CurrentEmployee != null;}
        public override void Execute(object parameter) => MainViewModel.Instance.DisplayHomeView();
    }
}
