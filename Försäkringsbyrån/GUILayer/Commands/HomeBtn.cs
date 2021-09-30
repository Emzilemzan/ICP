using GUILayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands
{
    /// <summary>
    /// command for what happends when "insurance consulting program" clicks on. 
    /// </summary>
    public class HomeBtn : BaseCommand
    {
        public override bool CanExecute(object parameter) => MainViewModel.Instance.Context.CurrentEmployee != null;
        public override void Execute(object parameter)
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.DisplayHomeView(); 
        }
    }
}
