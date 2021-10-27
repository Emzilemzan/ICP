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
    /// <summary>
    /// viewmodel for insuranceview. 
    /// </summary>
    public class InsuranceViewModel : BaseViewModel
    {
        public static readonly InsuranceViewModel Instance = new InsuranceViewModel();


        private InsuranceViewModel()
        {

        }

        private ICommand registerApplication_Btn; 
        public ICommand RegisterApplication_Btn => registerApplication_Btn ?? (registerApplication_Btn = new RelayCommand(x => { RegisterApplicationHome(); }));

        private void RegisterApplicationHome()
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
            get => registerSignedinsurance_Btn ?? (registerSignedinsurance_Btn = new RelayCommand(x => { RegisterSignedInusuranceHome(); }));
        }

        private void RegisterSignedInusuranceHome()
        {
            if (MainViewModel.Instance.CurrentTool != "SignedInsurance")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = SignedInsuranceViewModel.Instance;
            }
        }

    }
}
