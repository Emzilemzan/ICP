﻿using GUILayer.Commands;
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
    /// viewmodel for searchvalueview menuchoises. 
    /// </summary>
    public class SearchValueViewModel : BaseViewModel
    {
        public static readonly SearchValueViewModel Instance = new SearchValueViewModel();
        private SearchValueViewModel()
        {
        }

        #region  Commands
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
            get => searchEmployee_Btn ?? (searchEmployee_Btn = new RelayCommand(x => { SearchEmployeeHome(); CanCreate(); }));

        }
        public static void SearchEmployeeHome()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = SearchEmployeeViewModel.Instance;
        }


        private ICommand searchApplication_Btn;
        public ICommand SearchInsurance_Btn
        {
            get => searchApplication_Btn ?? (searchApplication_Btn = new RelayCommand(x => { SearchApplicationHome(); CanCreate(); }));

        }

        public static void SearchApplicationHome()
        {
            if (MainViewModel.Instance.CurrentTool != "Insurance")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = Instance;
                MainViewModel.Instance.CurrentTool = "Insurance";
                MainViewModel.Instance.SelectedViewModel = SearchApplicationChoiceViewModel.Instance;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = Instance;
            }
        }

        #endregion

    }


}

