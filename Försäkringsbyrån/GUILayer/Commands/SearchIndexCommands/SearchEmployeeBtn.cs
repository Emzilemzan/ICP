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
    /// command for search employee
    /// </summary>
    public class SearchEmployeeBtn: BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {

            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = SearchEmployeeViewModel.Instance;
        }
    }
}
