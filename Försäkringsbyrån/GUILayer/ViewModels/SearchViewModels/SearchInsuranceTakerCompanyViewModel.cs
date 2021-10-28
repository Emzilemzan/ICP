using GUILayer.Commands;
using GUILayer.ViewModels.InsuranceViewModels;
using Models.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.SearchViewModels
{
    /// <summary>
    /// Viewmodel to search all insurancestakers that are companies.
    /// </summary>
    public class SearchInsuranceTakerCompanyViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerCompanyViewModel Instance = new SearchInsuranceTakerCompanyViewModel();

        public SearchInsuranceTakerCompanyViewModel()
        {
            
            Insurances = UpdateInsurance();
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
        // Method that helps users to search for comopanys in our CollectionView.
        private bool Filter(Company company)
        {
            return SearchInput == null
                || company.OrganizationNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || company.CompanyName.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
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
       
        public void UpdateGridToDb()
        {
            Companies = UpdateCompanies();
            InsuredPersons = UpdateInsuredPersons();
            if (Companies != null)
            {
                foreach (Company c in Companies)
                {
                    Context.ITController.Edit(c);
                }
                CompanyGrid = CollectionViewSource.GetDefaultView(Companies);
                CompanyGrid.Filter = new Predicate<object>(o => Filter(o as Company));
            }
            if (InsuredPersons != null)
            {
                foreach (InsuredPerson ip in InsuredPersons)
                {
                    Context.IPController.Edit(ip);
                }
            }
        }
       
        private ICommand _removeC;
        public ICommand RemoveC => _removeC ?? (_removeC = new RelayCommand(x => { DeleteCompany(); }));
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
       
        private ICommand _exportC;
        public ICommand ExportC => _exportC ?? (_exportC = new RelayCommand(x => { ExportCompany(); }));

        /// <summary>
        /// method to create pdf document and open it when button is clicked. 
        /// </summary>
        private void ExportCompany()
        {
            if (SelectedCompany != null)
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream("FörsäkringstagareFöretag.pdf", FileMode.Create));
                BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                Font times = new Font(basefont, 15);
                document.Open();
                document.Add(new Paragraph("Försäkringstagare Företag", times));
                document.Add(new Paragraph("Organisationsnummer: \t" + SelectedCompany.OrganizationNumber));
                document.Add(new Paragraph("Företagsnamn: \t" + SelectedCompany.CompanyName));
                document.Add(new Paragraph("Gatuadress: \t" + SelectedCompany.StreetAddress));
                document.Add(new Paragraph("Postnummer: \t" + SelectedCompany.PostalCode));
                document.Add(new Paragraph("Postort: \t" + SelectedCompany.City));
                document.Add(new Paragraph("Rikt- & telefonnummer: \t" + SelectedCompany.DiallingCode + "-" + SelectedCompany.TelephoneNbr));
                document.Add(new Paragraph("Email: \t" + SelectedCompany.Email));
                document.Add(new Paragraph("Faxnummer: \t" + SelectedCompany.FaxNumber));
                document.Add(new Paragraph("Kontaktperson: \t" + SelectedCompany.ContactPerson));
                document.Close();
                Process.Start("FörsäkringstagareFöretag.pdf");
            }
            else
            {
                MessageBox.Show("Du måste markera ett företag att exportera. ");
            }
        }

        #endregion
    }
}
