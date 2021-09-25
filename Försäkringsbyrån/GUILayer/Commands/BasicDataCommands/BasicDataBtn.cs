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
    /// <summary>
    /// One click and basicdata menuchoices shows, click again and welcome page shows. 
    /// </summary>
    public class BasicDataBtn: BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.CurrentTool != "BasicData")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = BasicDataViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "BasicData";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }
    }
}
