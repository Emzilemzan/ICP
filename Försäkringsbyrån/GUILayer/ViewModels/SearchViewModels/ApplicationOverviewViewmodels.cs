﻿using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class ApplicationOverviewViewModels : BaseViewModel
    {
        public static readonly ApplicationOverviewViewModels Instance = new ApplicationOverviewViewModels();

        private ApplicationOverviewViewModels()
        {

        }

        #region command 
        private ICommand _updateBtn;

        public ICommand UpdateBtn
        {
            get => _updateBtn ?? (_updateBtn = new RelayCommand(x => { Update();}));
        }

        public void Update()
        {

        }

        private ICommand _exportBtn;

        public ICommand ExportBtn
        {
            get => _exportBtn ?? (_exportBtn = new RelayCommand(x => { Export(); }));
        }

        public void Export()
        {

        }

        private ICommand _removeBtn;

        public ICommand RemoveBtn
        {
            get => _removeBtn ?? (_removeBtn = new RelayCommand(x => { Remove(); }));
        }

        public void Remove()
        {

        }
        #endregion


    }
}