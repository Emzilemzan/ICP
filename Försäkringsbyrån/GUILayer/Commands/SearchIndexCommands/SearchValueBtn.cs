using GUILayer.ViewModels;
using GUILayer.ViewModels.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.SearchIndexCommands
{
    /// <summary>
    /// One click and searchvalue menuchoices shows, click again and welcome page shows. 
    /// </summary>
    public class SearchValueBtn: BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.CurrentTool != "Search")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = SearchValueViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "Search";
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
