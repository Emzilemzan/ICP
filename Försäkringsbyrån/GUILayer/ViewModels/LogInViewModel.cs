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

        public ICommand LogInBtn => new RelayCommand(LogIn, CanLogIn);

        public bool CanLogIn(object value) => true;

        private void LogIn(object value)
        {
            bool result = Context.UAController.ValidateEmployee(Username, Password);
            if (result == false)
            {
                MessageBox.Show("Användarnamn eller lösenord stämmer inte", "Fel uppgifter", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MainViewModel.Instance.DisplayHomeView();
                Instance.Username = string.Empty;
                Instance.Password = string.Empty;
            }
        }

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
    }
}
