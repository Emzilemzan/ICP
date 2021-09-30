using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Models.Models;
using System.ComponentModel;
using System.Windows.Data;
using GUILayer.Commands.SearchIndexCommands;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchInsuranceTakerPersonViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerPersonViewModel Instance = new SearchInsuranceTakerPersonViewModel();

        public RemovePersonBtn RemoveBtn { get; }
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
        public ObservableCollection<Insurance> Insurances { get; set; }
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();

        private Person _selectedPerson;

        public Person SelectedPeron
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        private string _socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get => _socialSecurityNumber;
            set
            {
                _socialSecurityNumber = value;
                OnPropertyChanged("SocialSecurityNumber");
            }
        }

        private string _insuranceTakerId;
        public string InsuranceTakerId
        {
            get => _insuranceTakerId;
            set
            {
                _insuranceTakerId = value;
                OnPropertyChanged("InsuranceTakerId");
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname= value;
                OnPropertyChanged("Lastname");
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        private string _streetAddress;
        public string StreetAddress
        {
            get => _streetAddress;
            set
            {
                _streetAddress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        private string _diallingCodeHome;
        public string DiallingCodeHome
        {
            get => _diallingCodeHome;
            set
            {
                _diallingCodeHome = value;
                OnPropertyChanged("DiallingCodeHome");
            }
        }
        private string _telephoneNbrHome;
        public string TelephoneNbrHome
        { 
            get => _telephoneNbrHome;
            set
            {
                _telephoneNbrHome = value;
                OnPropertyChanged("TelephoneNbrHome");
            }
        }
        private string _diallingCodeWork;
        public string DiallingCodeWork
        {
            get => _diallingCodeWork;
            set
            {
                _diallingCodeWork = value;
                OnPropertyChanged("DiallingCodeWork");
            }
        }
        private string _telephoneNbrWork;
        public string TelephoneNbrWork
        {
            get => _telephoneNbrWork;
            set
            {
                _telephoneNbrWork = value;
                OnPropertyChanged("TelephoneNbrWork");
            }
        }
        private string _emailOne;
        public string EmailOne
        {
            get => _emailOne;
            set
            {
                _emailOne = value;
                OnPropertyChanged("EmailOne");
            }
        }

        private string _emailTwo;
        public string EmailTwo
        {
            get => _emailTwo;
            set
            {
                _emailTwo = value;
                OnPropertyChanged("EmailTwo");
            }
        }
        #endregion
    }
}
