﻿using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
    /// <summary>
    /// viewmodel for statisticsandprospectusview
    /// </summary>
    public class StatisticsAndProspectusViewModel : BaseViewModel
    {
        public static readonly StatisticsAndProspectusViewModel Instance = new StatisticsAndProspectusViewModel();

        private StatisticsAndProspectusViewModel()
        {
            
        }

        private ICommand trendstatic_btn;
        public ICommand Trendstatic_btn
        {
            get => trendstatic_btn ?? (trendstatic_btn = new RelayCommand(x => { Trendstatic(); CanCreate(); }));
        }
        public bool CanCreate() => true;
        public static void Trendstatic()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = GetTrendstatitcsViewModel.Instance;
        }

        private ICommand getStatics_Btn;
        public ICommand GetStatics_Btn
        {
            get => getStatics_Btn ?? (getStatics_Btn = new RelayCommand(x => { GetStatics(); CanCreate(); }));

        }
        public static void GetStatics()
        {
            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = GetandexportSalesstatisticsViewModel.Instance;
        }

        private ICommand searchleads_Btn;
        public ICommand Searchleads_Btn
        {
            get => searchleads_Btn ?? (searchleads_Btn = new RelayCommand(x => { Searchleads(); CanCreate(); }));

        }
        public static void Searchleads()
        {

            MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
            MainViewModel.Instance.CurrentTool = "";
            MainViewModel.Instance.SelectedViewModel = GetandexportCustomerLeadsViewModel.Instance;
        }





    }
}
