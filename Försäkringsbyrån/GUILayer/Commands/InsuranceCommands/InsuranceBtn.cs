﻿using GUILayer.ViewModels;
using GUILayer.ViewModels.InsuranceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.Commands.InsuranceCommands
{
    /// <summary>
    /// One click and insurance menuchoices shows, click again and welcome page shows. 
    /// </summary>
    public class InsuranceBtn: BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;
        
        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.CurrentTool != "Insurance")
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Visible;
                MainViewModel.Instance.Tools = InsuranceViewModel.Instance;
                MainViewModel.Instance.CurrentTool = "Insurance";
                MainViewModel.Instance.SelectedViewModel = null;
            }
            else
            {
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                MainViewModel.Instance.SelectedViewModel = HomeViewModel.Instance;
            }

        }
        
    }
}
