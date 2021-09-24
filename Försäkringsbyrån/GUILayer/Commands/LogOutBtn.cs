using GUILayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands
{
    /// <summary>
    /// command for logout button
    /// </summary>
    public class LogOutBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {
            MainViewModel.Instance.DisplayLogInView();
        }
    }
}
