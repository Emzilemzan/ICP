using GUILayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands
{
    public class LogInBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) =>
           !string.IsNullOrWhiteSpace(LogInViewModel.Instance.Username) && !string.IsNullOrWhiteSpace(LogInViewModel.Instance.Password);
        /*
        public override void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(LogInViewModel.Instance.Username) && !string.IsNullOrWhiteSpace(LogInViewModel.Instance.Password)
                && LogInViewModel.Instance.Context.EController.ValidateEmployee(LogInViewModel.Instance.Username, LogInViewModel.Instance.Password))
                
                MainViewModel.Instance.DisplayHomeView();

        }
        */
    }
}
