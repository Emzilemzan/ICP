using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class SignedInsuranceViewModel : BaseViewModel
    {
        public static readonly SignedInsuranceViewModel Instance = new SignedInsuranceViewModel();
        public SignedInsuranceViewModel()
        {
            Applications = UpdateAC();
            InsuranceGrid = CollectionViewSource.GetDefaultView(Applications);
            InsuranceGrid.Filter = new Predicate<object>(o => Filter(o as Insurance));
        }

        private ICommand _showInsurance;
        public ICommand ShowInsurance
        {
            get=> _showInsurance ?? (_showInsurance = new RelayCommand(x => { ShowInsuranceMethod(); }));
        }

        private void ShowInsuranceMethod()
        {
            if (SelectedInsurance != null)
            {
                string SN = SerialNumber;
                string str = Regex.Replace(SN, @"\d", "");
                switch (str)
                {
                    case "LIV":
                        MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                        MainViewModel.Instance.CurrentTool = "";                       
                        MainViewModel.Instance.SelectedViewModel = SignedLifeInsuranceViewModel.Instance;
                        break;
                    case "FF":
                        MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                        MainViewModel.Instance.CurrentTool = "";
                        break;
                    case "SO":
                        MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                        MainViewModel.Instance.CurrentTool = "";
                        break;
                    default:
                        MessageBox.Show("Fanns ej i databasen");
                        break;
                }
                SelectedInsurance = null;
            }
        }

        #region Insurance properties

        private Insurance _si;
        public Insurance SelectedInsurance
        {
            get => _si;
            set
            {
                _si = value;
                OnPropertyChanged("SelectedInsurance");
            }
        }
        public string SerialNumber
        {
            get => SelectedInsurance.SerialNumber;
            set
            {
                SelectedInsurance.SerialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        public string TakerNbr
        {
            get => SelectedInsurance.TakerNbr;
            set
            {
                SelectedInsurance.TakerNbr = value;
                OnPropertyChanged("TakerNbr");
            }
        }

        public string TypeName
        {
            get => SelectedInsurance.TypeName;
            set
            {
                SelectedInsurance.TypeName = value;
                OnPropertyChanged("TypeName");
            }
        }

        #endregion
        #region Collection
        public ObservableCollection<Insurance> UpdateAC()
        {
            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if(e.InsuranceStatus == Status.Otecknad)
                x?.Add(e);
            }
            Applications = x;
            return Applications;
        }

        private ICollectionView _applicationCollection;
        public ICollectionView InsuranceGrid
        {
            get => _applicationCollection;
            set 
            { 
                _applicationCollection = value; 
                OnPropertyChanged("InsuranceGrid"); 
            }
        }

        private bool Filter(Insurance application)
        {
            return SearchInput == null
                || application.SerialNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || application.TypeName.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || application.TakerNbr.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                InsuranceGrid.Refresh();
            }
        }

        public ObservableCollection<Insurance> Applications { get; set; }

        #endregion
        
    }
}
