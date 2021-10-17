using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class InsuranceApplicationChoiceViewModel: BaseViewModel
    {
        public static readonly InsuranceApplicationChoiceViewModel Instance = new InsuranceApplicationChoiceViewModel();

      

        private InsuranceApplicationChoiceViewModel()
        {
            
        }
        /// <summary>
        /// Command to open Person application view.
        /// </summary>
        private ICommand pi_Btn;
        public ICommand PI_Btn
        {
            get => pi_Btn ?? (pi_Btn = new RelayCommand(x => { PersonApplicationHome(); CanCreate(); }));
        }

        public bool CanCreate() => true;
        private static void PersonApplicationHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed; 
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterPersonApplicationViewModel.Instance;
        }


        private ICommand liBtn;
        public ICommand LI_Btn
        {
            get => liBtn ?? (liBtn = new RelayCommand(x => { LifeApplicationHome(); CanCreate(); }));
        }

        private static void LifeApplicationHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterLifeApplicationViewModel.Instance;
        }

        /// <summary>
        /// Button to open Other person insurance company application. 
        /// </summary>
        private ICommand opic_Btn;
        public ICommand OPIC_Btn
        {
            get => opic_Btn ?? (opic_Btn = new RelayCommand(x => { OPICAHome(); CanCreate(); })); 
        }

        public static void OPICAHome()
        {

            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterOPICApplicationViewModel.Instance;
        }

        /// Command to open company Insurance view
        /// 
        private ICommand ci_Btn;
        public ICommand CI_Btn
        {
            get => ci_Btn ?? (ci_Btn = new RelayCommand(x => { CompanyInsuranceHome(); CanCreate(); }));
        }

        public static void CompanyInsuranceHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterApplicationViewModel.Instance;
        }

        ///
        // Command to open other person insurance persons 
        private ICommand pip_Btn;
        public ICommand OPIP_Btn
        {
            get => pip_Btn ?? (pip_Btn = new RelayCommand(x => { OPIPHome(); CanCreate(); }));
        }

        public static void OPIPHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = RegisterOPIPApplicationViewModel.Instance;
        }
    }
}
