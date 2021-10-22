using GUILayer.Commands;
using GUILayer.ViewModels.InsuranceViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchInsuranceTakerCompanyViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerCompanyViewModel Instance = new SearchInsuranceTakerCompanyViewModel();

        public SearchInsuranceTakerCompanyViewModel()
        {
            Companies = UpdateCompanies();
            CompanyGrid = CollectionViewSource.GetDefaultView(Companies);
            CompanyGrid.Filter = new Predicate<object>(o => Filter(o as Company));
            Insurances = UpdateInsurance();
            InsuredPersons = UpdateInsuredPersons();
        }
        
        #region list
        public ObservableCollection<Company> UpdateCompanies()
        {
            ObservableCollection<Company> x = new ObservableCollection<Company>();
            foreach (var c in Context.ITController.GetAllCompanies())
            {
                x?.Add(c);
            }
            Companies = x;
            return Companies;
        }

        public ObservableCollection<Insurance> UpdateInsurance()
        {

            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            if (SelectedCompany != null)
            {
                foreach (var ia in Context.IController.GetInsuranceTakerIASC(SelectedCompany))
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
            if (SelectedCompany != null)
            {
                foreach (var ip in Context.IPController.GetInsuranceTakerIPSC(SelectedCompany))
                {
                    x?.Add(ip);
                }
            }
            InsuredPersons = x;
            return InsuredPersons;
        }

        #endregion

        #region Specific Porperties and methods for search in collection

        private ICollectionView _companyCollection;
        public ICollectionView CompanyGrid
        {
            get => _companyCollection; 
            set { _companyCollection = value; OnPropertyChanged("CompanyGrid"); }
        }
        private bool Filter(Company company)
        {
            return SearchInput == null
                || company.OrganizationNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || company.CompanyName.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || company.ContactPerson.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                CompanyGrid.Refresh();
            }
        }
        #endregion

        #region Properties
        public ObservableCollection<Insurance> Insurances { get; set; } = new ObservableCollection<Insurance>();
        public ObservableCollection<InsuredPerson> InsuredPersons { get; set; } = new ObservableCollection<InsuredPerson>();
        public ObservableCollection<Company> Companies { get; set; }

        private Company _selectedCompany;

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
            }
        }
        public string CompanyName
        {
            get => SelectedCompany.CompanyName;
            set
            {
                SelectedCompany.CompanyName = value;
                OnPropertyChanged("CompanyName");
            }
        }
        public string DiallingCode
        {
            get => SelectedCompany.DiallingCode;
            set
            {
                SelectedCompany.DiallingCode = value;
                OnPropertyChanged("DiallingCode");
            }
        }

        public string City
        {
            get => SelectedCompany.City;
            set
            {
                SelectedCompany.City = value;
                OnPropertyChanged("City");
            }
        }

        public string TelephoneNbr
        {
            get => SelectedCompany.TelephoneNbr;
            set
            {
                SelectedCompany.TelephoneNbr = value;
                OnPropertyChanged("TelephoneNbr");
            }
        }
        public string FaxNumber
        {
            get => SelectedCompany.FaxNumber;
            set
            {
                SelectedCompany.FaxNumber = value;
                OnPropertyChanged("FaxNumber");
            }
        }

        public string Email
        {
            get => SelectedCompany.Email;
            set
            {
                SelectedCompany.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string ContactPerson
        {
            get => SelectedCompany.ContactPerson;
            set
            {
                SelectedCompany.ContactPerson = value;
                OnPropertyChanged("ContactPerson");
            }
        }
        public string OrganizationNumber
        {
            get => SelectedCompany.OrganizationNumber;
            set
            {
                SelectedCompany.OrganizationNumber = value;
                OnPropertyChanged("OrganizationNumber");
            }
        }

        public string StreetAddress
        {
            get => SelectedCompany.StreetAddress;
            set
            {
                SelectedCompany.StreetAddress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        public int PostalCode
        {
            get => SelectedCompany.PostalCode;
            set
            {
                SelectedCompany.PostalCode = value;
                OnPropertyChanged("PostalCode");
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

        private ICommand _updateC;
        public ICommand UpdateC
        {
            get => _updateC ?? (_updateC = new RelayCommand(x => { UpdateCompany(); }));
        }
        private void UpdateCompany()
        {
            if (SelectedCompany != null && SelectedCompany.OrganizationNumber != null && SelectedCompany.StreetAddress != null && SelectedCompany.City != null &&
                SelectedCompany.PostalCode != 0 && SelectedCompany.CompanyName != null && SelectedCompany.DiallingCode != null && SelectedCompany.TelephoneNbr != null)
            {
                SelectedCompany.OrganizationNumber = OrganizationNumber;
                SelectedCompany.StreetAddress = StreetAddress;
                SelectedCompany.CompanyName = CompanyName;
                SelectedCompany.PostalCode = PostalCode;
                SelectedCompany.FaxNumber = FaxNumber;
                SelectedCompany.ContactPerson = ContactPerson;
                SelectedCompany.TelephoneNbr = TelephoneNbr;
                SelectedCompany.Email = Email;
                SelectedCompany.DiallingCode = DiallingCode;
                SelectedCompany.City = City;
                Context.ITController.Edit(SelectedCompany);

                MessageBox.Show($"Uppdateringen lyckades av: {SelectedCompany.OrganizationNumber}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                Companies.Clear();
                foreach (var c in Context.ITController.GetAllCompanies())
                {
                    Companies?.Add(c);
                }
            }
            else
            {
                MessageBox.Show("Du måste markera ett företag i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _removeC;
        public ICommand RemoveC
        {
            get => _removeC ?? (_removeC = new RelayCommand(x => { DeleteCompany(); }));
        }

        private void DeleteCompany()
        {
            if (SelectedCompany != null)
            {
                bool valid = Context.ITController.CheckCompanyInInsurance(SelectedCompany);
                if (valid == true)
                {
                    MessageBoxResult result2 = MessageBox.Show("Denna försäkringstagare är registrerad på en försäkring, vid borttagning tar du även bort försäkringen och den försäkrade. Vill du göra detta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result2 == MessageBoxResult.Yes)
                    {
                        foreach (var ip in Context.IPController.GetInsuranceTakerIPSC(SelectedCompany))
                        {
                            Context.IPController.RemoveInsuredPerson(ip);
                        }
                        foreach (var it in Context.IController.GetInsuranceTakerIASC(SelectedCompany))
                        {
                            Context.IController.RemoveInsurance(it);
                        }
                        Context.ITController.RemoveCompanyInsuranceTaker(SelectedCompany);
                        MessageBox.Show("Företaget togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        Companies.Remove(SelectedCompany);
                        Companies.Clear();
                        foreach (var p in Context.ITController.GetAllCompanies())
                        {
                            Companies?.Add(p);
                        }
                        SignedInsuranceViewModel.Instance.UpdateAC();
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedCompany.OrganizationNumber} är inte borttaget");
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Vill du ta bort försäkringstagaren?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        Context.ITController.RemoveCompanyInsuranceTaker(SelectedCompany);
                        MessageBox.Show("Företaget togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        Companies.Remove(SelectedCompany);
                        Companies.Clear();
                        foreach (var p in Context.ITController.GetAllCompanies())
                        {
                            Companies?.Add(p);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{SelectedCompany.OrganizationNumber} är inte borttaget");
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
                SelectedIP.FirstName = FirstName;
                SelectedIP.LastName= LastName;
                SelectedIP.LastName = LastName;
                SelectedIP.SocialSecurityNumberIP = SocialSecurityNumberIP;
                SelectedIP.InsuredId = InsuredId;
                Context.IPController.Edit(SelectedIP);

                MessageBox.Show($"Uppdateringen lyckades av: {SelectedIP.InsuredId}", "Lyckad uppdatering", MessageBoxButton.OK, MessageBoxImage.Information);
                InsuredPersons.Clear();
                foreach(var i in Context.IPController.GetInsuranceTakerIPSC(SelectedCompany))
                {
                    InsuredPersons?.Add(i);
                }
            }
            else
            {
                MessageBox.Show("Du måste markera ett företag i registret eller ha fyllt i alla fält med en *", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _exportC;
        public ICommand ExportC
        {
            get => _exportC ?? (_exportC = new RelayCommand(x => { ExportCompany(); }));
        }


        private void ExportCompany()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
