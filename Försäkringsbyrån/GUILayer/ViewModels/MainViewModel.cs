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
            CreateOptionalTypes();
            CreateSAInsurances();
            CreateOtherPersonInsurance();
            CreateCompanyInsurance();
            CreateLifeInsurance();

        }
        #region commands
        private ICommand _homeBtn;
        public ICommand HomeBtn
        {
            get => _homeBtn ?? (_homeBtn = new RelayCommand(x => { Home(); CanCommandHome(); }));
        }

        public bool CanCommandHome() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null;

        private static void Home()
        {
            Instance.ToolsVisibility = Visibility.Collapsed;
            Instance.DisplayHomeView();
        }

        private ICommand _basicDataBtn;
        public ICommand BasicDataBtn
        {
            get => _basicDataBtn ?? (_basicDataBtn = new RelayCommand(x => { BasicData(); CanCommandBasicData(); }));
        }

        public bool CanCommandBasicData() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...

        private static void BasicData()
        {
            if (Instance.CurrentTool != "BasicData")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = BasicDataViewModel.Instance;
                Instance.CurrentTool = "BasicData";
                Instance.SelectedViewModel = null;
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }

        private ICommand _commissionBtn;
        public ICommand CommissionBtn
        {
            get => _commissionBtn ?? (_commissionBtn = new RelayCommand(x => { Commission(); CanCommandC(); }));
        }

        public bool CanCommandC() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...

        private static void Commission()
        {
            if (Instance.CurrentTool != "Commission")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = CommissionViewModel.Instance;
                Instance.CurrentTool = "Commission";
                Instance.SelectedViewModel = null;
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }

        }

        private ICommand _employmentManagementBtn;
        public ICommand EMBtn
        {
            get => _employmentManagementBtn ?? (_employmentManagementBtn = new RelayCommand(x => { EmployeeManagement(); CanCommandEM(); }));
        }

        public bool CanCommandEM() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...

        private static void EmployeeManagement()
        {
            if (Instance.CurrentTool != "EmployeeManagement")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = EmployeeManagementViewModel.Instance;
                Instance.CurrentTool = "EmployeeManagement";
                Instance.SelectedViewModel = null;
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }

        private ICommand _insuranceBtn;
        public ICommand InsuranceBtn
        {
            get => _insuranceBtn ?? (_insuranceBtn = new RelayCommand(x => { Insurance(); CanCommandIB(); }));
        }

        public bool CanCommandIB() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...


        private static void Insurance()
        {
            if (Instance.CurrentTool != "Insurance")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = InsuranceViewModel.Instance;
                Instance.CurrentTool = "Insurance";
                Instance.SelectedViewModel = null;
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }

        private ICommand _searchBtn;
        public ICommand SearchBtn
        {
            get => _searchBtn ?? (_searchBtn = new RelayCommand(x => { SearchIndex(); CanCommandSearch(); }));
        }

        public bool CanCommandSearch() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...

        private static void SearchIndex()
        {
            if (Instance.CurrentTool != "Search")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = SearchValueViewModel.Instance;
                Instance.CurrentTool = "Search";
                Instance.SelectedViewModel = null;
            }
            else
            {
                Instance.ToolsVisibility = Visibility.Collapsed;
                Instance.CurrentTool = "";
                Instance.SelectedViewModel = HomeViewModel.Instance;
            }
        }


        private ICommand _sapBtn;
        public ICommand SapBtn
        {
            get => _sapBtn ?? (_sapBtn = new RelayCommand(x => { Sap(); CanCommandSap(); }));
        }

        public bool CanCommandSap() => true;  //Ska ändras till --> MainViewModel.Instance.Context.CurrentEmployee != null && (MainViewModel.Instance.Context.CurrentEmployee.PÅBEFATTNINGSTYP...

        private static void Sap()
        {
            if (Instance.CurrentTool != "StatisticsAndProspect")
            {
                Instance.ToolsVisibility = Visibility.Visible;
                Instance.Tools = StatisticsAndProspectusViewModel.Instance;
                Instance.CurrentTool = "StatisticsAndProspect";
                Instance.SelectedViewModel = null;
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

        private void CreateOptionalTypes()
        {
            List<OptionalType> OptionList = new List<OptionalType>();


            OptionList.Add(new OptionalType { OptionalTypeId = 1, OptionalName = "Invaliditet vid olycksfall" });
            OptionList.Add(new OptionalType { OptionalTypeId = 2, OptionalName = "Höjning av livförsäkring" });
            OptionList.Add(new OptionalType { OptionalTypeId = 3, OptionalName = "Månadsersättning vid långvarig sjukskrivning" });

            List<OptionalType> NewOptionList = new List<OptionalType>();

            foreach(var i in Context.IController.GetAllOPT())
            {
                NewOptionList.Add(i);
            }
            if(NewOptionList.Count == 0)
            {
                foreach (var item in OptionList)
                {
                    Context.IController.AddOptionalTypes(item);
                }
            }
        }
        private void CreateSAInsurances()
        {
            List<SAInsurance> SAList = new List<SAInsurance>();
            SAList.Add(new SAInsurance { SAID = 1, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för barn" });
            SAList.Add(new SAInsurance { SAID = 2, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för vuxen"});

            List<SAInsurance> NewList = new List<SAInsurance>();
            foreach (var i in Context.IController.GetAllSAI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in SAList)
                {
                    Context.IController.AddSaInsurances(item);
                }
            }
        }

        private void CreateLifeInsurance()
        {
            List<LifeInsurance> LifeList = new List<LifeInsurance>();


            LifeList.Add(new LifeInsurance {LifeID = 1, LifeName = "Livförsäkring för vuxen" });

            List<LifeInsurance> NewList = new List<LifeInsurance>();
            foreach (var i in Context.IController.GetAllLIFE())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in LifeList)
                {
                    Context.IController.AddLifeInsurance(item);
                }
            }

        }

        private void CreateCompanyInsurance()
        {
            List<CompanyInsurance> CompList = new List<CompanyInsurance>();


            CompList.Add(new CompanyInsurance {FFId = 1, COIName = "Företagsförsäkring" });


            List<CompanyInsurance> NewList = new List<CompanyInsurance>();

            foreach (var i in Context.IController.GetAllCAI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in CompList)
                {
                    Context.IController.AddCompanyInsurance(item);
                }
            }

        }

        private void CreateOtherPersonInsurance()
        {
            List<OtherPersonInsurance> OPList = new List<OtherPersonInsurance>();


            OPList.Add(new OtherPersonInsurance { OPIId = 1, OPIName ="Övrig personförsäkring" });

            List<OtherPersonInsurance> NewList = new List<OtherPersonInsurance>();

            foreach (var i in Context.IController.GetAllOPI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in OPList)
                {
                    Context.IController.AddOtherPersonInsurance(item);
                }
            }

        }

    }
}

