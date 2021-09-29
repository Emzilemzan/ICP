using GUILayer.Commands;
using GUILayer.Commands.CommissionCommands;
using GUILayer.Commands.InsuranceCommands;
using GUILayer.Commands.EmployeeManagementCommands;
using GUILayer.Commands.SearchIndexCommands;
using GUILayer.Commands.StatisticsAndProspectusCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GUILayer.Commands.BasicDataCommands;
using System.Windows.Input;

namespace GUILayer.ViewModels
{
    /// <summary>
    /// mainviewmodel for mainview, handels which view to see. 
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public static readonly MainViewModel Instance = new MainViewModel();

        public BasicDataBtn BasicData_Btn { get; }
        public CommissionBtn Commission_Btn { get; }
        public InsuranceBtn Insurance_Btn { get; }
        public LogOutBtn LogOut_Btn { get; }
        public EmployeeManagementBtn EM_Btn { get; }
        public SearchValueBtn SearchIndex_Btn { get; }
        public StatisticsAndProspectusBtn SAP_Btn { get; }

        private MainViewModel()
        {
            _selectedViewModel = LogInViewModel.Instance;
            _toolsVisibility = Visibility.Hidden;
          
            BasicData_Btn = new BasicDataBtn();
            Commission_Btn = new CommissionBtn();
            Insurance_Btn = new InsuranceBtn();
            LogOut_Btn = new LogOutBtn();
            EM_Btn = new EmployeeManagementBtn();
            SearchIndex_Btn = new SearchValueBtn();
            SAP_Btn = new StatisticsAndProspectusBtn();

        }

        private ICommand _homeBtn;
        public ICommand HomeBtn
        {
            get => _homeBtn ?? (_homeBtn = new RelayCommand(x => { Home();  })); 
        }

        private static void Home()
        {

            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.DisplayHomeView();
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
            UpdateBtns();
        }

        private void UpdateBtns()
        {
            BasicData_Btn.RaiseCanExecuteChanged();
            Commission_Btn.RaiseCanExecuteChanged();
            Insurance_Btn.RaiseCanExecuteChanged();
            LogOut_Btn.RaiseCanExecuteChanged();
            EM_Btn.RaiseCanExecuteChanged();
            SearchIndex_Btn.RaiseCanExecuteChanged();
            SAP_Btn.RaiseCanExecuteChanged();
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

