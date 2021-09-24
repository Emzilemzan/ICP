using GUILayer.ViewModels;
using GUILayer.ViewModels.CommissionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.CommissionCommands
{
    /// <summary>
    /// One click and commission menuchoices shows, click again and welcome page shows. 
    /// </summary>
    public class CommissionBtn : BaseCommand
    { 
      public override bool CanExecute(object parameter = null) => true;

      public override void Execute(object parameter)
      {
            if (MainViewModel.Instance.CurrentTool != "Commission")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = CommissionViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "Commission";
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
