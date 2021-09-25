using GUILayer.ViewModels;
using GUILayer.ViewModels.StatisticsAndProspectusViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.StatisticsAndProspectusCommands
{
    /// <summary>
    /// One click and Statististics&Prospect menuchoices shows, click again and welcome page shows. 
    /// </summary>
    public class StatisticsAndProspectusBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.CurrentTool != "StatisticsAndProspect")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = StatisticsAndProspectusViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "StatisticsAndProspect";
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
