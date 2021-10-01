using GUILayer.Commands;
using GUILayer.Commands.SearchIndexCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    /// <summary>
    /// viewmodel for searchvalueview
    /// </summary>
    class SearchValueViewModel : BaseViewModel
    {
        public static readonly SearchValueViewModel Instance = new SearchValueViewModel();

        //public SearchCompanyBtn SearchCompany_Btn { get; }
        //public SearchPersonBtn SearchPerson_Btn { get; }
        //public SearchSignedInsuranceBtn SearchSignedI_Btn { get; }
        //public SearchEmployeeBtn SearchEmployee_Btn { get; }
        //public SearchApplicationBtn SearchApplication_Btn { get; }


        private SearchValueViewModel()
        {
            //SearchCompany_Btn = new SearchCompanyBtn();
            //SearchPerson_Btn = new SearchPersonBtn();
            //SearchEmployee_Btn = new SearchEmployeeBtn();
            //SearchApplication_Btn = new SearchApplicationBtn();
            //SearchSignedI_Btn = new SearchSignedInsuranceBtn();
        }
        /// <summary>
        /// Search Companie commad.
        /// </summary>
        private ICommand searchCompany_Btn;
        public ICommand SearchCompany_Btn
        {
            get => searchCompany_Btn ?? (searchCompany_Btn = new RelayCommand(x => { SearchCompanyHome(); CanCreate(); }));

        }
        public bool CanCreate() => true;

        public static void SearchCompanyHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = SearchInsuranceTakerCompanyViewModel.Instance;
        }

        private ICommand searchPerson_Btn;
        public ICommand SearchPerson_Btn
        {
            get => searchPerson_Btn ?? (searchPerson_Btn = new RelayCommand(x => { SearchPersonHome(); CanCreate(); }));

        }
        
        public static void SearchPersonHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = SearchInsuranceTakerPersonViewModel.Instance;
        }

        // Search Employee Command open the view.

        private ICommand searchEmployee_Btn; 
        public ICommand SearchEmployeeBtn
        {
            get => searchEmployee_Btn ?? (searchEmployee_Btn = new RelayCommand(x => { SearchPersonHome(); CanCreate(); }));

        }
        public static void SearchEmployeeHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = SearchEmployeeViewModel.Instance;
        }

        //SearchApplication_Btn = new SearchApplicationBtn();
        private ICommand searchApplication_Btn;
        public ICommand SearchApplicationBtn
        {
            get => searchApplication_Btn ?? (searchApplication_Btn = new RelayCommand(x => { SearchApplicationHome(); CanCreate(); }));

        }

        public static void SearchApplicationHome()
        {
            // no code yet.
        }
        //SearchSignedI_Btn = new SearchSignedInsuranceBtn();

        private ICommand searchSignedInsurance_Btn;
        public ICommand SearchSignedInsuranceBtn
        {
            get => searchSignedInsurance_Btn ?? (searchSignedInsurance_Btn = new RelayCommand(x => { SearchSignedInsuranceHome(); CanCreate(); }));

        }
        public static void SearchSignedInsuranceHome()
        {
            // no code yet.
        }


    }


}

