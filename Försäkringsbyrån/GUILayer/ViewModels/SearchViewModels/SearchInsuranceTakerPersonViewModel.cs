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
using System.Windows;
using GUILayer.ViewModels.InsuranceViewModels;

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


        public string SocialSecurityNumber
        {
            get => SelectedPerson.SocialSecurityNumber;
            set
            {
                SelectedPerson.SocialSecurityNumber = value;
                OnPropertyChanged("SocialSecurityNumber");
            }
        }

        public string Lastname
        {
            get => SelectedPerson.Lastname;
            set
            {
                SelectedPerson.Lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public string Firstname
        {
            get => SelectedPerson.Firstname;
            set
            {
                SelectedPerson.Firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public string StreetAddress
        {
            get => SelectedPerson.StreetAddress;
            set
            {
                SelectedPerson.StreetAddress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        public int PostalCode
        {
            get => SelectedPerson.PostalCode;
            set
            {
                SelectedPerson.PostalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        public string City
        {
            get => SelectedPerson.City;
            set
            {
                SelectedPerson.City = value;
                OnPropertyChanged("City");
            }
        }

        public string DiallingCodeHome
        {
            get => SelectedPerson.DiallingCodeHome;
            set
            {
                SelectedPerson.DiallingCodeHome = value;
                OnPropertyChanged("DiallingCodeHome");
            }
        }

        public string TelephoneNbrHome
        { 
            get => SelectedPerson.TelephoneNbrHome;
            set
            {
                SelectedPerson.TelephoneNbrHome = value;
                OnPropertyChanged("TelephoneNbrHome");
            }
        }

        public string DiallingCodeWork
        {
            get => SelectedPerson.DiallingCodeWork;
            set
            {
                SelectedPerson.DiallingCodeWork = value;
                OnPropertyChanged("DiallingCodeWork");
            }
        }
        public string TelephoneNbrWork
        {
            get => SelectedPerson.TelephoneNbrWork;
            set
            {
                SelectedPerson.TelephoneNbrWork = value;
                OnPropertyChanged("TelephoneNbrWork");
            }
        }

        public string EmailOne
        {
            get => SelectedPerson.EmailOne;
            set
            {
                SelectedPerson.EmailOne = value;
                OnPropertyChanged("EmailOne");
            }
        }

        public string EmailTwo
        {
            get => SelectedPerson.EmailTwo;
            set
            {
                SelectedPerson.EmailTwo = value;
                OnPropertyChanged("EmailTwo");
            }
        }
        #endregion

        #region properties for insuredperson
        private InsuredPerson _selectedIPerson;
        public InsuredPerson SelectedIP
        {
            get => _selectedIPerson;
            set
            {
                _selectedIPerson = value;
                OnPropertyChanged("SelectedIP");
            }
        }
        public int InsuredId
        {
            get => SelectedIP.InsuredId;
            set
            {
                SelectedIP.InsuredId = value;
                OnPropertyChanged("InsuredId");
            }
        }
        public string SocialSecurityNumberIP
        {
            get => SelectedIP.SocialSecurityNumberIP;
            set
            {
                SelectedIP.SocialSecurityNumberIP = value;
                OnPropertyChanged("SocialSecurityNumberIP");
            }
        }
        public string LastName
        {
            get => SelectedIP.LastName;
            set
            {
                SelectedIP.LastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string FirstName
        {
            get => SelectedIP.FirstName;
            set
            {
                SelectedIP.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string PersonType
        {
            get => SelectedIP.PersonType;
            set
            {
                SelectedIP.PersonType = value;
                OnPropertyChanged("PersonType");
            }
        }

        #endregion

        #region commands

        private ICommand _updateP;
        public ICommand UpdateP
        {
            get => _updateP ?? (_updateP = new RelayCommand(x => { UpdatePerson(); }));
        }


        private void UpdatePerson()
        {
            if (SelectedPerson != null && SelectedPerson.DiallingCodeHome != null && SelectedPerson.City != null && SelectedPerson.Firstname != null && SelectedPerson.Lastname != null && SelectedPerson.StreetAddress != null
                && SelectedPerson.SocialSecurityNumber != null && SelectedPerson.PostalCode != 0 && SelectedPerson.TelephoneNbrHome != null && SelectedPerson.EmailOne != null)
            {
                SelectedPerson.SocialSecurityNumber = SocialSecurityNumber;
                SelectedPerson.StreetAddress = StreetAddress;
                SelectedPerson.Firstname = Firstname;
                SelectedPerson.Lastname = Lastname;
                SelectedPerson.PostalCode = PostalCode;
                SelectedPerson.TelephoneNbrHome = TelephoneNbrHome;
                SelectedPerson.TelephoneNbrWork = TelephoneNbrWork;
                SelectedPerson.EmailOne = EmailOne;
                SelectedPerson.EmailTwo = EmailTwo;
                SelectedPerson.DiallingCodeHome = DiallingCodeHome;
                SelectedPerson.DiallingCodeWork = DiallingCodeWork;
                SelectedPerson.City = City;
                Context.ITController.Edit(SelectedPerson);
                
                MessageBox.Show($"Uppdateringen lyckades av: {SelectedPerson.SocialSecurityNumber}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                Persons.Clear();
                foreach (var p in Context.ITController.GetAllPersons())
                {
                    Persons?.Add(p);
                }

            }
            else
            {
                MessageBox.Show("Du måste markera en person i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _removeP;
        public ICommand RemoveP
        {
            get => _removeP ?? (_removeP = new RelayCommand(x => { DeletePerson(); }));
        }

        private void DeletePerson()
        {
            if (SelectedPerson != null)
            {
                bool valid = Context.ITController.CheckPersonInInsurance(SelectedPerson);
                if (valid == true)
                {
                    MessageBoxResult result2 = MessageBox.Show("Denna försäkringstagare är registrerad på en försäkring, vid borttagning tar du även bort försäkringen och den försäkrade. Vill du göra detta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result2 == MessageBoxResult.Yes)
                    {
                        foreach (var ip in Context.IPController.GetInsuranceTakerIPS(SelectedPerson))
                        {
                            Context.IPController.RemoveInsuredPerson(ip);
                        }
                        foreach (var it in Context.IController.GetInsuranceTakerIAS(SelectedPerson))
                        {
                            Context.IController.RemoveInsurance(it);
                        }
                        Context.ITController.RemovePersonInsuranceTaker(SelectedPerson);
                        MessageBox.Show("Personen togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        Persons.Remove(SelectedPerson);
                        Persons.Clear();
                        foreach (var p in Context.ITController.GetAllPersons())
                        {
                           Persons?.Add(p);
                        }
                        SignedInsuranceViewModel.Instance.UpdateAC();
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedPerson.SocialSecurityNumber} är inte borttaget");
                    }
                }
                else 
                {
                    MessageBoxResult result = MessageBox.Show("Vill du ta bort försäkringstagaren?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        Context.ITController.RemovePersonInsuranceTaker(SelectedPerson);
                        MessageBox.Show("Personen togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        Persons.Remove(SelectedPerson);
                        Persons.Clear();
                        foreach (var p in Context.ITController.GetAllPersons())
                        {
                            Persons?.Add(p);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedPerson.SocialSecurityNumber} är inte borttaget");
                    }
                }
            }
            else
            {
                MessageBox.Show("Antingen har ingen person markerats i registret eller så har du lämnat något fält tomt! ", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _updateIP;
        public ICommand UpdateIP
        {
            get => _updateIP ?? (_updateIP = new RelayCommand(x => { UpdateIPerson(); }));
        }


        private void UpdateIPerson()
        {
            if (SelectedIP != null && SelectedIP.PersonType != null && SelectedIP.LastName != null && SelectedIP.FirstName != null &&
               SelectedIP.SocialSecurityNumberIP != null && SelectedIP.InsuredId != 0)
            {
                if (SelectedPerson.SocialSecurityNumber == SelectedIP.SocialSecurityNumberIP)
                {
                    MessageBox.Show("Du kan inte ändra uppgifterna, eftersom den försäkrade är samma person som försäkringstagaren");
                }
                else 
                {
                    SelectedIP.FirstName = FirstName;
                    SelectedIP.LastName = LastName;
                    SelectedIP.InsuredId = InsuredId;
                    Context.IPController.Edit(SelectedIP);
                    MessageBox.Show($"Uppdateringen lyckades av: {SelectedIP.InsuredId}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                   
                }
                InsuredPersons.Clear();
                foreach (var i in Context.IPController.GetInsuranceTakerIPS(SelectedPerson))
                {
                    InsuredPersons?.Add(i);
                }
                Persons.Clear();
                foreach (var p in Context.ITController.GetAllPersons())
                {
                    Persons?.Add(p);
                }
            }
            else
            {
                MessageBox.Show("Du måste markera en försäkrad i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _exportP;
        public ICommand ExportP
        {
            get => _exportP ?? (_exportP = new RelayCommand(x => { ExportPerson(); }));
        }


        private void ExportPerson()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
