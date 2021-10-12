using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchApplicationViewModel : BaseViewModel
    {
        public static readonly SearchApplicationViewModel Instance = new SearchApplicationViewModel();

        private SearchApplicationViewModel()
        {
            Applications = UpdateAC();
            InsuranceGrid = CollectionViewSource.GetDefaultView(Applications);
            InsuranceGrid.Filter = new Predicate<object>(o => Filter(o as Insurance));
        }

        public ObservableCollection<Insurance> UpdateAC()
        {
        //    ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
        //    foreach (var e in Context.SMController.GetAllSalesMen())
        //    {
        //        x?.Add(e);
        //    }
        //    Applications = x;
            return Applications;
        }

        private ICollectionView _applicationCollection;
        public ICollectionView InsuranceGrid
        {
            get { return _applicationCollection; }
            set { _applicationCollection = value; OnPropertyChanged("InsuranceGrid"); }
        }

        private bool Filter(Insurance application)
        {
            return SearchInput == null
                || application.SerialNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || application.InsuredID.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
                

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
    }
}
