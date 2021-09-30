using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels
{
    //..
    /// <summary>
    /// LogInViewModel for the loginview. 
    /// </summary>
    public class LogInViewModel : BaseViewModel
    {
        public static readonly LogInViewModel Instance = new LogInViewModel();

        private LogInViewModel()
        {
        }

        #region command

        private ICommand _logInBtn;

        public ICommand LogInBtn
        {
            get => _logInBtn ?? (_logInBtn = new RelayCommand(x => { LogIn(); CanCommand(); }));
        }

        public void LogIn()
        {
            if (!string.IsNullOrWhiteSpace(LogInViewModel.Instance.Username) && !string.IsNullOrWhiteSpace(LogInViewModel.Instance.Password)
                && LogInViewModel.Instance.Context.EController.ValidateEmployee(LogInViewModel.Instance.Username, LogInViewModel.Instance.Password))
                MainViewModel.Instance.DisplayHomeView();
            else
                MessageBox.Show("Användarnamn eller lösenord är fel");


        }
        public bool CanCommand() => true;

        #endregion

        #region properties
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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
                OnPropertyChanged("Password");
            }
        }

        #endregion
    }
}
