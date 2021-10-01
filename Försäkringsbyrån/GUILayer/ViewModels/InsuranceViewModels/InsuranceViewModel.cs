using GUILayer.Commands;
using GUILayer.Commands.InsuranceCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// viewmodel for insuranceview. 
    /// </summary>
    public class InsuranceViewModel : BaseViewModel
    {
        public static readonly InsuranceViewModel Instance = new InsuranceViewModel();

        //public RegisterApplicationBtn RegisterApplication_Btn {get;}
        //public RegisterSignedInsuranceBtn RegisterSignedInsurance_Btn { get; }

        private InsuranceViewModel()
        {
            //RegisterSignedInsurance_Btn = new RegisterSignedInsuranceBtn();
            //RegisterApplication_Btn = new RegisterApplicationBtn();
        }

        private ICommand registerApplication_Btn; 
        public ICommand RegisterApplication_Btn
        {
            get => registerApplication_Btn ?? (registerApplication_Btn = new RelayCommand(x => { RegisterApplicationHome(); CanCreate(); }));
        }

        public bool CanCreate() => true; 

        public static void RegisterApplicationHome()
        {
            if (MainViewModel.Instance.CurrentTool != "InsuranceChoices")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = Instance;
                MainViewModel.Instance.CurrentTool = "InsuranceChoices";
                MainViewModel.Instance.SelectedViewModel = InsuranceApplicationChoiceViewModel.Instance;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = Instance;
            }
        }

        private ICommand registerSignedinsurance_Btn; 

        public ICommand RegisterSignedInsurance_Btn
        {
            get => registerSignedinsurance_Btn ?? (registerSignedinsurance_Btn = new RelayCommand(x => { RegisterSignedInusuranceHome(); CanCreate(); }));
        }

        public static void RegisterSignedInusuranceHome()
        {

        }

    }
}
