using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Models.Models;
using System.ComponentModel;
using System.Windows.Data;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchInsuranceTakerPersonViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerPersonViewModel Instance = new SearchInsuranceTakerPersonViewModel();

        public SearchInsuranceTakerPersonViewModel()
        {
            Persons = new ObservableCollection<Person>
            {
                new Person(){InsuranceTakerId=1, SocialSecurityNumber="991204-1222", Firstname="Emma", Lastname="Gunnarsson", City="Ulricehamn", PostalCode=52335, StreetAddress="Källgatan 10", DiallingCodeHome= "073", DiallingCodeWork="037", TelephoneNbrHome="7747733", TelephoneNbrWork="89675", EmailOne="hej@hotmail.com", EmailTwo=null},
                new Person(){InsuranceTakerId=2, SocialSecurityNumber="991204-1213", Firstname="Karl", Lastname="Gunnarsson", City="Ulricehamn", PostalCode=52335, StreetAddress="Källgatan 10", DiallingCodeHome= "077", DiallingCodeWork="047", TelephoneNbrHome="7787733", TelephoneNbrWork="349675", EmailOne="heja@hotmail.com", EmailTwo="cool.1@live.se"},
            };

            PersonGrid = CollectionViewSource.GetDefaultView(Persons);
            PersonGrid.Filter = new Predicate<object>(o => Filter(o as Person));
        }

        public Person CurrentPerson { get;}

        #region Specific Porperties and methods for search in collection

        private ICollectionView _personCollection;
        public ICollectionView PersonGrid
        {
            get { return _personCollection; }
            set { _personCollection = value; OnPropertyChanged("PersonGrid"); }
        }
        private bool Filter(Person person)
        {
            return SearchInput == null
                || person.SocialSecurityNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || person.Firstname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || person.Lastname.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                PersonGrid.Refresh();
            }
        }
        #endregion

        #region Properties

        public ObservableCollection<Person> Persons { get; set; }
        public ObservableCollection<InsuranceApplication> InsuranceApplications { get; set; }
        /*
        public ObservableCollection<InsuranceApplicationViewModel> InsuranceApplications { get; set; } = new ObservableCollection<InsuranceApplicationViewModel>();
        public ObservableCollection<InsuredPersonViewModel> InsuredPersons { get; set; } = new ObservableCollection<InsuredPersonViewModel>();
        */

        #endregion
    }
}
