using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static readonly MainViewModel Instance = new MainViewModel();

        public HomeBtn Home_Btn { get; }

        private MainViewModel()
        {
            _selectedViewModel = LogInViewModel.Instance;
            _toolsVisibility = Visibility.Hidden;
            Home_Btn = new HomeBtn();

        }

        public void DisplayLogInView()
        {
            SelectedViewModel = LogInViewModel.Instance;
            ToolsVisibility = Visibility.Collapsed;
            UpdateBtns();
        }

        public void DisplayHomeView()
        {
            SelectedViewModel = HomeViewModel.Instance;
            //HomeViewModel.Instance.UpdateFlow(); */
            UpdateBtns();
        }

        private void UpdateBtns()
        {
            Home_Btn.RaiseCanExecuteChanged();
        }

        public string CurrentTool { get; set; } = "";

        private BaseViewModel _tools;
        public BaseViewModel Tools
        {
            get => _tools;
            set
            {
                _tools = value;
                OnPropertyChanged("Tools");
            }
        }

        private Visibility _toolsVisibility;
        public Visibility ToolsVisibility
        {
            get => _toolsVisibility;
            set
            {
                _toolsVisibility = value;
                OnPropertyChanged("ToolsVisibility");
            }
        }

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;

                OnPropertyChanged("SelectedViewModel");
            }
        }
    }
}

