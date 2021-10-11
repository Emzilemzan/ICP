using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Models.Models;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GUILayer.Commands;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchInsuranceTakerPersonViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerPersonViewModel Instance = new SearchInsuranceTakerPersonViewModel();

       
        public SearchInsuranceTakerPersonViewModel()
        {

            Persons = UpdatePersons();
            PersonGrid = CollectionViewSource.GetDefaultView(Persons);
            PersonGrid.Filter = new Predicate<object>(o => Filter(o as Person));
            Insurances = UpdatePersonInsurance();
            InsuredPersons = UpdateInsuredPersons();
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

        #region Methods
        public ObservableCollection<Person> UpdatePersons()
        {
            ObservableCollection<Person> x = new ObservableCollection<Person>();
            foreach (var p in Context.ITController.GetAllPersons())
            {
                x?.Add(p);
            }
            Persons = x;
            return Persons;
        }

        public ObservableCollection<Insurance> UpdatePersonInsurance()
        {

            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            if (SelectedPerson != null)
            {
                foreach (var ia in Context.IController.GetInsuranceTakerIAS(SelectedPerson))
                {
                    x?.Add(ia);
                }
            }
            Insurances = x;
            return Insurances;
        }

        public ObservableCollection<InsuredPerson> UpdateInsuredPersons()
        {

            ObservableCollection<InsuredPerson> x = new ObservableCollection<InsuredPerson>();
            if (SelectedPerson != null)
            {
                foreach (var ip in Context.IPController.GetInsuranceTakerIPS(SelectedPerson))
                {
                    x?.Add(ip);
                }
            }
            InsuredPersons = x;
            return InsuredPersons;
        }

        #endregion

        #region Properties

    
        public ObservableCollection<Person> Persons { get; set; }
        public ObservableCollection<Insurance> Insurances { get; set; } = new ObservableCollection<Insurance>();
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();

        private Person _selectedPerson;

        public Person SelectedPerson
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

        private int _postalCode;
        public int PostalCode
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

        #region commands

        private ICommand _updateIT;
        public ICommand UpdateIT
        {
            get => _updateIT ?? (_updateIT = new RelayCommand(x => { UpdatePerson(); CanCommand(); }));
        }

        private bool CanCommand() => true;

        private void UpdatePerson()
        {
            throw new NotImplementedException();
        }

        private ICommand _deleteIT;
        public ICommand DeleteIT
        {
            get => _deleteIT ?? (_deleteIT = new RelayCommand(x => { DeletePerson(); CanCommand(); }));
        }


        private void DeletePerson()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
