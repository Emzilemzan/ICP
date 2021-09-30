using GUILayer.ViewModels;
using GUILayer.ViewModels.BasicDataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.BasicDataCommands
{
    public class BaseAmountTableBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = ""; 
            MainViewModel.Instance.SelectedViewModel = BaseAmountTableViewModel.Instance;

        }
    }
}
