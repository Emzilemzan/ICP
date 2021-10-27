using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GUILayer.ViewModels.BasicDataViewModels;
using GUILayer.ViewModels.CommissionViewModels;
using GUILayer.ViewModels.EmployeeManagementViewModels;
using GUILayer.ViewModels.InsuranceViewModels;
using GUILayer.ViewModels.SearchViewModels;
using GUILayer.ViewModels.StatisticsAndProspectusViewModels;
using Models.Models;

namespace GUILayer.ViewModels
{
    /// <summary>
    /// mainviewmodel for mainview, handels which view to see. 
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public static readonly MainViewModel Instance = new MainViewModel();

        private MainViewModel()
        {
            _selectedViewModel = LogInViewModel.Instance;
            _toolsVisibility = Visibility.Hidden;
            Context.GenerateData();
        }
    
        #region commands
        public ICommand HomeBtn => new RelayCommand(Home, CanCommandHome);

        public bool CanCommandHome(object value) => Context.CurrentUser != null;

        private void Home(object value)
        {
            Instance.ToolsVisibility = Visibility.Collapsed;
            Instance.DisplayHomeView();
        }

        public ICommand BasicDataBtn => new RelayCommand(BasicData, CanCommandBasicData);

        public bool CanCommandBasicData(object value) => Context.CurrentUser != null && Context.CurrentUser.BasicData == true;

        private void BasicData(object value)
        {
            if (Instance.CurrentTool != "BasicData")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = BasicDataViewModel.Instance;
                Instance.CurrentTool = "BasicData";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }
        public ICommand CommissionBtn => new RelayCommand(Commission, CanCommandC);

        public bool CanCommandC(object value)=> Context.CurrentUser != null && Context.CurrentUser.Commission == true;

        private void Commission(object value)
        {
            if (Instance.CurrentTool != "Commission")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = CommissionViewModel.Instance;
                Instance.CurrentTool = "Commission";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }

        }
        public ICommand EMBtn => new RelayCommand(EmployeeManagement, CanCommandEM);

        public bool CanCommandEM(object value) => Context.CurrentUser != null && Context.CurrentUser.EmployeeManagement == true;

        private void EmployeeManagement(object value)
        {
            
            if (Instance.CurrentTool != "EmployeeManagement")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = EmployeeManagementViewModel.Instance;
                Instance.CurrentTool = "EmployeeManagement";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }
        public ICommand InsuranceBtn => new RelayCommand(Insurance, CanCommandIB);

        public bool CanCommandIB(object value) => Context.CurrentUser != null && Context.CurrentUser.Insurances == true;


        private void Insurance(object value)
        {
            if (Instance.CurrentTool != "Insurance")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = InsuranceViewModel.Instance;
                Instance.CurrentTool = "Insurance";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }

        public ICommand LogOutBtn => new RelayCommand(LogOut, CanLogOut);

        public bool CanLogOut(object value) => Context.CurrentUser != null;

        private void LogOut(object value)
        {
            Context.CurrentUser = null;
            DisplayLogInView();
        }

        public ICommand SearchBtn => new RelayCommand(SearchIndex, CanCommandSearch);
        public bool CanCommandSearch(object value) => Context.CurrentUser != null && Context.CurrentUser.Search;

        private void SearchIndex(object value)
        {
            if (Instance.CurrentTool != "Search")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = SearchValueViewModel.Instance;
                Instance.CurrentTool = "Search";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
            
        }

        public ICommand SapBtn => new RelayCommand(Sap, CanCommandSap);

        public bool CanCommandSap(object value) => Context.CurrentUser != null && Context.CurrentUser.StatisticsAndProspects == true;

        private void Sap(object value)
        {
            if (Instance.CurrentTool != "StatisticsAndProspect")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = StatisticsAndProspectusViewModel.Instance;
                Instance.CurrentTool = "StatisticsAndProspect";
                Instance.SelectedViewModel = null;
                ResetAllViews();
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }

        #endregion

        public void DisplayLogInView()
        {
            SelectedViewModel = LogInViewModel.Instance;
            ToolsVisibility = Visibility.Collapsed;
        }

        public void DisplayHomeView()
        {
            SelectedViewModel = HomeViewModel.Instance;
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

        private void ResetAllViews()
        {
            RegisterPersonApplicationViewModel.Instance.EmptyAllChoices();
            AddEmployeeViewModel.Instance.EmptyAllChoices();
            AddUserAccessViewModel.Instance.EmptyAllChoices();
            RegisterApplicationViewModel.Instance.EmptyAllChoices();
            RegisterLifeApplicationViewModel.Instance.EmptyAllChoices();
            RegisterOPIPApplicationViewModel.Instance.EmptyAllChoices();
            RegisterOPICApplicationViewModel.Instance.EmptyAllChoices();
            AckValueVariableTableViewModel.Instance.EmptyAllChoices();
            BaseAmountTableViewModel.Instance.EmptyAllChoices();
            BaseAmountOptionViewModel.Instance.EmptyAllChoices();
            PermissionValueTableViewModel.Instance.EmptyAllChoices();
            SignedInsuranceViewModel.Instance.UpdateAC();
            VacationPayViewModel.Instance.EmptyAllChoices();
            SignedInsuranceViewModel.Instance.MakeSearchWordEmpty();
            GetandexportCustomerLeadsViewModel.Instance.Update();
            SAOverviewViewModel.Instance.UpdateGridToDb();
            GetTrendstatitcsViewModel.Instance.Update();
            CommissionOverviewViewModel.Instance.EmptyAllChoices1();
            ManageUserAccessViewModel.Instance.UpdateGridToDb();
            HandleEmployeeViewModel.Instance.UpdateGridToDb();
            SearchInsuranceTakerPersonViewModel.Instance.UpdateGridToDb();
            SearchInsuranceTakerCompanyViewModel.Instance.UpdateGridToDb();
            LifeOverviewViewModel.Instance.UpdateGridToDb();
            ApplicationOverviewViewModels.Instance.UpdateGridToDb();
            OPICApplicationOverviewViewModel.Instance.UpdateGridToDb();
            OPIPApplicationOverviewViewModel.Instance.UpdateGridToDb();
        }
    }
}

