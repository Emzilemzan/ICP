using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels
{
    /// <summary>
    /// LogInViewModel for the loginview. 
    /// </summary>
    public class LogInViewModel : BaseViewModel
    {
        public static readonly LogInViewModel Instance = new LogInViewModel();

        public LogInBtn LogIn { get; }

        private LogInViewModel()
        {
            LogIn = new LogInBtn();
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                LogIn.RaiseCanExecuteChanged();
                OnPropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                LogIn.RaiseCanExecuteChanged();
                OnPropertyChanged("Password");
            }
        }
    }
}
